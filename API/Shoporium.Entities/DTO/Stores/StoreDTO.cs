using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Stores
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MainPhoto { get; set; } = string.Empty;
        public string BackgroundPhoto { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public string OtherCategoryName { get; set; } = string.Empty;

        public StoreStatus Status { get; set; }

        public byte[]? DownloadedMainPhoto { get; set; }
        public byte[]? DownloadedBackgroundPhoto { get; set; }

        public long UserId { get; set; }
    }
}
