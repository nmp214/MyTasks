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
using MyTasks.Interfaces;

namespace MyTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IUser userSer;

        public List<Claim> claims = new List<Claim>();
        // public AdminController()
        // {
        //     this.claims = claims;
        //     this.userSer = userSer;
        // }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            System.Console.WriteLine("in login");
            User newUser;
            System.Console.WriteLine("newUser");
            newUser = userSer.Login(User);
            System.Console.WriteLine(newUser.ToString());
            if (newUser == null)
                return Unauthorized();

            if (newUser.kind == "Admin")
            {
                claims.Add(new Claim("type", "Admin"));
                claims.Add(new Claim("ID", "273"));
            }
            if (newUser.kind == "User")
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