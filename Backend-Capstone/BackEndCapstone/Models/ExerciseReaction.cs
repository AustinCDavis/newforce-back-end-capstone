using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class ExerciseReaction
    {
        public int Id { get; set; }

        [Required]
        public int RegimenExerciseId { get; set; }
        public RegimenExercise? RegimenExercise { get; set; }

        [Required]
        public int ReactionId { get; set; }
        public Reaction? Reaction { get; set; }
    }
}
