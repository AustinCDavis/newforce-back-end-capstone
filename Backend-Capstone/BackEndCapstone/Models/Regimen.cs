using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Regimen
    {
        public int Id { get; set; }

        [Required]
        public int ProviderProfileId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDateTime { get; set; }

        public RegimenExercise? RegimenExercise { get; set; }
        public UserProfile? UserProfile { get; set; }
    }
}
