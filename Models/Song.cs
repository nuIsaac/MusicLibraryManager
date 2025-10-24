namespace MusicLib.Api.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; // /uploads/<file>
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
