using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public class UsersContext : IdentityDbContext<IdentityUser>
    {
        public UsersContext(DbContextOptions<UsersContext> options) :
        base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();

            const string adminRoleId = "02FE34E6-D974-439A-AD6B-032DDC1CDD47";
            const string teacherRoleId = "5C24F991-CBC7-43C8-BBC6-F51AB6DFBD22";
            const string studentRoleId = "3BAE9791-3BAF-4C97-9E69-AF551E65F309";
            builder.Entity<IdentityRole>().HasData(new IdentityRole[]{
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = teacherRoleId,
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                },
                new IdentityRole
                {
                    Id = studentRoleId,
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            });

            const string adminUserId = "7317BB72-7732-48F5-A34F-6110D503578D";
            const string teacherUserId = "2A6EE01C-E688-456B-A469-AF63AEB0CE8E";
            const string studentUserId = "27C53444-1DC6-4CFF-B6AF-5FAF5A7C7722";
            IdentityUser adminIdentity = new IdentityUser
            {
                Id = adminUserId,
                UserName = "DefaultAdmin",
                NormalizedUserName = "DEFAULTADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                //PasswordHash = hasher.HashPassword(null, "admin")
            };
            IdentityUser teacherIdentity = new IdentityUser
            {
                Id = teacherUserId,
                UserName = "DefaultTeach",
                NormalizedUserName = "DEFAULTTEACH",
                Email = "teach@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                //PasswordHash = hasher.HashPassword(null, "teach")
            };
            IdentityUser studentIdentity = new IdentityUser
            {
                Id = studentUserId,
                UserName = "DefaultStudent",
                NormalizedUserName = "DEFAULTSTUDENT",
                Email = "stud@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                //PasswordHash = hasher.HashPassword(null, "stud"),
            };

            adminIdentity.PasswordHash = hasher.HashPassword(adminIdentity, "admin");
            teacherIdentity.PasswordHash = hasher.HashPassword(teacherIdentity, "teach");
            studentIdentity.PasswordHash = hasher.HashPassword(studentIdentity, "stud");

            builder.Entity<IdentityUser>().HasData(new IdentityUser[]
            {
                adminIdentity,
                teacherIdentity,
                studentIdentity
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = teacherUserId,
                    RoleId = teacherRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = studentUserId,
                    RoleId = studentRoleId
                }
            });
        }
    }
}
