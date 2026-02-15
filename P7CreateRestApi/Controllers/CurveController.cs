using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos.CurvePoint;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class CurveController : ControllerBase
{
    private readonly ICurvePointRepository _repository;

    public CurveController(ICurvePointRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CurvePointOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        var curvePoints = await _repository.GetAsync();

        var outputDtos = curvePoints.Select(c => new CurvePointOutputDto(c.Id, c.CurveId, c.Term, c.CurvePointValue))
            .ToList();

        return Ok(outputDtos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CurvePointOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CurvePointInputDto inputDto)
    {
        var curvePoint = new CurvePoint
        {
            CurveId = inputDto.CurveId,
            Term = inputDto.Term,
            CurvePointValue = inputDto.CurvePointValue
        };

        await _repository.AddAsync(curvePoint);

        var outputDto = new CurvePointOutputDto(curvePoint.Id, curvePoint.CurveId, curvePoint.Term,
            curvePoint.CurvePointValue);

        return CreatedAtAction(nameof(GetById), new { id = curvePoint.Id }, outputDto);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CurvePointOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var curvePoint = await _repository.GetByIdAsync(id);

        if (curvePoint == null)
        {
            return NotFound();
        }

        var outputDto = new CurvePointOutputDto(curvePoint.Id, curvePoint.CurveId, curvePoint.Term,
            curvePoint.CurvePointValue);

        return Ok(outputDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CurvePointOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, CurvePointInputDto inputDto)
    {
        var curvePoint = await _repository.GetByIdAsync(id);

        if (curvePoint == null)
        {
            return NotFound();
        }

        curvePoint.CurveId = inputDto.CurveId;
        curvePoint.Term = inputDto.Term;
        curvePoint.CurvePointValue = inputDto.CurvePointValue;

        await _repository.UpdateAsync();

        var outputDto = new CurvePointOutputDto(curvePoint.Id, curvePoint.CurveId, curvePoint.Term,
            curvePoint.CurvePointValue);

        return Ok(outputDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var curvePoint = await _repository.GetByIdAsync(id);

        if (curvePoint == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(curvePoint);

        return NoContent();
    }
}