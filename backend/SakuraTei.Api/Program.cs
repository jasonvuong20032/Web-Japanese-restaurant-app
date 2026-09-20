using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using SakuraTei.Api.Common;
using SakuraTei.Api.Endpoints;
using SakuraTei.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Dịch vụ
// ---------------------------------------------------------------------------

// Thực đơn và bài viết là dữ liệu tĩnh; đơn hàng và đặt bàn giữ trong bộ nhớ tiến trình.
// Cả bốn đều singleton để trạng thái không mất giữa các request.
builder.Services.AddSingleton<IDishService, DishService>();
builder.Services.AddSingleton<IBlogService, BlogService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IReservationService, ReservationService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    // Giữ nguyên tiếng Việt có dấu trong JSON thay vì escape thành \uXXXX.
    options.SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Sakura Tei API",
        Version = "v1",
        Description =
            "API thực đơn, đặt món và đặt bàn cho website ẩm thực Nhật Sakura Tei. "
            + "Bản này lưu dữ liệu trong bộ nhớ, chưa gắn cơ sở dữ liệu.",
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "SakuraTei.Api.xml");
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

const string CorsPolicy = "AllowFrontend";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:5173"];

builder.Services.AddCors(options => options.AddPolicy(CorsPolicy, policy => policy
    .WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();

// ---------------------------------------------------------------------------
// Pipeline
// ---------------------------------------------------------------------------

// Bắt mọi lỗi chưa xử lý và trả ProblemDetails, không để lộ stack trace ra ngoài.
app.UseExceptionHandler(handler => handler.Run(async context =>
{
    var feature = context.Features.Get<IExceptionHandlerFeature>();
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("UnhandledException");
    logger.LogError(feature?.Error, "Lỗi chưa xử lý tại {Path}", context.Request.Path);

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/json; charset=utf-8";

    var body = new ProblemDetailsBody(
        "Lỗi máy chủ",
        app.Environment.IsDevelopment() && feature is not null
            ? feature.Error.Message
            : "Đã có lỗi xảy ra phía máy chủ. Vui lòng thử lại sau.",
        StatusCodes.Status500InternalServerError);

    await context.Response.WriteAsJsonAsync(body);
}));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sakura Tei API v1");
        options.DocumentTitle = "Sakura Tei API";
    });
}

app.UseCors(CorsPolicy);

var api = app.MapGroup("/api");
api.MapMenuEndpoints();
api.MapOrderEndpoints();
api.MapReservationEndpoints();
api.MapBlogEndpoints();

app.MapGet("/health", (IDishService dishes) => TypedResults.Ok(new
{
    Status = "healthy",
    Service = "Sakura Tei API",
    DishCount = dishes.Search(new() { PageSize = 1 }).TotalItems,
    CategoryCount = dishes.GetCategories().Count,
    ServerTime = DateTimeOffset.Now,
}))
.WithName("HealthCheck")
.WithSummary("Kiểm tra API sống và đã nạp đủ thực đơn.");

// Vào thẳng gốc thì chuyển sang Swagger cho tiện khi phát triển.
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.Run();
