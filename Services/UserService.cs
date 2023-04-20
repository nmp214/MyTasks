using MyTasks.Models;
using MyTasks.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using MyTasks.Controllers;
using System.Security.Claims;


namespace MyTasks.Services
{
    public class UserService : IUser
    {
        List<User> users { get; }
        private string filePath;
        private IWebHostEnvironment webHost;
        AdminController adminController = new AdminController();

        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "user.json");

            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public void savaToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }

        public static void getToken(List<Claim> claims, SecurityToken token)
        {
            System.Console.WriteLine("in service:");
            foreach (var item in claims)
            {
                System.Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine("token: " + token);
            System.Console.WriteLine((JwtSecurityToken)token);
            JwtSecurityToken t = (JwtSecurityToken)token;
            string id = t.Claims.FirstOrDefault(t => t.Type == "ID").ToString();
            System.Console.WriteLine("id:" + id);
        }

        public List<User> GetAll()
        {
            System.Console.WriteLine("in user service");
            // Claim c; string id;
            // c = adminController.claims.FirstOrDefault(t => t.Type == "ID");
            // if (c != null)
            //     id = c.ToString();
            // // string t = adminController.getToken();
            // // System.Console.WriteLine("in service. t: " + t);
            return users;
        }

        public void Add(User user)
        {
            user.UserId = users.Max(t => t.UserId) + 1;
            users.Add(user);
            savaToFile();
        }

        public User Get(int id) => users.FirstOrDefault(t => t.UserId == id);


        public bool Update(int id, User newUser)
        {
            if (newUser.UserId != id)
                return false;
            var user = users.FirstOrDefault(t => t.UserId == id);
            user.UserName = newUser.UserName;
            user.Password = newUser.Password;
            user.kind = newUser.kind;

            savaToFile();
            return true;
        }
        public bool Delete(int id)
        {
            var user = users.FirstOrDefault(t => t.UserId == id);
            if (user == null)
                return false;
            users.Remove(user);
            savaToFile();
            return true;
        }
    }
}