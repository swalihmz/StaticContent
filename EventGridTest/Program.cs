using Azure.Messaging.EventGrid;
using EventGridTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<EventGridPublisherClient>(factory =>
{
    return new EventGridPublisherClient(
        new Uri(builder.Configuration["EventGridEndpoint"]),
        new Azure.AzureKeyCredential(builder.Configuration["EventGridAccessKey"]));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
