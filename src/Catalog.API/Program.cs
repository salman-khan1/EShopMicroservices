using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

//add service containers
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();


var app = builder.Build();

//Configure the Http request pipeline

app.MapCarter();

app.Run();
