namespace Cep.Domain.Dtos.Address
{
    public class AddressResponseDto
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public int StreetId { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string Neighborhood { get; set; }
        public int NeighborhoodId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}