using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);


//add service containers
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);


}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddCarter();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

//Configure the Http request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
