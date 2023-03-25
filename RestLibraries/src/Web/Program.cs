using RestLibraries.Web.Endpoints;
using RestLibraries.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = builder.Environment.ApplicationName,
        Version = "v1" 
    });
});

var app = builder.Build();
app.AddPaymentEndpoints();

app.UseDeveloperExceptionPage();
app.UseMigrationsEndPoint();
app.UseSwagger();
app.UseSwaggerUI(c => 
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
    $"{builder.Environment.ApplicationName} v1"));

app.Run();

public partial class Program { }