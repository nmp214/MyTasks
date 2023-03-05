using MyTasks.Models;
using MyTasks.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace MyTasks.Services
{
    public class TaskService : ITask
    {
        List<MyTask> tasks { get; }
        private string filePath;
        private IWebHostEnvironment webHost;
        
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


        public List<MyTask> GetAll() => tasks;

        public void Add(MyTask task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            savaToFile();
        }

        public MyTask Get(int id) => tasks.FirstOrDefault(t => t.Id == id);


        public bool Update(int id, MyTask newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id);
            task.Name = newTask.Name;
            task.IsDone = newTask.IsDone;
            savaToFile();
            return true;
        }
        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            savaToFile();
            return true;
        }
    }
}