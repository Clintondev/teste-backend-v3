using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Api.Examples;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TheatricalPlayersRefactoringKata API",
        Version = "v1",
        Description = "API para processamento de extratos teatrais."
    });
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<StatementRequestExample>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheatricalPlayersRefactoringKata API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
