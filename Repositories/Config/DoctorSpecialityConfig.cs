using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class DoctorSpecialityConfig : IEntityTypeConfiguration<DoctorSpecialties>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialties> builder)
        {
            builder.HasData(
                new DoctorSpecialties { DoctorSpecialityId = 1, DoctorSpecialtyName = "Family Doctor" },
                new DoctorSpecialties { DoctorSpecialityId = 2, DoctorSpecialtyName = "Other"}
                );
        }
    }
}
