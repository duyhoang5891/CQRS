using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Nemo.Cmd.Api.Commands;
using Nemo.Cmd.Api.DTOs;

namespace Nemo.Cmd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewItemController : ControllerBase
    {
        private readonly ILogger<NewItemController> _logger;

        private readonly ICommandDispatcher _commandDispatcher;

        public NewItemController(ILogger<NewItemController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult<CreateItemResponse>> NewItemAsync(AddItemCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;

                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new CreateItemResponse { Id = id, Message = "Create new item successfully!" });
            }
            catch (InvalidOperationException ex)
            {

                _logger.Log(LogLevel.Warning, ex, "Client made bad request!");

                return BadRequest(new CreateItemResponse { Message = "" });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to creat new post!";

                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new CreateItemResponse { Id = id, Message = SAFE_ERROR_MESSAGE });
            }
        }
    }
}
