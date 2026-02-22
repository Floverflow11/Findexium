using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos.BidList;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class BidListController : ControllerBase
{
    private readonly IBidListRepository _repository;

    public BidListController(IBidListRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BidListOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetList()
    {
        var bidLists = await _repository.GetAsync();

        var outputDtos = bidLists.Select(b =>
                new BidListOutputDto(b.BidListId, b.Account, b.BidType, b.BidQuantity))
            .ToList();

        return Ok(outputDtos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BidListOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] BidListInputDto inputDto)
    {
        var bidList = new BidList
        {
            Account = inputDto.Account,
            BidType = inputDto.BidType,
            BidQuantity = inputDto.BidQuantity,
            Benchmark = string.Empty,
            Commentary = string.Empty,
            BidSecurity = string.Empty,
            BidStatus = string.Empty,
            Trader = string.Empty,
            Book = string.Empty,
            CreationName = string.Empty,
            RevisionName = string.Empty,
            DealName = string.Empty,
            DealType = string.Empty,
            SourceListId = string.Empty,
            Side = string.Empty
        };

        await _repository.AddAsync(bidList);

        var outputDto = new BidListOutputDto(bidList.BidListId, bidList.Account, bidList.BidType, bidList.BidQuantity);

        return CreatedAtAction(nameof(GetById), new { id = bidList.BidListId }, outputDto);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BidListOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var bidList = await _repository.GetByIdAsync(id);

        if (bidList == null)
        {
            return NotFound();
        }

        var outputDto = new BidListOutputDto(bidList.BidListId, bidList.Account, bidList.BidType, bidList.BidQuantity);

        return Ok(outputDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BidListOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, BidListInputDto inputDto)
    {
        var bidList = await _repository.GetByIdAsync(id);

        if (bidList == null)
        {
            return NotFound();
        }

        bidList.Account = inputDto.Account;
        bidList.BidType = inputDto.BidType;
        bidList.BidQuantity = inputDto.BidQuantity;

        await _repository.UpdateAsync();

        var outputDto = new BidListOutputDto(bidList.BidListId, bidList.Account, bidList.BidType, bidList.BidQuantity);

        return Ok(outputDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var bidList = await _repository.GetByIdAsync(id);

        if (bidList == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(bidList);

        return NoContent();
    }
}