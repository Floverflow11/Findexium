using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos.BidList;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Tests;

public class BidListControllerTests
{
    private readonly Mock<IBidListRepository> _mockRepository;
    private readonly BidListController _controller;

    public BidListControllerTests()
    {
        _mockRepository = new Mock<IBidListRepository>();
        _controller = new BidListController(_mockRepository.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResultWithBidLists()
    {
        var input = new List<BidList>
        {
            new()
            {
                BidListId = 1,
                Account = "Account1",
                BidType = "BidType1",
                BidQuantity = 100,
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
            },
            new()
            {
                BidListId = 2,
                Account = "Account2",
                BidType = "BidType2",
                BidQuantity = 200,
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
            }
        };

        _mockRepository.Setup(r => r.GetAsync()).ReturnsAsync(input);

        var result = await _controller.GetList();

        var ok = Assert.IsType<OkObjectResult>(result);
        var output = Assert.IsType<List<BidListOutputDto>>(ok.Value);

        Assert.Equal(input.Count, output.Count);
        Assert.Equal(input[0].BidListId, output[0].Id);
        Assert.Equal(input[0].Account, output[0].Account);
        Assert.Equal(input[0].BidType, output[0].BidType);
        Assert.Equal(input[0].BidQuantity, output[0].BidQuantity);
    }

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenNoBidListsExist()
    {
        var input = new List<BidList>();

        _mockRepository.Setup(r => r.GetAsync()).ReturnsAsync(input);

        var result = await _controller.GetList();

        var ok = Assert.IsType<OkObjectResult>(result);
        var output = Assert.IsType<List<BidListOutputDto>>(ok.Value);

        Assert.Empty(output);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithValidInput()
    {
        const int id = 1;

        var input = new BidListInputDto { Account = "Account1", BidType = "BidType1", BidQuantity = 100 };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<BidList>())).Callback<BidList>(b => b.BidListId = id)
            .Returns(Task.CompletedTask);

        var result = await _controller.Create(input);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetById), created.ActionName);
        Assert.Equal(id, created.RouteValues?[nameof(id)]);

        var output = Assert.IsType<BidListOutputDto>(created.Value);
        Assert.Equal(input.Account, output.Account);
        Assert.Equal(input.BidType, output.BidType);
        Assert.Equal(input.BidQuantity, output.BidQuantity);

        _mockRepository.Verify(r => r.AddAsync(It.IsAny<BidList>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ReturnsOkResultWithBidList_WhenBidListExists()
    {
        var input = new BidList
        {
            BidListId = 1,
            Account = "Account1",
            BidType = "BidType1",
            BidQuantity = 100,
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

        _mockRepository.Setup(r => r.GetByIdAsync(input.BidListId)).ReturnsAsync(input);

        var result = await _controller.GetById(input.BidListId);

        var ok = Assert.IsType<OkObjectResult>(result);
        var output = Assert.IsType<BidListOutputDto>(ok.Value);

        Assert.Equal(input.BidListId, output.Id);
        Assert.Equal(input.Account, output.Account);
        Assert.Equal(input.BidType, output.BidType);
        Assert.Equal(input.BidQuantity, output.BidQuantity);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenBidListDoesNotExist()
    {
        const int id = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((BidList?)null);

        var result = await _controller.GetById(id);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WhenBidListExists()
    {
        const int id = 1;

        var bidList = new BidList
        {
            BidListId = id,
            Account = "Account1",
            BidType = "BidType1",
            BidQuantity = 100,
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

        var input = new BidListInputDto { Account = "NewAccount1", BidType = "NewBidType1", BidQuantity = 1000 };

        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(bidList);
        _mockRepository.Setup(r => r.UpdateAsync()).ReturnsAsync(1);

        var result = await _controller.Update(id, input);

        var ok = Assert.IsType<OkObjectResult>(result);
        var output = Assert.IsType<BidListOutputDto>(ok.Value);

        Assert.Equal(id, output.Id);
        Assert.Equal(input.Account, output.Account);
        Assert.Equal(input.BidType, output.BidType);
        Assert.Equal(input.BidQuantity, output.BidQuantity);

        _mockRepository.Verify(r => r.UpdateAsync(), Times.Once);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenBidListDoesNotExist()
    {
        const int id = -1;
        var input = new BidListInputDto { Account = "NewAccount1", BidType = "NewBidType1", BidQuantity = 1000 };

        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((BidList?)null);

        var result = await _controller.Update(id, input);

        Assert.IsType<NotFoundResult>(result);
        _mockRepository.Verify(r => r.UpdateAsync(), Times.Never);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenBidListExists()
    {
        var existing = new BidList
        {
            BidListId = 1,
            Account = "Account1",
            BidType = "BidType1",
            BidQuantity = 100,
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

        _mockRepository.Setup(r => r.GetByIdAsync(existing.BidListId)).ReturnsAsync(existing);
        _mockRepository.Setup(r => r.DeleteAsync(existing)).Returns(Task.CompletedTask);

        var result = await _controller.Delete(existing.BidListId);

        Assert.IsType<NoContentResult>(result);
        _mockRepository.Verify(r => r.DeleteAsync(existing), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenBidListDoesNotExist()
    {
        const int id = -1;
        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((BidList?)null);

        var result = await _controller.Delete(id);

        Assert.IsType<NotFoundResult>(result);
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<BidList>()), Times.Never());
    }
}