/**
 * Thông tin thương hiệu xuất hiện ở nhiều trang (Header, Footer, Về chúng tôi, Đặt bàn…).
 * Gom về một chỗ để đổi hotline hay địa chỉ thì chỉ sửa một dòng.
 */

const ADDRESS = '128 Lê Lợi, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh'

export const BRAND = {
  name: 'Sakura Tei',
  nameJp: 'さくら亭',
  slogan: 'おいしい料理と、静かな時間を。',

  address: ADDRESS,
  addressShort: '128 Lê Lợi, Quận 1, TP. Hồ Chí Minh',

  hotline: '1900 6868',
  hotlineHref: 'tel:19006868',
  email: 'xinchao@sakuratei.vn',
  emailHref: 'mailto:xinchao@sakuratei.vn',

  openingHours: '11:00 – 22:00 mỗi ngày',
  lastSeating: '21:00',

  /** Bản đồ nhúng — dạng `output=embed` không cần API key. */
  mapEmbedUrl: `https://www.google.com/maps?q=${encodeURIComponent(ADDRESS)}&hl=vi&z=17&output=embed`,
  /** Mở thẳng ứng dụng Google Maps trên điện thoại, hoặc trang Maps trên máy tính. */
  mapLinkUrl: `https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(ADDRESS)}`,
} as const
