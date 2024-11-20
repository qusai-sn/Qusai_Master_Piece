using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<PaymentMethodDTO>> GetPaymentMethod(int userId)
    {
        var paymentMethod = await _paymentMethodService.GetUserPaymentMethodAsync(userId);
        if (paymentMethod == null) return NotFound();
        return Ok(paymentMethod);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdatePaymentMethod(int userId, PaymentMethodDTO paymentMethod)
    {
        var result = await _paymentMethodService.UpdatePaymentMethodAsync(userId, paymentMethod);
        if (!result) return BadRequest();
        return Ok(new { message = "Payment method updated successfully" });
    }
}