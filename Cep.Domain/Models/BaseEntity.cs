using System.ComponentModel.DataAnnotations;

namespace Cep.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}