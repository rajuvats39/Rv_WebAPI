using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Win32;
using Newtonsoft.Json.Serialization;
using Rv_WebAPI.Models.Data;
using Rv_WebAPI.Models.Entity;

namespace Rv_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // DI for Connection string
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddControllers();

            // Configure DbContext Class
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BlogAppDbContext>(options => options.UseSqlServer(connectionString));
            // Register repository implementations
            builder.Services.AddScoped<IRepositoryBlogApp<BlogAppModel>, IRepositoryBlogAppSql<BlogAppModel>>();
            builder.Services.AddScoped<IRepositoryBlogApp<BlogAppCatagotyModel>, IRepositoryBlogAppSql<BlogAppCatagotyModel>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ✅ Serve static files from custom folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Resources", "UploadedFiles")),
                RequestPath = "/Resources/UploadedFiles"
            });

            // Enable serving static files
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.UseRouting();
            app.Run();
        }
    }
}


