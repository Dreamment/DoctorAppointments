using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminDoctorSpeciality
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminDoctorSpeciality(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        internal async Task CreateDoctorSpecialtyAsync(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges)
        {
            var doctorSpecialty = _mapper.Map<DoctorSpecialties>(doctorSpecialtyDto);
            await _repositoryManager.DoctorSpeciality.CreateDoctorSpecialtyAsync(doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }

        internal async Task DeleteDoctorSpecialtyAsync(int doctorSpecialtyId, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            var doctorSpecialty = doctorSpecialties.FirstOrDefault();
            if (doctorSpecialty == null)
                throw new Exception("DoctorSpecialty not found");
            await _repositoryManager.DoctorSpeciality.DeleteDoctorSpecialtyAsync(doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges)
            => await _repositoryManager.DoctorSpeciality.GetAllDoctorSpecialtiesAsync(trackChanges);

        internal async Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            return doctorSpecialties.FirstOrDefault();
        }

        internal async Task UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                               ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            var doctorSpecialty = doctorSpecialties.FirstOrDefault();
            if (doctorSpecialty == null)
                throw new Exception("DoctorSpecialty not found");
            _mapper.Map(updateDoctorSpecialtyDto, doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }
    }
}
