
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Repository;
using Store.Repository.Interfaces;
using Store.Services.Services;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var Service = app.Services.CreateScope())
            {
                var LoggerFactory = Service.ServiceProvider.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = Service.ServiceProvider.GetRequiredService<StoreDBContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, LoggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = LoggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
