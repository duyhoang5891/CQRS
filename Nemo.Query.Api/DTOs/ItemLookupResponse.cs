using Nemo.Common.DTOs;
using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Api.DTOs
{
    public class ItemLookupResponse: BaseResponse
    {
        public List<Item> items { get; set; }
    }
}
