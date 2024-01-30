using IdentityService.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace IdentityService.Data
{
    public class UsersContext : IdentityDbContext<TsUser, IdentityRole<Guid>, Guid>
    {
        public UsersContext(DbContextOptions<UsersContext> options) :
        base(options)
        { }

        public DbSet<Groups> Groups { get; set; }
        public DbSet<StudentsByGroups> StudentsByGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            PasswordHasher<TsUser> hasher = new PasswordHasher<TsUser>();

            Guid adminRoleId = Guid.Parse("02FE34E6-D974-439A-AD6B-032DDC1CDD47");
            Guid teacherRoleId = Guid.Parse("5C24F991-CBC7-43C8-BBC6-F51AB6DFBD22");
            Guid studentRoleId = Guid.Parse("3BAE9791-3BAF-4C97-9E69-AF551E65F309");
            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>[]{
                new IdentityRole<Guid>
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = teacherRoleId,
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                },
                new IdentityRole<Guid>
                {
                    Id = studentRoleId,
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            });

            Guid adminUserId = Guid.Parse("7317BB72-7732-48F5-A34F-6110D503578D");
            Guid teacherUserId = Guid.Parse("2A6EE01C-E688-456B-A469-AF63AEB0CE8E");
            Guid studentUserId = Guid.Parse("27C53444-1DC6-4CFF-B6AF-5FAF5A7C7722");
            TsUser adminIdentity = new TsUser
            {
                Id = adminUserId,
                UserName = "DefaultAdmin",
                NormalizedUserName = "DEFAULTADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser teacherIdentity = new TsUser
            {
                Id = teacherUserId,
                UserName = "DefaultTeach",
                NormalizedUserName = "DEFAULTTEACH",
                Email = "teach@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser studentIdentity = new TsUser
            {
                Id = studentUserId,
                UserName = "DefaultStudent",
                NormalizedUserName = "DEFAULTSTUDENT",
                Email = "stud@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };


            adminIdentity.PasswordHash = hasher.HashPassword(adminIdentity, "admin");
            teacherIdentity.PasswordHash = hasher.HashPassword(teacherIdentity, "teach");
            studentIdentity.PasswordHash = hasher.HashPassword(studentIdentity, "stud");

            builder.Entity<TsUser>().HasData(new TsUser[]
            {
                adminIdentity,
                teacherIdentity,
                studentIdentity
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>[]
            {
                new IdentityUserRole<Guid>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = teacherUserId,
                    RoleId = teacherRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = studentUserId,
                    RoleId = studentRoleId
                }
            });
        }
    }
}
