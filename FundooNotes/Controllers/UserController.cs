using FundooNotes.Entity;
using FundooNotes.Interface;
using FundooNotes.Models;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    public readonly IUser _userService;
    public readonly ILogger<UserController> _logger;

    public UserController(IUser userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    [Route("Register")]
    public IActionResult Register (UserRegistration newUser)
    {
        try
        {
            User user = _userService.Register(newUser);
            if (user != null)
            {
                return Ok(new { success = true, message = "User Registration Successfull", data = user });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something Went Wrong:", data = user });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login(string email, string password)
    {
        try
        {
            string result = _userService.LogIn(email, password);
            if (result != null)
            {
                return Ok(new { success = true, message = "Login Successfull:", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something Went Wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("ForgetPassword")]
    public IActionResult ForgetPassword(string email)
    {
        try
        {
            bool result = _userService.ForgetPassWord(email);
            if (result != null)
            {
                return Ok(new { success = true, message = "Reset Link sent On Your Email" });
            }
            else
            {
                return BadRequest(new { success = false, message = "something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    [HttpPut]
    [Route("ResetPassword")]
    public IActionResult ResetPassword(string newPassword, string confirmPassword)
    {
        try
        {
            // user id we store it pass that userid
            string userId = "";
            bool result = _userService.ResetPassword(userId, newPassword, confirmPassword);
            if (result != null)
            {
                return Ok(new { success = true, message = "Password Updated Successfully" });
            }
            else
            {
                return BadRequest(new { sucess = false, messsge = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
