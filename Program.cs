﻿var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.UseRouting();
    
    app.UseEndpoints(routeBuilder =>
    {
        routeBuilder.MapHub<MyHub>("/chat");
        routeBuilder.MapHub<RobotHub>("/robot");
        
        routeBuilder.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("This app used SignalR!");
        });
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
