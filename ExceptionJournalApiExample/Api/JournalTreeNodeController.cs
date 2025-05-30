using Asp.Versioning;
using ExceptionJournalApiExample.App.Middlewares;
using ExceptionJournalApiExample.Domain.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionJournalApiExample.Api;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/journal/node")]
public class JournalTreeNodeController(IJournalRepository repo) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromQuery] string nodeName)
    {
        var creationResult = await repo.TryCreateAsync(nodeName);
        return Ok(creationResult);
    }

    [HttpPost("attach")]
    public async Task<IActionResult> Attach([FromQuery] string parentNodeId, [FromQuery] string nodeName)
    {
        if (!Guid.TryParse(parentNodeId, out var parentNodeGuid))
            throw new SecureException("Guid is not a valid node id");

        var attachmentResult = await repo.TryAttachAsync(parentNodeGuid, nodeName);
        return Ok(attachmentResult);
    }

    [HttpPost("rename")]
    public async Task<IActionResult> Rename([FromQuery] string treeName, [FromQuery] string nodeId,
        [FromQuery] string newNodeName)
    {
        if (!Guid.TryParse(nodeId, out var nodeGuid))
            return NotFound("Guid is not a valid node id");

        await repo.TryRenameAsync(nodeGuid, newNodeName);
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromQuery] string nodeId)
    {
        if (!Guid.TryParse(nodeId, out var nodeGuid))
            return NotFound("Guid is not a valid node id");

        await repo.TryRemoveAsync(nodeGuid);
        return Ok();
    }
}