using mamma_shopping_helper.Data;
using mamma_shopping_helper.Service;
using Microsoft.EntityFrameworkCore;

namespace mamma_shopping_helper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registrazione DbContext con retry policy
            builder.Services.AddDbContext<Data.MammaDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    }));


            builder.Services.AddScoped<IListeDellaSpesaService, ListeDellaSpesaService>();

            builder.Services.AddScoped<IProdottoService, ProdottoService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:4200", 
                            "https://localhost:4200")
                        .AllowAnyMethod()                
                        .AllowAnyHeader()                
                        .AllowCredentials();      
                });
            });


        

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MammaDbContext>();

                    // Applica eventuali migrations pending
                    context.Database.Migrate();

                    // Popola il database con dati mockup
                    DbSeeder.SeedData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Si è verificato un errore durante il seeding del database.");
                }
            }

            app.UseCors("AllowFrontend");

            // Verifica connessione al database all'avvio
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Data.MammaDbContext>();
                try
                {
                    dbContext.Database.CanConnect();
                    Console.WriteLine("? Database connesso correttamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"? Errore connessione database: {ex.Message}");
                }
            }



            // Configure the HTTP request pipeline
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
