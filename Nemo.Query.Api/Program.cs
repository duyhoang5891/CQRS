using Confluent.Kafka;
using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Nemo.Query.Domain.Repositories;
using Nemo.Query.Infratructure.Consumers;
using Nemo.Query.Infratructure.DataAccess;
using Nemo.Query.Infratructure.Handlers;
using Nemo.Query.Infratructure.Repositories;
using EventHandler = Nemo.Query.Infratructure.Handlers.EventHandler;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString(nameof(NemoDbContext))));
builder.Services.AddDbContext<NemoDbContext>(configureDbContext);
builder.Services.AddSingleton<NemoDbContextFactory>(new NemoDbContextFactory(configureDbContext));

//Create database and table from code
//var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<NemoDbContext>();
//await dataContext.Database.EnsureCreatedAsync();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IEventHandler, EventHandler>();
builder.Services.Configure<ConsumerConfig>(config.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
