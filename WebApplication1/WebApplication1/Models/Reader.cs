using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Table("Reader")]
public class Reader
{
    [Key]
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = null!;
    [MaxLength(150)]
    public string? Email { get; set; } 
    
    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    public virtual ICollection<ReaderBook> ReaderBooks { get; set; } = new List<ReaderBook>();
}