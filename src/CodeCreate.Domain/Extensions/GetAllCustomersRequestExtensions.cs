using CodeCreate.App.Contracts.Requests;
using CodeCreate.Domain.Options;

namespace CodeCreate.Domain.Extensions
{
    public static class GetAllCustomersRequestExtensions
    {
        public static GetAllCustomersOptions ToGetAllCustomersOptions(this GetAllCustomersRequest getAllCustomersRequest)
        {
            return new GetAllCustomersOptions
            {
                Page = getAllCustomersRequest.Page,
                PageSize = getAllCustomersRequest.PageSize,
                SortField = getAllCustomersRequest.SortBy?.Trim('+', '-'),
                SortOrder = getAllCustomersRequest.SortBy is null ?
                    SortOrder.Unsorted :
                    getAllCustomersRequest.SortBy.StartsWith('-') ?
                        SortOrder.Descending :
                        SortOrder.Ascending,
            };
        }
    }
}
