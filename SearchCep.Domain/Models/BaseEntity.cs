using System.ComponentModel.DataAnnotations;

namespace SearchCep.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}