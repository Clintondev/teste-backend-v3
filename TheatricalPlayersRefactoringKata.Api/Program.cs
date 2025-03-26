using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Api.Examples;
using TheatricalPlayersRefactoringKata.AsyncProcessing;
using TheatricalPlayersRefactoringKata.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TheaterContext>(options =>
    options.UseSqlite("Data Source=theater.db"));

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepositoryEF>();

builder.Services.AddSingleton<AsyncStatementProcessor>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TheatricalPlayersRefactoringKata API",
        Version = "v1",
        Description = "API para processamento de extratos teatrais."
    });
    options.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<StatementRequestExample>();

var app = builder.Build();

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

app.Run();

public partial class Program { }
