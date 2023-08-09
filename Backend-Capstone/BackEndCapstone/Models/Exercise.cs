using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string Muscle { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Instructions { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(255)]
        public string VideoLocation { get; set; }
        public Regimen? Regimen { get; set; }

        public RegimenAssignment? RegimenAssignment { get; set; }
    }
}
