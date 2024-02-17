using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    public interface IStudentAnswersRepository
    {
        public Task<List<Guid>> GetCompletedTests(Guid userId);
        public Task SaveAnswers(SaveAnswersDto dto, Guid userId);
    }
    public class StudentAnswersRepository : IStudentAnswersRepository
    {
        private readonly AssignedTestsContext _context;
        public StudentAnswersRepository(AssignedTestsContext context)
        {
            _context = context;
        }
        public async Task<List<Guid>> GetCompletedTests(Guid userId)
        {
            return await _context.StudentAnswers
                .Where(a => a.UserId == userId)
                .Select(res => res.AssignedTestImmutableId)
                .ToListAsync();
        }

        public async Task SaveAnswers(SaveAnswersDto dto, Guid userId)
        {
            _context.StudentAnswers.Add(new StudentAnswer
            {
                Answers = dto.Tasks,
                AssignedTestImmutableId = dto.AssignedTestImmutableId,
                UserId = userId
            });

            await _context.SaveChangesAsync();
        }
    }
}
