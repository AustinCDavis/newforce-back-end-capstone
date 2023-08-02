using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class RegimenExercise
    {
        public int Id { get; set; }

        [Required]
        public int RegimenId { get; set; }
        public Regimen? Regimen { get; set; }

        [Required]
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
