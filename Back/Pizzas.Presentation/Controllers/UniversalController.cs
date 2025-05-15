using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UniversalController : ControllerBase
{
    private readonly IMediator _mediator;

    public UniversalController(IMediator mediator) => _mediator = mediator;

    [HttpPost("batchexecute")]
    public async Task<IActionResult> Execute([FromBody] BatchRequestDto request)
    {
        var assembly = Assembly.Load("Pizzas.RequestPipeline");
        var results = new List<object>();

        foreach (var item in request.Requests)
        {
            var commandType = assembly.GetTypes().FirstOrDefault(t => t.Name == item.Action);
            if (commandType is null)
                return BadRequest(new { Error = $"Unknown action" });

            var commandJson = item.Parameters.ToString();
            var command = JsonConvert.DeserializeObject(commandJson, commandType);

            if (command is null)
                return BadRequest(new { Error = "Invalid parameters format" });

            var result = await _mediator.Send(command);
            results.Add(result);
        }

        return new JsonResult(results); 
    }

}