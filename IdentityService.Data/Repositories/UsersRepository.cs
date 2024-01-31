using Common.Dto;
using Common.Exceptions;
using IdentityService.Data.Contracts.DTO;
using IdentityService.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Repositories
{

    public interface IUsersRepository
    {
        public Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroupsAsync(PaginationRequest paginationRequest);
        public Task<PaginatedResponse<FindUserResponseDto>> FindUserAsync(PaginationRequest<FindRequestDto> paginationRequest);
        public void CreateNewGroupAsync(CreateNewGroupRequest dto);
        public void AddStudentsToGroup(AddStudentsToGroupRequest dto);

    }

    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;
        public UsersRepository(UsersContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroupsAsync(PaginationRequest paginationRequest)
        {
            IQueryable<GetAllGroupsResponseDto> request = _context.Groups.Join(_context.Users, g => g.TeacherId, u => u.Id, (g, u) => new
            {
                GroupImmutableId = g.ImmutableId,
                GroupName = g.Name,
                TeacherName = u.Name,
                TeacherSurname = u.Surname,
                DeletionDate = g.DeletionDate
            }).Where(g => g.DeletionDate == null).Select(r => new GetAllGroupsResponseDto
            {
                GroupName = r.GroupName,
                TeacherName = r.TeacherName,
                TeacherSurname = r.TeacherSurname,
                GroupImmutableId = r.GroupImmutableId
            });

            int allEntriesCount = await request.CountAsync();
            return new PaginatedResponse<GetAllGroupsResponseDto>(await request.ToListAsync(), allEntriesCount);
        }

        public async Task<PaginatedResponse<FindUserResponseDto>> FindUserAsync(PaginationRequest<FindRequestDto> paginationRequest)
        {
            var requestDto = paginationRequest.Request;
            string roleToBeFound = string.Empty;
            switch (requestDto.UserType)
            {
                case Common.Constants.UserTypes.Admin:
                    roleToBeFound = "Admin";
                    break;
                case Common.Constants.UserTypes.Teacher:
                    roleToBeFound = "Teacher";
                    break;
                case Common.Constants.UserTypes.Student:
                    roleToBeFound = "Student";
                    break;
                default:
                    throw new BadRequestException("Unsupported User type");
            }

            var joinedTables = _context.UserRoles.Join(_context.Users, ur => ur.UserId, u => u.Id, (ur, u) => new
            {
                User = u,
                RoleId = ur.RoleId,
            }).Join(_context.Roles, res => res.RoleId, r => r.Id, (res, r) => new
            {
                User = res.User,
                Role = r
            }).Where(res => res.Role.Name == roleToBeFound);

            if (!string.IsNullOrEmpty(requestDto.Name))
                joinedTables = joinedTables.Where(r => r.User.Name.Contains(requestDto.Name));

            if (!string.IsNullOrEmpty(requestDto.Surname))
                joinedTables = joinedTables = joinedTables.Where(r => r.User.Surname.Contains(requestDto.Surname));

            int allEntriesCount = await joinedTables.CountAsync();

            List<FindUserResponseDto> res = joinedTables.Select(res => new FindUserResponseDto
            {
                UserId = res.User.Id,
                Surname = res.User.Surname,
                Email = res.User.Email,
                Name = res.User.Name
            })
                .Skip(paginationRequest.PageSize * (paginationRequest.PageNumber - 1))
                .Take(paginationRequest.PageSize)
                .ToList();

            return new PaginatedResponse<FindUserResponseDto>(res, allEntriesCount);
        }

        public void AddStudentsToGroup(AddStudentsToGroupRequest dto)
        {
            if (!_context.Groups.Any(g => g.ImmutableId == dto.GroupImmutableId))
                throw new BadRequestException("Such group does not exist");

            DateTime creationDate = DateTime.Now.ToUniversalTime();

            // only add new students
            var existingStuds = _context.StudentsByGroups
                .Where(g => g.GroupImmutableId == dto.GroupImmutableId && dto.Students.Contains(g.StuedntId))
                .Select(res => res.StuedntId).ToList();
            var studentsToAdd = dto.Students.Where(s => !existingStuds.Contains(s)).ToList();

            foreach (var student in studentsToAdd)
            {
                _context.StudentsByGroups.Add(new StudentsByGroups
                {
                    ImmutableId = Guid.NewGuid(),
                    StuedntId = student,
                    GroupImmutableId = dto.GroupImmutableId,
                    Version = 1,
                    CreationDate = creationDate
                });
            }

            _context.SaveChanges();
        }

        public void CreateNewGroupAsync(CreateNewGroupRequest dto)
        {
            // check for teacher
            if (!_context.UserRoles.Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new
            {
                UserId = ur.UserId,
                RoleName = r.Name
            }).Any(res => res.UserId == dto.TeacherId && res.RoleName == "Teacher"))
                throw new BadRequestException("User was not a teacher or does not exist");

            _context.Groups.Add(new Groups
            {
                ImmutableId = Guid.NewGuid(),
                Name = dto.Name,
                TeacherId = dto.TeacherId,
                Version = 1,
                CreationDate = DateTime.Now.ToUniversalTime(),
            });

            _context.SaveChanges();
        }

    }
}
