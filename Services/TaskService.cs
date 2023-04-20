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
    public class TaskService : ITask
    {
        private static string tokenId;
        List<MyTask> tasks { get; }
        private string filePath;
        private IWebHostEnvironment webHost;
        AdminController adminController = new AdminController();

        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "task.json");

            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<MyTask>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public void savaToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
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
            tokenId = t.Claims.FirstOrDefault(t => t.Type == "ID").Value.ToString();
            System.Console.WriteLine("id:" + tokenId);
        }

        public List<MyTask> GetAll()
        {
            System.Console.WriteLine("in task service. tokenid: " + tokenId + "userid: " + tasks.First().UserId);
            List<MyTask> newTasks = tasks.Where(t => t.UserId.ToString() == tokenId).ToList();
            return newTasks;
        }
        public MyTask Get(int id) => tasks.FirstOrDefault(t => t.Id == id && t.UserId.ToString() == tokenId);

        public void Add(MyTask task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            savaToFile();
        }

        public bool Update(int id, MyTask newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id && t.UserId.ToString() == tokenId);
            task.Name = newTask.Name;
            task.IsDone = newTask.IsDone;
            savaToFile();
            return true;
        }
        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id && t.UserId.ToString() == tokenId);
            if (task == null)
                return false;
            tasks.Remove(task);
            savaToFile();
            return true;
        }
    }
}