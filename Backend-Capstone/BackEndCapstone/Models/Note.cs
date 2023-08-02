using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public int ProviderProfileId { get; set; }

        [Required]
        public int PatientProfileId { get; set; }
        public UserProfile? ProviderProfile { get; set; }
        public UserProfile? PatientProfile { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }
    }
}
