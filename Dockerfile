# Bản deploy gộp: frontend build xong được nhét vào wwwroot của API, nên ảnh cuối
# chỉ chạy một process và web với API dùng chung một origin.

# ---------------------------------------------------------------------------
# 1. Dựng frontend
# ---------------------------------------------------------------------------
FROM node:22-bookworm-slim AS web
WORKDIR /src/frontend

# Chép manifest trước để tầng cài gói được cache lại khi chỉ có mã nguồn đổi.
COPY frontend/package.json frontend/package-lock.json ./
RUN npm ci

COPY frontend/ ./
# Không set VITE_API_BASE_URL: web và API cùng origin nên đường dẫn /api mặc định là đúng.
RUN npm run build

# ---------------------------------------------------------------------------
# 2. Dựng backend
# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS api
WORKDIR /src

COPY backend/SakuraTei.Api/SakuraTei.Api.csproj backend/SakuraTei.Api/
RUN dotnet restore backend/SakuraTei.Api/SakuraTei.Api.csproj

COPY backend/ backend/
COPY --from=web /src/frontend/dist/ backend/SakuraTei.Api/wwwroot/

RUN dotnet publish backend/SakuraTei.Api/SakuraTei.Api.csproj -c Release -o /app

# ---------------------------------------------------------------------------
# 3. Ảnh chạy
# ---------------------------------------------------------------------------
# Dùng bản Debian chứ không dùng Alpine: DishService bỏ dấu tiếng Việt bằng
# string.Normalize(FormD), việc này cần thư viện ICU. Thiếu ICU thì tìm kiếm
# không dấu hỏng âm thầm — gõ "ca hoi" không ra "cá hồi" mà chẳng báo lỗi gì.
# Vì lý do đó cũng đừng đặt DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_URLS=http://+:8080

COPY --from=api /app ./

# Chạy bằng người dùng thường, không chạy bằng root.
USER $APP_UID

EXPOSE 8080
ENTRYPOINT ["dotnet", "SakuraTei.Api.dll"]
