using CommonReg.BLL.Services.Interfaces;
using CommonReg.Common.Helpers;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Request;
using CommonReg.Common.UIModels.User.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace CommonReg.API.Controllers
{

    [Route("users")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class UserController : ControllerBase
    {
        private const string USER_NOT_FOUND_ERROR_MESSAGE = "User not Found";

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        [SwaggerOperation(
        Summary = "Get list of users",
        Description = "Get users",
        OperationId = "getUsers",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ViewUsers))]
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetUsers()
        {

            IEnumerable<UserResponseModel> users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpDelete("user")]
        [SwaggerOperation(
        Summary = "Delete user",
        Description = "Delete user",
        OperationId = "deleteUser",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.DeleteUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUser(
            [FromQuery]
            [Required]
            Guid userId)
        {

            await _userService.DeleteUser(userId);

            return NoContent();
        }

        [HttpPatch("user")]
        [SwaggerOperation(
        Summary = "Update user",
        Description = "Update user",
        OperationId = "updateUser",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ModifyUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser(
            [FromBody]
            [Required]
            UpdateUserRequestModel model)
        {

            bool result = await _userService.UpdateUser(model);

            return result ? NoContent() : NotFound();
        }

        [HttpGet("roles")]
        [SwaggerOperation(
        Summary = "Get user roles",
        Description = "Get user roles",
        OperationId = "getUserRoles",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ViewUserRoles))]
        [ProducesResponseType(typeof(IEnumerable<UserRoleModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserRoleModel>>> GetUserRoles(
            [FromQuery]
            [Required]
            Guid userId)
        {

            IEnumerable<UserRoleModel> roles = await _userService.GetUserRoles(userId);

            return Ok(roles ?? new List<UserRoleModel>());
        }

        [HttpGet("all-roles")]
        [SwaggerOperation(
            Summary = "Get all roles",
            Description = "Get all roles",
            OperationId = "getAllRoles",
            Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ViewUserRoles))]
        [ProducesResponseType(typeof(IEnumerable<RoleResponseModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleResponseModel>>> GetAllRoles()
        {

            IEnumerable<RoleResponseModel> roles = await _userService.GetAllRoles();

            return Ok(roles);
        }

        [HttpPut("{userId:Guid}/roles/{roleId:int}")]
        [SwaggerOperation(
        Summary = "Add role for user",
        Description = "Add role for user",
        OperationId = "addRoleForUser",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ModifyUserRoles))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddRoleForUser(
            [FromRoute]
            Guid userId,
            [FromRoute]
            int roleId
    )
        {
            AccountEntity user = await _userService.GetUserById(userId);

            if (user == null)
                return BadRequest(USER_NOT_FOUND_ERROR_MESSAGE);

            await _userService.AddRoleForUser(userId, roleId);

            return NoContent();
        }

        [HttpDelete("{userId:Guid}/roles/{roleId:int}")]
        [SwaggerOperation(
        Summary = "Delete role for user",
        Description = "Delete role for user",
        OperationId = "deleteRoleForUser",
        Tags = new[] { "User" }
        )]
        [Authorize(Policy = nameof(UserPermission.ModifyUserRoles))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteRoleForUser(
            [FromRoute]
            Guid userId,
            [FromRoute]
            int roleId
        )
        {
            AccountEntity user = await _userService.GetUserById(userId);

            if (user == null)
                return BadRequest(USER_NOT_FOUND_ERROR_MESSAGE);

            await _userService.DeleteRoleForUser(userId, roleId);

            return NoContent();
        }

    }
}
