using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication4.Service;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ReadersController(IDbService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReadersDetails()
    {
        return Ok(await service.GetAllReadersAsync());
        
    }

    [HttpPost]
    public async Task<IActionResult> AddReader([FromBody] CreateReaderDto createReaderDto)
    {
        try
        {
            var reader = await service.CreateReaderAsync(createReaderDto);
            return Ok(reader);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader([FromRoute] int id)
    {
        try
        {
            await service.DeleteReaderAsync(id);
            return Ok($"Usunąłem studenta o id {id}");
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    
}