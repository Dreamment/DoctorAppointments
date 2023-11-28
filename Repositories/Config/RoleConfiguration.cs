using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class  RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "359ce375-376c-42b6-b52e-a02b48e116a3",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "8203b0fd-a012-4f37-a2fc-30b977a94518",
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                },
                new IdentityRole
                {
                    Id = "8f416413-0a82-477c-a646-43f1ba04715b",
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                }
                                                                        );
        }
    }
}
