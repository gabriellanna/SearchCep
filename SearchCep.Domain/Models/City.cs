namespace SearchCep.Domain.Models
{
    public class City : BaseEntity
    {
        public City()
        {
        }
        public City(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public IList<Neighborhood> Neighborhoods { get; set; }
        public State State { get; set; }
        public int StateId { get; set; }
    }
}