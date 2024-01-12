namespace SearchCep.Domain.Models
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
        public Street(string name, string cep)
        {
            Name = name;
            Cep = cep;
        }

        public string Name { get; set; }
        public string Cep { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public int NeighborhoodId { get; set; }
    }
}