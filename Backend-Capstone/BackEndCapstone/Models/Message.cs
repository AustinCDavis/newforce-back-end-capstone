using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public int FromId { get; set; }

        [Required]
        public int ToId { get; set; }
        public UserProfile? FromUserProfile { get; set; }
        public UserProfile? ToUserProfile { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }
    }
}
