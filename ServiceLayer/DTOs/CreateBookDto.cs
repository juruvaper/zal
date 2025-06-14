using System.ComponentModel.DataAnnotations;

public class CreateBookRequestDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public int AuthorId { get; set; }
}
