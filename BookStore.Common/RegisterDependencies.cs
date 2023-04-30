using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Data.Interfaces;
using BookStore.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStore.Common
{
    public static class RegisterDependencies
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IShelveRackService, ShelveRackService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IShelveRackRepository, ShelveRackRepository>();
        }
    }
}
