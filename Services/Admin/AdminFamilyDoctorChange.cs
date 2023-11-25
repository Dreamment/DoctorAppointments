using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminFamilyDoctorChange
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminFamilyDoctorChange(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        internal async Task CreateFamilyDoctorChange(CreateFamilyDoctorChangeDto familyDoctorChangeDto, bool trackChanges)
        {
            var familyDoctorChange = _mapper.Map<FamilyDoctorChanges>(familyDoctorChangeDto);
            await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(familyDoctorChange);
            await _repositoryManager.SaveAsync();
        }

        internal async Task DeleteFamilyDoctorChange(int changeId, bool trackChanges)
        {
            var familyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.ChangeId == changeId, trackChanges);
            var familyDoctorChange = familyDoctorChanges.FirstOrDefault();
            await _repositoryManager.FamilyDoctorChanges.DeleteFamilyDoctorChangeAsync(familyDoctorChange);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChanges(bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetAllFamilyDoctorChangesAsync(trackChanges);

        internal async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCode(string doctorCode, bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.NewFamilyDoctorCode == doctorCode || fdc.PreviousFamilyDoctorCode == doctorCode, trackChanges);

        internal async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCId(ulong patientTCId, bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.PatientTCId == patientTCId, trackChanges);
    }
}
