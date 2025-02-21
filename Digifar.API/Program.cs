using Digifar.Application;
using Digifar.Application.Common.Exceptions;
using Digifar.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//configuring Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()  || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

//exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
app.UseCors("AllowAllOrigins"); 