using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IRegimenAssignmentRepository
    {
        void Add(RegimenAssignment regimenAssignment);
        void Delete(int id);
        //List<RegimenAssignment> GetAllregimens();
        void Update(RegimenAssignment regimenAssignment);
    }
}