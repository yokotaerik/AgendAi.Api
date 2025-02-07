using MarcAI.Application.Dtos.Costumers;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarcAI.API.Controllers;

[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém os dados do usuário")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo cliente", Description = "Cria um novo cliente com os dados fornecidos.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o cliente criado.", typeof(CostumerDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Se os dados do cliente forem inválidos.")]
    public async Task<IActionResult> Create([FromBody] RegisterCostumerDto data)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customer = await _customerService.Create(data);
            return Ok(customer);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
