using CommonReg.Common.Helpers;
using CommonReg.Common.JWTToken.Models;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Response;
using CommonReg.Common.UIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using CommonReg.BLL.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace CommonReg.API.Controllers;

[Route("accounts")]
[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AccountController : ControllerBase
{
    private const string TAG = "Account";
    private const string EMAIL_IS_ALREADY_REGISTERED_ERROR_MESSAGE = "Email is already registered.";
    private const string INSERT_USER_FORGOT_PASS_EXCEPTION = "UserForgotPassword was not inserted successfully";
    private const string CHANGE_PASSWORD_SUCCESS_MESSAGE = "Your password has been successfully changed.";
    private const string USER_NOT_FOUND_ERROR_MESSAGE = "User not Found";
    private const string LINK_IS_INVALID_ERROR_MESSAGE = "Link is invalid.";
    private const string LINK_EXPIRED_ERROR_MESSAGE = "Link expired.";

    private readonly IAccountService _accountService;
    private readonly IUserService _userService;

    public AccountController(IAccountService accountService, IUserService userService)
    {
        _accountService = accountService;
        _userService = userService; 
    }

    [HttpPost("login")]
    [SwaggerOperation(
            Summary = "login to user account",
            Description = "login to user account",
            OperationId = "login",
            Tags = new[] { TAG }
        )]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginRequestModel user)
    {
        string userAgent = Request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
        TokenResult token = await _accountService.Login(user, userAgent);

        return !token.Success ? BadRequest(token.Error) : Ok(token.TokenModel);
    }

    [HttpGet("user/profile")]
    [SwaggerOperation(
        Summary = "Get user profile",
        OperationId = "getUserProfile",
        Tags = new[] { TAG }
    )]
    [Authorize]
    [ProducesResponseType(typeof(UserProfileResponseModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetUserProfileAsync()
    {
        Guid userId = User.GetUserId();

        UserProfileResponseModel response = await _userService.GetUserProfileById(userId);

        return Ok(response);
    }


    [HttpPost("token/refresh")]
    [SwaggerOperation(
        Summary = "refresh access token",
        Description = "refresh access token",
        OperationId = "refreshToken",
        Tags = new[] { TAG }
    )]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestModel refreshTokenRequest)
    {
        string userAgent = Request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
        TokenResult token = await _accountService.RefreshToken(refreshTokenRequest, userAgent);

        return !token.Success ? BadRequest(token.Error) : Ok(token.TokenModel);
    }

    [HttpPost("sign-up")]
    [SwaggerOperation(
    Summary = "Registration user account",
    OperationId = "registrationAsync",
    Tags = new[] { TAG }
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RegistrationAsync(
     [FromBody] RegistrationRequestModel user
     )
    {
        bool isSuccess = await _accountService.Registration(user);

        return !isSuccess ? BadRequest(EMAIL_IS_ALREADY_REGISTERED_ERROR_MESSAGE) : Ok();
    }

    [HttpPost("sign-up/confirm")]
    [SwaggerOperation(
        Summary = "Confirm registration",
        OperationId = "confirmRegistrationAsync",
        Tags = new[] { TAG }
    )]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> ConfirmRegistrationAsync(
        [FromBody] ConfirmRegistrationRequestModel model
        )
    {
        AccountEntity result = await _accountService.SetActive(model);

        if (result == null)
        {
            return BadRequest(LINK_IS_INVALID_ERROR_MESSAGE);
        }

        string userAgent = Request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
        TokenResult token = await _accountService.GetToken(result, userAgent);

        return Ok(token.TokenModel);
    }


    [HttpPost("sign-up/confirm/link/send-again")]
    [SwaggerOperation(
    Summary = "Send confirmation again",
    OperationId = "sendRegistrationConfirmationAgain",
    Tags = new[] { TAG }
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SendRegistrationConfirmationAgainAsync(
      [FromBody] SendConfirmationRegistrationRequestModel model
      )
    {
        await _accountService.SendConfirmationRegistrationAgain(model.Email);

        return Ok();
    }

    [HttpPost("password/forgot")]
    [SwaggerOperation(
      Summary = "Forgot password",
      OperationId = "forgotPasswordAsync",
      Tags = new[] { TAG }
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> ForgotPasswordAsync(
      [FromBody] ForgotPasswordRequestModel model
      )
    {
        AccountEntity user = await _userService.GetUserByEmail(model.Email);
        if (user == null)
            return NotFound(USER_NOT_FOUND_ERROR_MESSAGE);

        int insertedCount = await _accountService.ForgotPassword(user);

        return insertedCount < 1 ? throw new Exception(INSERT_USER_FORGOT_PASS_EXCEPTION) : (ActionResult)Ok();
    }

    [HttpPost("password/restore")]
    [SwaggerOperation(
        Summary = "Restore password",
        Description = "Restore password",
        OperationId = "restorePassword",
        Tags = new[] { TAG }
    )]
    public async Task<ActionResult> RestorePassword([FromBody] RestorePasswordRequestModel model)
    {
        UserForgotPasswordEntity entity = await _accountService.GetForgotUserPassByIdAndCode(model.UserId, model.Code);

        if (entity == null)
            return BadRequest(LINK_EXPIRED_ERROR_MESSAGE);

        await _accountService.RestoreUserPassword(model.UserId, model.Email);

        return Ok(CHANGE_PASSWORD_SUCCESS_MESSAGE);
    }


    [HttpPost("logout")]
    [SwaggerOperation(
     Summary = "Logout",
     OperationId = "logout",
     Tags = new[] { TAG }
    )]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> LogoutAsync()
    {
        int sessionId = User.GetSessionId();
        Guid userId = User.GetUserId();

        await _accountService.DeleteSession(sessionId, userId);

        return NoContent();
    }
}