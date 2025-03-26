using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Api.Examples;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigurePipeline(app);

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TheatricalPlayersRefactoringKata API",
            Version = "v1",
            Description = "API para processamento de extratos teatrais."
        });
        options.ExampleFilters();
    });

    services.AddSwaggerExamplesFromAssemblyOf<StatementRequestExample>();
}

static void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TheatricalPlayersRefactoringKata API V1");
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

public partial class Program { }
