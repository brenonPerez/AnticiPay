using AnticiPay.Application.UseCases.Carts.AddInvoice;
using AnticiPay.Application.UseCases.Carts.GetCartOpen;
using AnticiPay.Application.UseCases.Carts.RemoveInvoice;
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
        [FromBody] RequestInvoiceCartJson request)
    {
        await useCase.Execute(request);

        return Created(string.Empty, string.Empty);
    }

    [HttpDelete("remove-invoice")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveInvoiceFromCart(
    [FromServices] IRemoveInvoiceFromCartUseCase useCase,
    [FromBody] RequestInvoiceCartJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpGet("cart")]
    [ProducesResponseType(typeof(ResponseCartJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartOpen(
        [FromServices] IGetCartOpenUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}
