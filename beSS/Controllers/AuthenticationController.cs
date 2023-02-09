﻿using System;
using System.Threading.Tasks;
using beSS.Models.RequestModels;
using beSS.Services;
using Microsoft.AspNetCore.Mvc;

namespace beSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationUser request)
        {
            var newUser = await _userService.Registration(request);
            return Ok(newUser);
        }

        [HttpGet("Get-list-user")]
        public IActionResult GetListUser()
        {
            var listUser = _userService.GetlistUsers();
            return Ok(listUser);
        }

        [HttpPost("Edit-user")]
        public IActionResult EditUser(EditUserRequest request)
        {
            var targetUser = _userService.EditUser(request);
            return Ok(targetUser);
        }

        [HttpDelete("Delete-User{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var tatgetUser = _userService.DeleteUser(id);
            return Ok(tatgetUser);
        }
    }
}