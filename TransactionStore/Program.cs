using LoggerService;
using NLog;
using TransactionStore.DAL.StoredProcedure;
using TransactionStore.Services;
using TransactionStore.Services.Interfaces;

namespace TransactionStore;
internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
            {
                policy.WithOrigins("http://localhost:5175").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });

        //builder.Services.AddAutoMapper(typeof(MapperConfigAPI), typeof(MapperConfigBusiness));

        builder.Services.AddScoped<ILoggerManager, LoggerManager>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ITransactionStoredProcedure, TransactionStoredProcedure>();
        builder.Services.AddScoped<IRateService, RateService>();
        builder.Services.AddScoped<ICalculationService, CalculationService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins);
        app.MapControllers();
        app.Run();
    }
}