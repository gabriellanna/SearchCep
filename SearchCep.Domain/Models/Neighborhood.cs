namespace SearchCep.Domain.Models
{
    public class Neighborhood : BaseEntity
    {
        public Neighborhood()
        {
        }
        public Neighborhood(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public IList<Street> Streets { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}