using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IPatientAssignmentRepository
    {
        void Add(PatientAssignment patientAssignment);
        void Delete(int id);
        List<PatientAssignment> GetPatientAssignmentByProviderId(int id);
        List<PatientAssignment> GetPatientAssignments();
        void Update(PatientAssignment patientAssignment);
    }
}