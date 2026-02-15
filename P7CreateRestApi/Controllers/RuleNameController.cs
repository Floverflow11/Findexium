using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class RuleNameController : ControllerBase
{
    private readonly IRuleNameRepository _repository;

    public RuleNameController(IRuleNameRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RuleNameOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        var ruleNames = await _repository.GetAsync();

        var outputDtos = ruleNames.Select(r =>
            new RuleNameOutputDto(r.Id, r.Name, r.Description, r.Json, r.Template, r.SqlStr, r.SqlPart)).ToList();

        return Ok(outputDtos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RuleNameOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] RuleNameInputDto inputDto)
    {
        var ruleName = new RuleName
        {
            Name = inputDto.Name,
            Description = inputDto.Description,
            Json = inputDto.Json,
            Template = inputDto.Template,
            SqlStr = inputDto.SqlStr,
            SqlPart = inputDto.SqlPart
        };

        await _repository.AddAsync(ruleName);

        var outputDto = new RuleNameOutputDto(ruleName.Id, ruleName.Name, ruleName.Description, ruleName.Json,
            ruleName.Template, ruleName.SqlStr, ruleName.SqlPart);

        return CreatedAtAction(nameof(GetById), new { id = ruleName.Id }, outputDto);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RuleNameOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var ruleName = await _repository.GetByIdAsync(id);

        if (ruleName == null)
        {
            return NotFound();
        }

        var outputDto = new RuleNameOutputDto(ruleName.Id, ruleName.Name, ruleName.Description, ruleName.Json,
            ruleName.Template, ruleName.SqlStr, ruleName.SqlPart);

        return Ok(outputDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RuleNameOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, RuleNameInputDto inputDto)
    {
        var ruleName = await _repository.GetByIdAsync(id);

        if (ruleName == null)
        {
            return NotFound();
        }

        ruleName.Name = inputDto.Name;
        ruleName.Description = inputDto.Description;
        ruleName.Json = inputDto.Json;
        ruleName.Template = inputDto.Template;
        ruleName.SqlStr = inputDto.SqlStr;
        ruleName.SqlPart = inputDto.SqlPart;

        await _repository.UpdateAsync();

        var outputDto = new RuleNameOutputDto(ruleName.Id, ruleName.Name, ruleName.Description, ruleName.Json,
            ruleName.Template, ruleName.SqlStr, ruleName.SqlPart);

        return Ok(outputDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var ruleName = await _repository.GetByIdAsync(id);

        if (ruleName == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(ruleName);

        return NoContent();
    }
}