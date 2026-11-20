using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("visitors")]
public sealed class VisitorController(IVisitorService visitorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VisitorResponse>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await visitorService.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    [ProducesResponseType<VisitorResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisitorResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var visitor = await visitorService.GetByIdAsync(id, cancellationToken);
        return visitor is null ? NotFound() : Ok(visitor);
    }

    [HttpPost]
    [ProducesResponseType<VisitorResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VisitorResponse>> Create(
        VisitorRequest request,
        CancellationToken cancellationToken)
    {
        var visitor = await visitorService.CreateAsync(request, cancellationToken);
        return visitor is null ? BadRequest("The specified user does not exist.") :
            CreatedAtAction(nameof(GetById), new { id = visitor.Id }, visitor);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        VisitorRequest request,
        CancellationToken cancellationToken)
    {
        return await visitorService.UpdateAsync(id, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return await visitorService.DeleteAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
