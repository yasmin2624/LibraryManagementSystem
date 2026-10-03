using LibraryManagementSystem.Application.DTOs.Borrow;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowsController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        // GET: api/Borrows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowDto>>> GetAll()
        {
            var borrows = await _borrowService.GetAllAsync();

            return Ok(borrows);
        }

        // GET: api/Borrows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowDto>> GetById(int id)
        {
            var borrow = await _borrowService.GetByIdAsync(id);

            if (borrow == null)
                return NotFound();

            return Ok(borrow);
        }

        // POST: api/Borrows
        [HttpPost]
        public async Task<ActionResult<BorrowDto>> Create(CreateBorrowDto dto)
        {
            try
            {
                var borrow = await _borrowService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = borrow.BorrowID },
                    borrow);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Borrows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateBorrowDto dto)
        {
            var updated = await _borrowService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // PUT: api/Borrows/5/return
        [HttpPut("{borrowId}/return")]
        public async Task<IActionResult> ReturnBook(int borrowId)
        {
            var returned = await _borrowService.ReturnBookAsync(borrowId);

            if (!returned)
                return NotFound();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BorrowDto>>> Search(
    int? bookId,
    int? memberId,
    string? status)
        {
            var borrows = await _borrowService.SearchAsync(
                bookId,
                memberId,
                status);

            return Ok(borrows);
        }

        // GET: api/Borrows/member/5/history
        [HttpGet("member/{memberId}/history")]
        public async Task<ActionResult<IEnumerable<BorrowDto>>> GetBorrowingHistory(
            int memberId)
        {
            var history =
                await _borrowService.GetBorrowingHistoryAsync(memberId);

            return Ok(history);
        }
    }
}