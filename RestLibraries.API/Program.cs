var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = builder.Environment.ApplicationName,
        Version = "v1" 
    });
});
var app = builder.Build();

app.MapGet("/rates", (string currencyA, string currencyB) => {
    return "{\"rates\":{\"EURGBP\":{\"rate\":0.882949,\"timestamp\":1679154723}},\"code\":200}";
});

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => 
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
    $"{builder.Environment.ApplicationName} v1"));

app.Run();
