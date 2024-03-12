using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    public interface IReviewRepository
    {
        public Task SaveReview(AssignedTestReviewSaveRequestDto requestDto);
    }
    public class ReviewRepository: IReviewRepository
    {
        private readonly AssignedTestsContext _context;
        public ReviewRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        public async Task SaveReview(AssignedTestReviewSaveRequestDto requestDto)
        {
            AssignedTestReview review = new AssignedTestReview
            {
                AssignedTestImmutableId = requestDto.AssignedTestImmutableId,
                Comments = requestDto.Comments,
                FinalComment = requestDto.FinalComment,
                StudentAnswerId = requestDto.StudentAnswerId
            };

            await _context.SaveChangesAsync();
        }
    }
}
