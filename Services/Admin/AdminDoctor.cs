using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminDoctor
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminDoctor(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        internal async Task<string> CreateDoctor(CreateDoctorDto doctorDto, bool trackChanges)
        {
            var doctor = _mapper.Map<Doctors>(doctorDto);
            doctor.DoctorCode = GenerateDoctorCode();
            while (await IsDoctorCodeExist(doctor.DoctorCode, trackChanges))
            {
                doctor.DoctorCode = GenerateDoctorCode();
            }
            await _repositoryManager.Doctor.CreateDoctorAsync(doctor);
            await _repositoryManager.SaveAsync();
            return doctor.DoctorCode;
        }

        internal async Task DeleteDoctor(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault();
            if (doctor == null)
                throw new Exception("Doctor not found");
            await _repositoryManager.Doctor.DeleteDoctorAsync(doctor);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<Doctors>> GetAllDoctors(bool trackChanges)
            => await _repositoryManager.Doctor.GetAllDoctorsAsync(trackChanges);

        internal async Task<Doctors> GetDoctorByDoctorCode(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            return doctors.FirstOrDefault();
        }

        internal async Task UpdateDoctor(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault();
            if (doctor == null)
                throw new Exception("Doctor not found");
            _mapper.Map(updateDoctorDto, doctor);
            await _repositoryManager.SaveAsync();
        }

        private string GenerateDoctorCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsDoctorCodeExist(string doctorCode, bool trackChanges)
        {
            var doctor = await _repositoryManager.Doctor
                .GetDoctorsByConditionAsync(d => d.DoctorCode == doctorCode, trackChanges);
            if (doctor == null)
                return false;
            return true;

        }
    }
}
