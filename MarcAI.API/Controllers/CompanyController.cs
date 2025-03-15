using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarcAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém uma lista de empresas")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna as empresas baseada no filtro", typeof(CompanyDto))]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var companies = await _companyService.GetList();
                return Ok(companies);
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém uma  empresa")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a empresa", typeof(CompleteCompanyDto))]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var company = await _companyService.GetById(id);
                return Ok(company);
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


        [HttpGet("ownerUserId/{id}")]
        public async Task<IActionResult> GetOwnerUserId(Guid id)
        {
            try
            {
                var company = await _companyService.GetOwnerUserId(id);
                return Ok(company);
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
        [SwaggerOperation(Summary = "Cria uma nova empresa", Description = "Cria uma nova empresa com os dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a empresa criada.", typeof(CompanyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se os dados da empresa forem inválidos.")]
        public async Task<IActionResult> Create([FromBody] RegisterCompanyDto data)
        {
            try
            {
                var company = await _companyService.Create(data);
                return Ok(company);
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
        [SwaggerOperation(Summary = "Atualiza uma empresa", Description = "Atualiza uma empresa com os dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a empresa atualizada.", typeof(CompanyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se os dados da empresa forem inválidos.")]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyDto data)
        {
            try
            {
                var company = await _companyService.Update(data);
                return Ok(company);
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
        [SwaggerOperation(Summary = "Deleta uma empresa", Description = "Deleta uma empresa com o id fornecido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna true se a empresa foi deletada.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se o id da empresa for inválido.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _companyService.Delete(id);
                return Ok(deleted);
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
}
