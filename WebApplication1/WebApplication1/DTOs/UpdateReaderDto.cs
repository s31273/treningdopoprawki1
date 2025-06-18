using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UpdateReaderDto
{
    [MaxLength(100)] [Required] public string Name { get; set; } = null!;
    [MaxLength(150)] 
    public string? Email { get; set; } = null!;
    public ICollection<UpdateBorrowingsForReaderDto> Borrowings { get; set; } = new List<UpdateBorrowingsForReaderDto>();
}

public class UpdateBorrowingsForReaderDto
{
    [Required] [MaxLength(100)] public string Title { get; set; } = null!;
    [Required] [MaxLength(100)] public string Genre { get; set; } = null!;
    [Required] public DateTime BorrowDate { get; set; }
    [Required] [MaxLength(50)] public string ReturnStatus { get; set; } = null!;
}