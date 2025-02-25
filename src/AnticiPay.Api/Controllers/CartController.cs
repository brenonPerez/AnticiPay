using AnticiPay.Application.UseCases.Carts.AddInvoice;
using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnticiPay.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    [HttpPost("add-invoice")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddInvoiceToCart(
        [FromServices] IAddInvoiceToCartUseCase useCase,
        [FromBody] RequestAddInvoiceToCartJson request)
    {
        await useCase.Execute(request);

        return Created();
    }
}
