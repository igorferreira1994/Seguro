using Application.Commands;
using Application.Ports;
using Infrastructure.Adapters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços
ConfigureServices(builder.Services);

var app = builder.Build();

// Pipeline de middleware
ConfigureMiddleware(app);

// Mapeamento de endpoints
PropostasController.MapEndpoints(app);

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarPropostaCommand).Assembly));
    services.AddSingleton<IPropostaRepository, InMemoryPropostaRepository>();
    services.AddSingleton<IContratacaoRepository, InMemoryContratacaoRepository>();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

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
