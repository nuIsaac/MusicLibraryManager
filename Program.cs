using Microsoft.EntityFrameworkCore;
using MusicLib.Api.Data;
using MusicLib.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// SQLite for local dev
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=musiclib.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create DB / apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

// Serve static files from wwwroot (index.html, app.js, styles.css, uploads)
app.UseDefaultFiles();
app.UseStaticFiles();

// List songs
app.MapGet("/api/songs", async (AppDbContext db) =>
    await db.Songs.OrderByDescending(s => s.CreatedUtc).ToListAsync());

// Upload song (multipart/form-data: title, file)
app.MapPost("/api/songs/upload", async (HttpRequest req, IWebHostEnvironment env, AppDbContext db) =>
{
    var form = await req.ReadFormAsync();
    var file = form.Files["file"];
    if (file is null || file.Length == 0) return Results.BadRequest("No file");

    var title = form["title"].ToString();
    if (string.IsNullOrWhiteSpace(title))
        title = Path.GetFileNameWithoutExtension(file.FileName);

    // Allow only common audio types
    var ok = new[] { "audio/mpeg","audio/wav","audio/x-wav","audio/aac","audio/flac","audio/ogg" };
    if (!ok.Contains(file.ContentType)) return Results.BadRequest("Unsupported audio type");

    var root = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    var uploads = Path.Combine(root, "uploads");
    Directory.CreateDirectory(uploads);

    var name = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
    var savePath = Path.Combine(uploads, name);
    using var stream = File.Create(savePath);
    await file.CopyToAsync(stream);

    var song = new Song { Title = title, FilePath = $"/uploads/{name}" };
    db.Songs.Add(song);
    await db.SaveChangesAsync();

    return Results.Ok(song);
});

// Delete song + file
app.MapDelete("/api/songs/{id:int}", async (int id, IWebHostEnvironment env, AppDbContext db) =>
{
    var song = await db.Songs.FindAsync(id);
    if (song is null) return Results.NotFound();

    var root = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    var absolute = Path.Combine(root, song.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
    if (File.Exists(absolute)) File.Delete(absolute);

    db.Songs.Remove(song);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
