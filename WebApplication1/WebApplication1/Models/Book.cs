using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Table("Book")]
public class Book
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)] 
    public string Title { get; set; } = null!;
    [MaxLength(50)] 
    public string Genre { get; set; } = null!;
    
    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    public virtual ICollection<ReaderBook> ReaderBooks { get; set; } = new List<ReaderBook>();
    
}