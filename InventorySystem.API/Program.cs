
using Microsoft.EntityFrameworkCore;
namespace InventorySystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ============================
            // DATABASE
            // ============================
            builder.Services.AddDbContext<Data.InventoryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ============================
            // Repositories
            // ============================
            builder.Services.AddScoped<Core.Interfaces.IItemRepository, Repositories.ItemRepository>();
            builder.Services.AddScoped<Core.Interfaces.IUserRepository, Repositories.UserRepository>();

            // ============================
            // Services
            // ============================
            builder.Services.AddScoped<Core.Interfaces.IItemService, Services.ItemService>();
            builder.Services.AddScoped<Core.Interfaces.IUserService, Services.UserService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
