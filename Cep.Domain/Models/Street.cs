namespace Cep.Domain.Models
{
    public class Street : BaseEntity
    {
        public Street()
        {
        }
        public Street(string name)
        {
            Name = name;
        }
        public Street(string name, string cep, int neighborhoodId, DateTime lastUpdate)
        {
            Name = name;
            Cep = cep;
            NeighborhoodId = neighborhoodId;
            LastUpdate = lastUpdate;
        }

        public string Name { get; set; }
        public string Cep { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public int NeighborhoodId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}