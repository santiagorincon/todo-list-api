using Microsoft.EntityFrameworkCore;
using todo_list.DataAccess;
using todo_list.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(
    builder.Services,
    builder.Configuration
);

var app = builder.Build();

app.MapPost("/api/todos", async (TaskItem task, AppDbContext db) =>
{
    db.Tasks.Add(task);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{task.Id}", task);
});

app.MapGet("/api/todos", async (AppDbContext db) =>
{
    return await db.Tasks.ToListAsync();
});

app.MapGet("/api/todos/{id}", async (int id, AppDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

app.MapPut("/api/todos/{id}", async (int id, TaskItem updatedTask, AppDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound();

    task.Title = updatedTask.Title;
    task.Description = updatedTask.Description;
    task.Status = updatedTask.Status;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/todos/{id}", async (int id, AppDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound();

    db.Tasks.Remove(task);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.UseCors("AllowLocalhost");

app.Run();

void ConfigureServices(IServiceCollection services, ConfigurationManager configManager) {
    services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseNpgsql(configManager.GetConnectionString("AppDb"));
        }, ServiceLifetime.Transient);

    services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}