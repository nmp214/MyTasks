using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyTasks.Interfaces;
using MyTasks.Services;

namespace MyTasks.Utilities
{
    public static class Helper
    {
        public static void AddTasks(this IServiceCollection service)
        {
            service.AddSingleton<ITask, TaskService>();
        }

        public static void AddUsers(this IServiceCollection service)
        {
            service.AddSingleton<IUser, UserService>();
        }
    }
}