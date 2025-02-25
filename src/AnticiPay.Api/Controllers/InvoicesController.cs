using AnticiPay.Application.UseCases.Invoices.Register;
using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnticiPay.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvoicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseInvoiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
    [FromServices] IRegisterInvoiceUseCase useCase,
    [FromBody] RequestInvoiceJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
