using Application.Backends.RuntimeJournal.Interfaces;
using Application.Middlewares.RuntimeJournal.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Application.Userspaces.RuntimeJournalApi.Api;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/journal")]
public class RuntimeJournalController(ICommonRuntimeJournalService commonJournalService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromQuery] string journalHeader, [FromBody] object payload)
    {
        var createdJournalId = await commonJournalService.RegisterJournal(journalHeader, payload);
        return Ok(createdJournalId);
    }

    [HttpPost("create_with_parent")]
    public async Task<IActionResult> Attach([FromQuery] string parentJournalIdStr, [FromQuery] string journalHeader, [FromBody] object payload)
    {
        if (!Guid.TryParse(parentJournalIdStr, out var parentJournald))
            throw new SecureJournalException("Guid is not a valid journal id");

        var attachmentResultId = await commonJournalService.RegisterJournal(journalHeader, payload, parentJournald);
        return Ok(attachmentResultId);
    }

    // [HttpPost("delete")]
    // public async Task<IActionResult> Delete([FromQuery] string nodeId)
    // {
    //     if (!Guid.TryParse(nodeId, out var nodeGuid))
    //         return NotFound("Guid is not a valid node id");
    //
    //     // await repoTryRemoveAsync(nodeGuid);
    //     return Ok();
    // }
}