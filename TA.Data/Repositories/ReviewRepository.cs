using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    /// <summary>
    /// Respository for working with Review table
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Save review(comments) of a student's answer
        /// </summary>
        /// <param name="requestDto">comments with metadata</param>
        /// <returns></returns>
        public Task SaveReview(AssignedTestReviewSaveRequestDto requestDto);
    }
    public class ReviewRepository: IReviewRepository
    {
        private readonly AssignedTestsContext _context;
        public ReviewRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task SaveReview(AssignedTestReviewSaveRequestDto requestDto)
        {
            AssignedTestReview review = new AssignedTestReview
            {
                AssignedTestImmutableId = requestDto.AssignedTestImmutableId,
                Comments = requestDto.Comments,
                FinalComment = requestDto.FinalComment,
                StudentAnswerId = requestDto.StudentAnswerId
            };
            _context.Review.Add(review);

            await _context.SaveChangesAsync();
        }
    }
}
