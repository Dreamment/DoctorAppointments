using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentsDomain.Entities
{
    public class DoctorSpecialties
    {
        [Key]
        public int DoctorSpecialtyId { get; set; }

        [Required]
        public string DoctorSpecialtyName { get; set; }
    }
}
