using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_API_Test.Dtos.Comment;
using Web_API_Test.Extensions;
using Web_API_Test.Mappers;
using Web_API_Test.Models;
using Web_API_Test.Repositories.Interfaces;

namespace Web_API_Test.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (commentDto == null)
            {
                return BadRequest();
            }

            if (!await _stockRepository.StockExists(stockId))
            {
                return BadRequest($"Stock with ID {stockId} not found.");
            }

            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);


            var comment = commentDto.ToCommentFromCreate(stockId);
            comment.AppUserId = appUser.Id;

            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateAsync(id, updateCommentRequestDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }
            return NoContent();
        }
    }
}
