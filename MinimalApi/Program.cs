using MinimalApi.Library.DataAccess;
using MinimalApi.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanReadTodoList", policy =>
        //policy.Requirements.Add(new MinimumAgeRequirement(21))
        policy.RequireClaim("RTL")
    );
});

builder.Services.AddSingleton<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomerRoutes();

app.Run();