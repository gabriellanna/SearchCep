namespace Cep.Domain.Models
{
    public class Neighborhood : BaseEntity
    {
        public Neighborhood()
        {
        }
        public Neighborhood(string name, int cityId)
        {
            Name = name;
            CityId = cityId;
        }
        public string Name { get; set; }
        public IList<Street> Streets { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}