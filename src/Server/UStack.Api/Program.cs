using UStack.Identity.Infrastructure.Extensions;
using UStack.Users.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.IntegrateIdentityModule(builder.Configuration);
builder.Services.IntegrateUsersModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();