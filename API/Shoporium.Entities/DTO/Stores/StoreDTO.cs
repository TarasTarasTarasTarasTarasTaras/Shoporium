﻿using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Stores
{
    public class StoreDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MainPhoto { get; set; } = string.Empty;
        public string BackgroundPhoto { get; set; } = string.Empty;
        public string OtherCategoryName { get; set; } = string.Empty;
        public StoreStatus Status { get; set; }

        public long UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
