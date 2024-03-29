using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Nemo.Cmd.Api.Commands;
using Nemo.Cmd.Domain.Aggregates;
using Nemo.Cmd.Infratructure.Config;
using Nemo.Cmd.Infratructure.Dispatchers;
using Nemo.Cmd.Infratructure.Handlers;
using Nemo.Cmd.Infratructure.Producers;
using Nemo.Cmd.Infratructure.Repositories;
using Nemo.Cmd.Infratructure.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<ItemAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();


// register command handler
var commandHanlder = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<AddItemCommand>(commandHanlder.HandlerAsync);
dispatcher.RegisterHandler<EditItemCommand>(commandHanlder.HandlerAsync);
dispatcher.RegisterHandler<DeleteItemCommand>(commandHanlder.HandlerAsync);

builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>(_ => dispatcher);

builder.Services.AddControllers();
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
