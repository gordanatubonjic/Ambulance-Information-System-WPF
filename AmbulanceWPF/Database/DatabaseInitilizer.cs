using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AmbulanceWPF.Database

{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AmbulanceDbContext();

           context.Database.EnsureCreated();

                          

            Console.WriteLine("Database created successfully!");
        }

       
    }
}