using LibraryManagementSystem.Application.DTOs.Payment;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();

            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Create(CreatePaymentDto dto)
        {
            try
            {
                var payment = await _paymentService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = payment.PaymentID },
                    payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdatePaymentDto dto)
        {
            var updated = await _paymentService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // GET: api/Payments/borrow/5
        [HttpGet("borrow/{borrowId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetByBorrowId(
            int borrowId)
        {
            var payments = await _paymentService.GetByBorrowIdAsync(borrowId);

            return Ok(payments);
        }
    }
}