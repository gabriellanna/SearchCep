namespace Cep.Domain.Models
{
    public class State : BaseEntity
    {
        public State()
        {
        }
        public State(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public IList<City>? Cities { get; set; }
    }
}