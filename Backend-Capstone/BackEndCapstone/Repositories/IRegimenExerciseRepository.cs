using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IRegimenExerciseRepository
    {
        void Add(RegimenExercise regimenExercise);
        void Delete(int id);
        //List<RegimenExercise> GetExercisesByRegimenId(int id);
        void Update(RegimenExercise regimenExercise);
    }
}