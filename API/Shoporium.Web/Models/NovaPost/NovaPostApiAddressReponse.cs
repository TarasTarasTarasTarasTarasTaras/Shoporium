namespace Shoporium.Web.Models.NovaPost
{
    public class NovaPostApiAddressReponse
    {
        public string Present { get; set; } = string.Empty;
        public int Warehouses { get; set; }
        public string MainDescription { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string SettlementTypeCode { get; set; } = string.Empty;
        public string Ref { get; set; } = string.Empty;
        public string DeliveryCity { get; set; } = string.Empty;
        public bool AddressDeliveryAllowed { get; set; }
        public bool StreetsAvailability { get; set; }
        public string ParentRegionTypes { get; set; } = string.Empty;
        public string ParentRegionCode { get; set; } = string.Empty;
        public string RegionTypes { get; set; } = string.Empty;
        public string RegionTypesCode { get; set; } = string.Empty;
    }
}
