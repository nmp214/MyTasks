using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;

namespace MyTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public List<Claim> claims = new List<Claim>();
        public AdminController()
        {
            this.claims = claims;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            User newUser = null;
            Boolean isAdminExists = false;
            Boolean isUserExists = false;
            List<User> users = TokenService.users;
            foreach (User user in users)
            {
                if (user.kind == "Admin" && user.UserName == User.UserName && user.Password == User.Password)
                    isAdminExists = true;
                if (user.kind == "User" && user.UserName == User.UserName && user.Password == User.Password)
                {
                    isUserExists = true;
                    newUser = user;
                    break;
                }
            }
            if (!isAdminExists && !isUserExists)
                return Unauthorized();

            if (isAdminExists)
            {
                claims.Add(new Claim("type", "Admin"));
                claims.Add(new Claim("ID", "273"));
            }
            if (isUserExists)
            {
                claims.Add(new Claim("type", "User"));
                claims.Add(new Claim("ID", newUser.UserId.ToString()));
            }
            SecurityToken token = TokenService.GetToken(claims);
            var t = HttpContext.Request.Headers["Authorization"];
            foreach (var item in claims)
            {
                System.Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine("t: " + t.ToString());
            TaskService.getToken(claims, token);
            return new OkObjectResult(TokenService.WriteToken(token));
        }


    }
}