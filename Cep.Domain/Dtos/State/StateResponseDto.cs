namespace Cep.Domain.Dtos.State
{
    public class StateResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CityStateResponseDto>? Cities { get; set; }
    }
}