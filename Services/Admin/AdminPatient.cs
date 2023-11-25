using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminPatient
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminPatient(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        internal async Task CreatePatientAsync(CreatePatientDto patientDto, bool trackChanges)
        {
            var patient = _mapper.Map<Patients>(patientDto);
            await _repositoryManager.Patient.CreatePatientAsync(patient);
            await _repositoryManager.SaveAsync();
        }

        internal async Task DeletePatientAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault();
            await _repositoryManager.Patient.DeletePatientAsync(patient);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges)
            => await _repositoryManager.Patient.GetAllPatientsAsync(trackChanges);

        internal async Task<Patients> GetPatientByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            return patients.FirstOrDefault();
        }

        internal async Task UpdatePatientAsync(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault();
            _mapper.Map(updatePatientDto, patient);
            await _repositoryManager.SaveAsync();
        }
    }
}
