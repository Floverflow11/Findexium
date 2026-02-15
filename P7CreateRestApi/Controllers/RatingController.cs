using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos.Rating;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
    private readonly IRatingRepository _repository;

    public RatingController(IRatingRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RatingOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        var ratings = await _repository.GetAsync();

        var outputDtos = ratings.Select(r =>
                new RatingOutputDto(r.Id, r.MoodysRating, r.SandPRating, r.FitchRating, r.OrderNumber))
            .ToList();

        return Ok(outputDtos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RatingOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] RatingInputDto inputDto)
    {
        var rating = new Rating
        {
            MoodysRating = inputDto.MoodysRating,
            SandPRating = inputDto.SandPRating,
            FitchRating = inputDto.FitchRating,
            OrderNumber = inputDto.OrderNumber
        };

        await _repository.AddAsync(rating);

        var outputDto = new RatingOutputDto(rating.Id, rating.MoodysRating, rating.SandPRating, rating.FitchRating,
            rating.OrderNumber);

        return CreatedAtAction(nameof(GetById), new { id = rating.Id }, outputDto);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RatingOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var rating = await _repository.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        var outputDto = new RatingOutputDto(rating.Id, rating.MoodysRating, rating.SandPRating, rating.FitchRating,
            rating.OrderNumber);

        return Ok(outputDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RatingOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, RatingInputDto inputDto)
    {
        var rating = await _repository.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        rating.MoodysRating = inputDto.MoodysRating;
        rating.SandPRating = inputDto.SandPRating;
        rating.FitchRating = inputDto.FitchRating;
        rating.OrderNumber = inputDto.OrderNumber;

        await _repository.UpdateAsync();

        var outputDto = new RatingOutputDto(rating.Id, rating.MoodysRating, rating.SandPRating, rating.FitchRating,
            rating.OrderNumber);

        return Ok(outputDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var rating = await _repository.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(rating);

        return NoContent();
    }
}