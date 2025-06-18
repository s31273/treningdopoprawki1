namespace WebApplication1.DTOs;

public class GetReaderDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<GetBookDetailsForReaderDto> Books { get; set; } = new List<GetBookDetailsForReaderDto>();
}

public class GetBookDetailsForReaderDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime BorrowDate { get; set; }
    public string ReturnStatus { get; set; }
}