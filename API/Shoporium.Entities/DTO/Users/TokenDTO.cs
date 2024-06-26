﻿namespace Shoporium.Entities.DTO.Users
{
    public class TokenDTO
    {
        public long TokenId { get; set; }
        public string TokenValue { get; set; } = string.Empty;
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
