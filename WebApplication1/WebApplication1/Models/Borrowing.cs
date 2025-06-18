using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;
[Table("Borrowing")]
[PrimaryKey(nameof(BookId), nameof(ReaderId))]
public class Borrowing
{
    [Column("Book_Id")]
    public int BookId { get; set; }
    [Column("Reader_Id")]
    public int ReaderId { get; set; }
    public DateTime BorrowDate { get; set; }
    [MaxLength(50)]
    public string ReturnStatus { get; set; } = null!;
    
    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; } = null!;
    [ForeignKey(nameof(ReaderId))]
    public virtual Reader Reader { get; set; } = null!;
}