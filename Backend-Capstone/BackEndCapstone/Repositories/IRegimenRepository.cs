using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IRegimenRepository
    {
        void Add(Regimen regimen);
        void Delete(int id);
        List<Regimen> GetAll();
        Regimen GetRegimenById(int id);
        List<Regimen> GetPatientRegimensById(int id);
        List<Regimen> GetProviderRegimensById(int id);
        void Update(Regimen regimen);
    }
}