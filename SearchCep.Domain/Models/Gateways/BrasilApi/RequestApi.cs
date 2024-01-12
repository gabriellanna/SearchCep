namespace SearchCep.Domain.Models.Gateways.BrasilApi
{
    public class ResponseApi : BaseEntity
    {
        public string Cep { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Service { get; set; }

    }
}