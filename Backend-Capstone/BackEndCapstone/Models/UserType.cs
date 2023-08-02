using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class UserType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        public static int ADMIN_ID => 1;
        public static int PATIENT_ID => 2;
        public static int PROVIDER_ID => 3;
    }
}
