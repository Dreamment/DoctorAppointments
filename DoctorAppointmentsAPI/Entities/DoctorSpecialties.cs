using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentsAPI.Entities
{
    public class DoctorSpecialties
    {
        [Key]
        public int DoctorSpecialityId { get; set; }

        [Required]
        public string DoctorSpecialtyName { get; set; }
    }
}
