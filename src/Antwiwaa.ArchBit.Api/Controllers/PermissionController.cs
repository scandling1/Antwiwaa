using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Permissions.Commands;
using Antwiwaa.ArchBit.Application.Permissions.Queries;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Antwiwaa.ArchBit.Api.Controllers
{
    public class PermissionController : ApiController
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeactivatePermissionCmd { PermissionId = id }, cancellationToken);

            if (result.IsFailure) return Problem(result.Error);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPermissionQuery { PermissionId = id }, cancellationToken);

            if (result.HasNoValue) return NotFound();

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<UserPermissionStringDto>> Get(string userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserPermissionsQuery { UserId = userId }, cancellationToken);

            return result.IsFailure
                ? Problem(result.Error)
                : Ok(new UserPermissionStringDto { Permissions = result.Value });
        }

        [AllowAnonymous]
        [HttpPost("GetPermissionList/{page}/{pageSize}")]
        public async Task<ActionResult<DataTableVm<PermissionDto>>> GetPermissionListAsync(
            [FromBody] DataTableListRequestModel model, int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPermissionListQuery
                {
                    Page = page,
                    PageSize = pageSize,
                    Parameters = model.Parameters,
                    DataReadMode = model.DataReadMode
                },
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionDto permissionDto,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new CreatePermissionCmd { PermissionDto = permissionDto },
                cancellationToken);

            return result.IsFailure ? Problem(result.Error) : Ok();
        }

        [HttpPut("{permissionId}")]
        public async Task<ActionResult> Put([FromBody] PermissionDto permissionDto, int permissionId,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdatePermissionCmd
                {
                    PermissionDto = permissionDto,
                    PermissionId = permissionId
                },
                cancellationToken);

            return Ok();
        }
    }
}