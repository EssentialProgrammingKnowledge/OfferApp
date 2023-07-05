using Microsoft.AspNetCore.Mvc;
using OfferApp.Core.DTO;
using OfferApp.Core.Services;

namespace OfferApp.Api.Controllers
{
    public class BidController : BaseController
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;            
        }

        [HttpGet]
        public async Task<IEnumerable<BidDto>> GetAll()
        {
            return await _bidService.GetAllBids();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BidDto?>> Get(int id)
        {
            var bid = await _bidService.GetBidById(id);
            return OkOrNotFound(bid);
        }

        [HttpPost]
        public async Task<ActionResult> Add(BidDto bidDto)
        {
            var dto = await _bidService.AddBid(bidDto);
            return CreatedAtAction(nameof(Get), new { dto.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, BidDto bidDto)
        {
            bidDto.Id = id;
            await _bidService.UpdateBid(bidDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bidService.DeleteBid(id);
            return NoContent();
        }
    }
}
