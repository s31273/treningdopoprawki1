using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateReaderDto
{
    [Required] 
    [MaxLength(150)] 
    public string Name { get; set; } = null!;
    [MaxLength(150)] 
    public string? Email { get; set; }

    public ICollection<CreateBookForReaderDto> Books { get; set; } = new List<CreateBookForReaderDto>();
}

public class CreateBookForReaderDto
{
    [Required] [MaxLength(100)] 
    public string Title { get; set; } = null!;
    [Required] [MaxLength(50)] 
    public string Genre { get; set; } = null!;
}