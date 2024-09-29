using Strategies.Domain;
using Strategies.Domain.Presentation;
using Strategies.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Tried reading the appsettings.json file from the Strategies.ConsoleApp project, but it didn't work.
// builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Strategies.ConsoleApp"));
// builder.Configuration.AddJsonFile("../Strategies.ConsoleApp/appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
            {
                policy.WithOrigins("http://127.0.0.1:5500");
                policy.WithOrigins("http://localhost:5173");
                policy.WithOrigins("https://strategies-portfolio-charting-ekctcbgpgfgjf0h0.canadacentral-01.azurewebsites.net/");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddDomainServices(builder.Configuration);
builder.Services.AddScoped<IMessageService, ConsoleMessageService>();

// Register controllers
builder.Services.AddControllers()
    // This option permits the serialization of special floating-point values such as NaN, Infinity, and -Infinity
    // Because some Results properties are of type double, this is necessary to prevent serialization errors
    .AddJsonOptions(options => 
        {
            options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // If using Swagger
    app.UseSwaggerUI(); // If using Swagger
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.MapControllers();  // Map attribute-routed controllers

app.Run();


