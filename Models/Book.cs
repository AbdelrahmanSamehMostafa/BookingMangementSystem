using System.ComponentModel.DataAnnotations;

namespace BookingMangementSystem.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Summary { get; set; }

    public bool IsRecommended { get; set; }

    public string? FilePath { get; set; }

    [Required(ErrorMessage = "The Author is required")]
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
