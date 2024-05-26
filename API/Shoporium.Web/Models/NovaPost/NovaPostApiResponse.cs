namespace Shoporium.Web.Models.NovaPost
{
    public class NovaPostAdresses : NovaPostApiResponse
    {
        public List<Data> Data { get; set; } = new List<Data>();
    }

    public class NovaPostPostOffices: NovaPostApiResponse
    {
        public List<NovaPostApiWarehouseResponse>? Data { get; set; }
    }

    public class NovaPostApiResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public List<string> MessageCodes { get; set; } = new List<string>();
        public List<string> ErrorCodes { get; set; } = new List<string>();
        public List<string> WarningCodes { get; set; } = new List<string>();
        public List<string> InfoCodes { get; set; } = new List<string>();
    }

    public class Data
    {
        public int TotalCount { get; set; }
        public List<NovaPostApiAddressReponse>? Addresses { get; set; }
    }
}
