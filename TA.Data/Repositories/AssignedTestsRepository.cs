﻿using Common.Constants;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    public interface IAssignedTestsRepository
    {
        public Task AssignTest(AssignTestRequestDto dto, Guid userId);
        public Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(List<Guid>? groups, Guid userId, PaginationRequest request);
    }

    public class AssignedTestsRepository : IAssignedTestsRepository
    {
        private readonly AssignedTestsContext _context;
        public AssignedTestsRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        public async Task AssignTest(AssignTestRequestDto dto, Guid userId)
        {
            DateTime currentTime = DateTime.Now.ToUniversalTime();

            _context.AssignedTests.Add(new AssignedTests
            {
                AssignedTime = currentTime,
                CreationDate = currentTime,
                GroupImmutableId = dto.GroupImmutableId,
                ImmutableId = Guid.NewGuid(),
                Version = 1,
                State = AssignedTestState.Assigned,
                StudentImmutableId = dto.StudentImmutableId,
                TestDescriptionId = dto.TestDescriptionId,
                AssignedBy = userId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(List<Guid>? groups, Guid userId, PaginationRequest request)
        {
            IQueryable<AssignedTests> resultQuery = _context.AssignedTests.Where(t => t.DeletionDate == null
            && t.StudentImmutableId == userId || t.AssignedBy == userId);

            if (groups != null && groups.Any())
            {
                IQueryable<AssignedTests> addition = _context.AssignedTests.Where(t => t.DeletionDate == null && t.GroupImmutableId.HasValue
            && groups.Contains(t.GroupImmutableId.Value));
                resultQuery = resultQuery.Union(addition);
            }

            List<AssisgnedTestResponseDto> res = await resultQuery.Select(r => new AssisgnedTestResponseDto
            {
                AssignedTime = r.AssignedTime,
                DueTo = r.DueTo,
                TestDescriptionId = r.TestDescriptionId,
                TeacherId = r.AssignedBy
            }).Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            int itemsCount = await resultQuery.CountAsync();

            return new PaginatedResponse<AssisgnedTestResponseDto>(res, itemsCount);
        }
    }
}
