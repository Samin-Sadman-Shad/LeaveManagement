using HR.LeaveManagement.Application;
using HR.LeaveManagement.Persistance;
using HR.LeaveManagement.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using HR.LeaveManagement.API.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigurePersistsanceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "LeaveManagement", Version = "v2" });
});*/

builder.Services.ConfigSwagger();

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
    ui.SwaggerEndpoint("/swagger/v2/swagger.json", "LeaveManagement Api v2");
});

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

/*app.MapGet("/", () => "Hello World!");*/

app.Run();
