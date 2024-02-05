using Common.Dto;
using Microsoft.EntityFrameworkCore;


namespace Common.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<PaginatedResponse<T>> PaginateResult<T>(this IQueryable<T> query, PaginationRequest paginationRequest)
        {
            int allEntriesCount = await query.CountAsync();
            List<T> result = await query.Skip(paginationRequest.PageSize * (paginationRequest.PageNumber - 1))
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginatedResponse<T>(result, allEntriesCount);
        }

        public static async Task<PaginatedResponse<T>> PaginateResult<T, TT>(this IQueryable<T> query, PaginationRequest<TT> paginationRequest)
        {
            int allEntriesCount = await query.CountAsync();
            List<T> result = await query.Skip(paginationRequest.PageSize * (paginationRequest.PageNumber - 1))
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginatedResponse<T>(result, allEntriesCount);
        }
    }
}
