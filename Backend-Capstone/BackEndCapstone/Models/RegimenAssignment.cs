using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class RegimenAssignment
    {
        public int Id { get; set; }

        [Required]
        public int RegimenId { get; set; }
        public Regimen? Regimen { get; set; }

        [Required]
        public int PatientProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

        [Required]
        public DateTime AssignmentDate { get; set; }
    }
}
