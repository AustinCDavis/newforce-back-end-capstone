using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IRegimenExerciseRepository
    {
        //RegimenExercise GetRegimenExerciseByRegimenIdAndExerciseId(int regimenId, int exerciseId);
        void Add(RegimenExercise regimenExercise);
        void Delete(int id);
        void Update(RegimenExercise regimenExercise);
    }
}