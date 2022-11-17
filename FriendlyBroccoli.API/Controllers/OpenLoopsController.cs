using FriendlyBroccoli.API.Contracts;
using FriendlyBroccoli.DataAccess;
using FriendlyBroccoli.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace FriendlyBroccoli.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OpenLoopsController : ControllerBase
{
    private readonly ILogger<OpenLoopsController> _logger;

    public OpenLoopsController(ILogger<OpenLoopsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
    public IActionResult Get()
    {
        var openLoops = Repository.Get();

        var response = new GetOpenLoopsResponse
        {
            OpenLoops = openLoops
            .Select(o => new GetOpenLoopDto()
            {
                Id = o.Id,
                Note = o.Note.Value,
                CreateDate = o.DateCreate
            })
            .ToArray()
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public IActionResult Create([FromBody] CreateOpenLoopRequest newOpenLoop)
    {
        var result = Repository.CreateNew(newOpenLoop.Note);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}

