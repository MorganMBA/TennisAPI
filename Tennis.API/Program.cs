using Amazon.DynamoDBv2;
using Tennis.Core.Repositories;
using Tennis.Domain.Services.Impl;
using Tennis.Domain.Services.Interfaces;
using Tennis.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAWSService<IAmazonDynamoDB>(new Amazon.Extensions.NETCore.Setup.AWSOptions()
{
    Region = Amazon.RegionEndpoint.EUCentral1,
    Credentials = new Amazon.Runtime.BasicAWSCredentials("fakeAccessKeyId", "fakeSecretAccessKey"),
    DefaultClientConfig = { ServiceURL = "http://localhost:8000", AuthenticationRegion = "eu-central-1" }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();


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
