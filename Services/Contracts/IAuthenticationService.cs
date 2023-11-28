using Entities.DataTransferObjects.Auth;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);

        Task<IdentityResult> RegisterDoctor(UserForDoctorRegistrationDto userForDoctorRegistrationDto, Doctors doctor);

        Task<IdentityResult> RegisterPatient(UserForPatientRegistrationDto userForPatientRegistrationDto, Patients patient);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto);

        Task<string> CreateToken();
    }
}
