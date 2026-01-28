using UStack.Course.Infrastructure.Extensions;
using UStack.Identity.Infrastructure.Extensions;
using UStack.Notification.Infrastructure;
using UStack.Users.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
    c.EnableAnnotations();
});

builder.Services.IntegrateIdentityModule(builder.Configuration);
builder.Services.IntegrateUsersModule(builder.Configuration);
builder.Services.IntegrateCourseModule(builder.Configuration);
builder.Services.IntegrateNotificationModule(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();