using HR.LeaveManagement.Application;
using HR.LeaveManagement.Persistance;
using HR.LeaveManagement.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigurePersistsanceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LeaveManagement", Version = "v1" });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

//middleware section
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseSwagger();
app.UseSwaggerUI(ui =>
{
    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaveManagement Api v1");
});

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

/*app.MapGet("/", () => "Hello World!");*/

app.Run();
