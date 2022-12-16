using System.Reflection.Metadata.Ecma335;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/api/Todo", () => new string[] { "string1, string2" });
app.MapGet("api/Todo/{id}", (int id) => $"Id: {id}").RequireAuthorization("CanReadTodoList");

app.Run();