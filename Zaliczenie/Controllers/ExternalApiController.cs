using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ExternalDataController : ControllerBase
{
    private readonly ExternalApiService _externalApiService;

    public ExternalDataController(ExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserDto>?>> GetUsers()
    {
        string url = "https://jsonplaceholder.typicode.com/users";
        var users = await _externalApiService.GetDataAsync<List<UserDto>>(url);

        if (users == null) return NotFound();

        return Ok(users);
    }
}
