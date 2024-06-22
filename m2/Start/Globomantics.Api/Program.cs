using Globomantics.Api.Extensions.Swagger;
using Globomantics.Api.Middlewares;
using Globomantics.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c=>c.AddSwaggerApiKeySecurity());
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();


var app = builder.Build();

//app.UseApiKey();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
