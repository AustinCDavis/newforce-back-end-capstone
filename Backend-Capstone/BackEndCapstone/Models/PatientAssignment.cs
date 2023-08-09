using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class PatientAssignment
    {
        public int Id { get; set; }

        [Required]
        public int ProviderProfileId { get; set; }

        [Required]
        public int PatientProfileId { get; set; }
        public UserProfile? ProviderProfile { get; set; }
        public UserProfile? PatientProfile { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
