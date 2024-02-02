using IdentityService.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            #region defaults
            Guid adminUserId = Guid.Parse("7317BB72-7732-48F5-A34F-6110D503578D");
            Guid teacherUserId = Guid.Parse("2A6EE01C-E688-456B-A469-AF63AEB0CE8E");
            Guid studentUserId = Guid.Parse("27C53444-1DC6-4CFF-B6AF-5FAF5A7C7722");
            TsUser adminIdentity = new TsUser
            {
                Name = "Defaut",
                Surname = "Admin",
                Id = adminUserId,
                UserName = "DefaultAdmin",
                NormalizedUserName = "DEFAULTADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser teacherIdentity = new TsUser
            {
                Name = "Defaut",
                Surname = "Teacher",
                Id = teacherUserId,
                UserName = "DefaultTeach",
                NormalizedUserName = "DEFAULTTEACH",
                Email = "teach@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser studentIdentity = new TsUser
            {
                Name = "Defaut",
                Surname = "Student",
                Id = studentUserId,
                UserName = "DefaultStudent",
                NormalizedUserName = "DEFAULTSTUDENT",
                Email = "stud@gmail.com",
                NormalizedEmail = "TEACH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            adminIdentity.PasswordHash = hasher.HashPassword(adminIdentity, "4100");
            teacherIdentity.PasswordHash = hasher.HashPassword(teacherIdentity, "4100");
            studentIdentity.PasswordHash = hasher.HashPassword(studentIdentity, "4100");
            #endregion

            #region teachers
            Guid fstTeachUserId = Guid.Parse("{5A0D3404-1038-474C-A5BD-BA2177499BC1}");
            Guid secTeachUserId = Guid.Parse("{8B6C3B04-C5CC-41E1-BDEF-87F459780D7A}");
            Guid thirdTeachUserId = Guid.Parse("{E1CD61DD-29F1-4904-BEBF-FC5FE1FFC931}");
            TsUser fstTeachIdentity = new TsUser
            {
                Name = "Erich",
                Surname = "Hartmann",
                Id = fstTeachUserId,
                UserName = "eh",
                NormalizedUserName = "EH",
                Email = "eh@gmail.com",
                NormalizedEmail = "EH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser secTeachIdentity = new TsUser
            {
                Name = "Gigachad",
                Surname = "Teacher",
                Id = secTeachUserId,
                UserName = "gigateach",
                NormalizedUserName = "GIGATEACH",
                Email = "gigateach@gmail.com",
                NormalizedEmail = "GIGATEACH@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser thirdTeachIdentity = new TsUser
            {
                Name = "Sigma",
                Surname = "Teacher",
                Id = thirdTeachUserId,
                UserName = "sigt",
                NormalizedUserName = "SIGT",
                Email = "sigt@gmail.com",
                NormalizedEmail = "SIGT@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            fstTeachIdentity.PasswordHash = hasher.HashPassword(fstTeachIdentity, "4100");
            secTeachIdentity.PasswordHash = hasher.HashPassword(secTeachIdentity, "4100");
            thirdTeachIdentity.PasswordHash = hasher.HashPassword(thirdTeachIdentity, "4100");
            #endregion

            #region students
            Guid fstStudentUserId = Guid.Parse("{FDE274EE-0A4A-4B53-93AF-68FEDCFC60DF}");
            Guid secStudentUserId = Guid.Parse("{1C1FD164-C4B2-4E96-9B4A-625EB6454584}");
            Guid thirdStudentUserId = Guid.Parse("{882339C7-1D54-4EA5-B6F0-D247A21179D8}");
            Guid fourthStudentUserId = Guid.Parse("{7158DEDD-A907-4D88-9DCD-1683464E4F85}");
            Guid fifthStudentUserId = Guid.Parse("{A96FD176-89D4-4C53-98A6-A9103723889B}");
            TsUser fstStudentIdentity = new TsUser
            {
                Name = "Ivan",
                Surname = "Eblanov",
                Id = fstStudentUserId,
                UserName = "ieov",
                NormalizedUserName = "IEOV",
                Email = "ieov@gmail.com",
                NormalizedEmail = "IEOV@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser secStudentIdentity = new TsUser
            {
                Name = "Anton",
                Surname = "Condom",
                Id = secStudentUserId,
                UserName = "ac",
                NormalizedUserName = "AC",
                Email = "ac@gmail.com",
                NormalizedEmail = "AC@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser thirdStudentIdentity = new TsUser
            {
                Name = "Temp",
                Surname = "Name",
                Id = thirdStudentUserId,
                UserName = "tn",
                NormalizedUserName = "TN",
                Email = "tn@gmail.com",
                NormalizedEmail = "TN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser fourthStudentIdentity = new TsUser
            {
                Name = "Fourth",
                Surname = "Student",
                Id = fourthStudentUserId,
                UserName = "frs",
                NormalizedUserName = "FRS",
                Email = "frs@gmail.com",
                NormalizedEmail = "FRS@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            TsUser fifthStudentIdentity = new TsUser
            {
                Name = "Fifth",
                Surname = "Sudent",
                Id = fifthStudentUserId,
                UserName = "ffs",
                NormalizedUserName = "FFS",
                Email = "ffs@gmail.com",
                NormalizedEmail = "FFS@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            fstStudentIdentity.PasswordHash = hasher.HashPassword(fstStudentIdentity, "4100");
            secStudentIdentity.PasswordHash = hasher.HashPassword(secStudentIdentity, "4100");
            thirdStudentIdentity.PasswordHash = hasher.HashPassword(thirdStudentIdentity, "4100");
            fourthStudentIdentity.PasswordHash = hasher.HashPassword(fourthStudentIdentity, "4100");
            fifthStudentIdentity.PasswordHash = hasher.HashPassword(fifthStudentIdentity, "4100");
            #endregion

            builder.Entity<TsUser>().HasData(new TsUser[]
            {
                adminIdentity,
                teacherIdentity,
                studentIdentity,
                fstTeachIdentity,
                secTeachIdentity,
                thirdTeachIdentity,
                fstStudentIdentity,
                secStudentIdentity,
                thirdStudentIdentity,
                fourthStudentIdentity,
                fifthStudentIdentity,
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
                },
                new IdentityUserRole<Guid>
                {
                    UserId = fstTeachUserId,
                    RoleId = teacherRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = secTeachUserId,
                    RoleId = teacherRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = thirdTeachUserId,
                    RoleId = teacherRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = fstStudentUserId,
                    RoleId = studentRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = secStudentUserId,
                    RoleId = studentRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = thirdStudentUserId,
                    RoleId = studentRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = fourthStudentUserId,
                    RoleId = studentRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = fifthStudentUserId,
                    RoleId = studentRoleId
                }
            });
        }
    }
}
