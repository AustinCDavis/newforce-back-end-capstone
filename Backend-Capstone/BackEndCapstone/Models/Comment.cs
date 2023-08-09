using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RegimenExerciseId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }

    }
}
