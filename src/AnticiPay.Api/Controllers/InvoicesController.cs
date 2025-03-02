using AnticiPay.Application.UseCases.Invoices.GetAll;
using AnticiPay.Application.UseCases.Invoices.GetAllNotInCart;
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

    [HttpGet("not-in-cart")]
    [ProducesResponseType(typeof(ResponseInvoicesSimulatedJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllNotInCart(
    [FromServices] IGetAllNotInCartInvoicesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Invoices.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ResponseInvoicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
    [FromServices] IGetAllInvoicesUseCase useCase,
    [FromQuery] string? number = null,
    [FromQuery] decimal? amount = null,
    [FromQuery] DateTime? dueDate = null,
    [FromQuery] int pageIndex = 0,
    [FromQuery] int pageSize = 10)
    {
        var response = await useCase.Execute(new RequestFilterInvoicesJson
        {
            Number = number,
            Amount = amount,
            DueDate = dueDate,
            PageIndex = pageIndex,
            PageSize = pageSize
        });

        if (response.Invoices.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }
}
