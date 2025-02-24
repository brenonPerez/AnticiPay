using AnticiPay.Application.UseCases.Companies.Register;
using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AnticiPay.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredCompanyJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
    [FromServices] IRegisterCompanyUseCase useCase,
    [FromBody] RequestRegisterCompanyJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
