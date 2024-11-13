using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly IFMPService _fmpService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo, 
            IStockRepository stockRepo, IFMPService fmpService, 
            UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _fmpService = fmpService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject) {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            var comments = await _commentRepo.GetAllAsync(queryObject);
            var commentDtos = comments.Select(x => x.ToCommentDto());
            return Ok(commentDtos);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto createCommentRequestDto) {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) { 
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null) { 
                    return BadRequest("Stock does not exists");
                } else { 
                    await _stockRepo.CreateAsync(stock);
                }
            }

            if (stock == null) return BadRequest("Stock not found");

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = createCommentRequestDto.ToComment(stock.Id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto) {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var commentModel = await _commentRepo.UpdateAsync(id, updateCommentRequestDto);

            if (commentModel == null) {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}