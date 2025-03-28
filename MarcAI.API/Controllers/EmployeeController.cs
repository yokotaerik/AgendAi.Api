﻿using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MarcAI.Application.Dtos.Filters;

namespace MarcAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém um funcionário pelo ID")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var employee = await _employeeService.GetById(id);
            return Ok(employee);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByUserId(string email)
    {
        try
        {
            var employee = await _employeeService.GetByEmail(email);
            return Ok(employee);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Obtém uma lista de funcionários")]
    public async Task<IActionResult> GetList([FromQuery] EmployeeFilterDto filter)
    {
        try
        {
            var employees = await _employeeService.GetList(filter);
            return Ok(employees);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo funcionário")]
    public async Task<IActionResult> Create([FromBody] RegisterEmployeeDto data)
    {
        try
        {
            await _employeeService.Create(data);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Atualiza um funcionário")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeDto data)
    {
        try
        {
            await _employeeService.Update(data);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _employeeService.Delete(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
