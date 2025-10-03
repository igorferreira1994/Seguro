using Application.Commands;
using Application.Ports;
using Infrastructure.Adapters;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SeguroDbContext>();
    db.Database.Migrate(); // Aplica migrations automaticamente
}

// Pipeline de middleware
ConfigureMiddleware(app);

// Mapeamento de endpoints
PropostasController.MapEndpoints(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // MediatR
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarPropostaCommand).Assembly));

    // Entity Framework + SQL Server
    services.AddDbContext<SeguroDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Repositórios com persistência real
    services.AddScoped<IPropostaRepository, SqlPropostaRepository>();
    services.AddScoped<IContratacaoRepository, SqlContratacaoRepository>();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Validação de modelo
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
}

static void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
}
