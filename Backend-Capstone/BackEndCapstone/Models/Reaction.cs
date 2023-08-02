using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Reaction
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string IconCode { get; set; }
    }
}
