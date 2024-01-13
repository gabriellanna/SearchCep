namespace Cep.Domain.Models
{
    public class City : BaseEntity
    {
        public City()
        {
        }
        public City(string name, int stateId)
        {
            Name = name;
            StateId = stateId;
        }
        public string Name { get; set; }
        public IList<Neighborhood> Neighborhoods { get; set; }
        public State State { get; set; }
        public int StateId { get; set; }
    }
}