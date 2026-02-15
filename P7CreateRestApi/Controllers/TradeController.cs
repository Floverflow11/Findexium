using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class TradeController : ControllerBase
{
    private readonly ITradeRepository _repository;

    public TradeController(ITradeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TradeOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        var trades = await _repository.GetAsync();

        var outputDtos = trades.Select(t => new TradeOutputDto(t.TradeId, t.Account, t.AccountType, t.BuyQuantity,
            t.SellQuantity, t.BuyPrice, t.SellPrice, t.TradeDate, t.TradeSecurity, t.TradeStatus, t.Trader,
            t.Benchmark, t.Book, t.CreationName, t.CreationDate, t.RevisionName, t.RevisionDate, t.DealName,
            t.DealType, t.SourceListId, t.Side)).ToList();

        return Ok(outputDtos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TradeOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] TradeInputDto inputDto)
    {
        var trade = new Trade
        {
            Account = inputDto.Account,
            AccountType = inputDto.AccountType,
            BuyQuantity = inputDto.BuyQuantity,
            SellQuantity = inputDto.SellQuantity,
            BuyPrice = inputDto.BuyPrice,
            SellPrice = inputDto.SellPrice,
            TradeDate = inputDto.TradeDate,
            TradeSecurity = inputDto.TradeSecurity,
            TradeStatus = inputDto.TradeStatus,
            Trader = inputDto.Trader,
            Benchmark = inputDto.Benchmark,
            Book = inputDto.Book,
            CreationName = inputDto.CreationName,
            RevisionName = inputDto.RevisionName,
            DealName = inputDto.DealName,
            DealType = inputDto.DealType,
            SourceListId = inputDto.SourceListId,
            Side = inputDto.Side
        };

        await _repository.AddAsync(trade);

        var outputDto = new TradeOutputDto(trade.TradeId, trade.Account, trade.AccountType, trade.BuyQuantity,
            trade.SellQuantity, trade.BuyPrice, trade.SellPrice, trade.TradeDate, trade.TradeSecurity,
            trade.TradeStatus, trade.Trader,
            trade.Benchmark, trade.Book, trade.CreationName, trade.CreationDate, trade.RevisionName, trade.RevisionDate,
            trade.DealName,
            trade.DealType, trade.SourceListId, trade.Side);

        return CreatedAtAction(nameof(GetById), new { id = trade.TradeId }, outputDto);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TradeOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var trade = await _repository.GetByIdAsync(id);

        if (trade == null)
        {
            return NotFound();
        }

        var outputDto = new TradeOutputDto(trade.TradeId, trade.Account, trade.AccountType, trade.BuyQuantity,
            trade.SellQuantity, trade.BuyPrice, trade.SellPrice, trade.TradeDate, trade.TradeSecurity,
            trade.TradeStatus, trade.Trader,
            trade.Benchmark, trade.Book, trade.CreationName, trade.CreationDate, trade.RevisionName, trade.RevisionDate,
            trade.DealName,
            trade.DealType, trade.SourceListId, trade.Side);

        return Ok(outputDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TradeOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, TradeInputDto inputDto)
    {
        var trade = await _repository.GetByIdAsync(id);

        if (trade == null)
        {
            return NotFound();
        }

        trade.Account = inputDto.Account;
        trade.AccountType = inputDto.AccountType;
        trade.BuyQuantity = inputDto.BuyQuantity;
        trade.SellQuantity = inputDto.SellQuantity;
        trade.BuyPrice = inputDto.BuyPrice;
        trade.SellPrice = inputDto.SellPrice;
        trade.TradeDate = inputDto.TradeDate;
        trade.TradeSecurity = inputDto.TradeSecurity;
        trade.TradeStatus = inputDto.TradeStatus;
        trade.Trader = inputDto.Trader;
        trade.Benchmark = inputDto.Benchmark;
        trade.Book = inputDto.Book;
        trade.CreationName = inputDto.CreationName;
        trade.RevisionName = inputDto.RevisionName;
        trade.DealName = inputDto.DealName;
        trade.DealType = inputDto.DealType;
        trade.SourceListId = inputDto.SourceListId;
        trade.Side = inputDto.Side;

        await _repository.UpdateAsync();

        var outputDto = new TradeOutputDto(trade.TradeId, trade.Account, trade.AccountType, trade.BuyQuantity,
            trade.SellQuantity, trade.BuyPrice, trade.SellPrice, trade.TradeDate, trade.TradeSecurity,
            trade.TradeStatus, trade.Trader,
            trade.Benchmark, trade.Book, trade.CreationName, trade.CreationDate, trade.RevisionName, trade.RevisionDate,
            trade.DealName,
            trade.DealType, trade.SourceListId, trade.Side);

        return Ok(outputDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var trade = await _repository.GetByIdAsync(id);

        if (trade == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(trade);

        return NoContent();
    }
}