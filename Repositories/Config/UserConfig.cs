using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = "18dd2552-17ae-4bdb-9621-6061e1308bd4",
                    Name = "admin",
                    Surname = "admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = "AQAAAAEAACcQAAAAEK1UJlTVFUSnIG7wzErpGrmGlG8/+UwZiCkLEhu8cP+XpYYMznxyZc2sVPsFN3Aytw==", // admin
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    PhoneNumber = "admin",
                    LockoutEnabled = true
                });
        }
    }
}
