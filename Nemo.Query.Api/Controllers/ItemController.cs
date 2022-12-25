using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nemo.Query.Api.DTOs;
using Nemo.Query.Api.Queries;
using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IQueryDispatcher<Item> _queryDispatcher;

        public ItemController(ILogger<ItemController> logger, IQueryDispatcher<Item> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<ItemLookupResponse>> GetAllItemAsync()
        {
            var items = await _queryDispatcher.SendAsync(new FindAllItemQuery());

            if(items == null)
                return NoContent();

            return Ok(new ItemLookupResponse
            {
                items = items
            });
        }
    }
}
