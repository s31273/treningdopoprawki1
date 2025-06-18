using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("ReaderBook")]
[PrimaryKey(nameof(BookId), nameof(ReaderId))]
public class ReaderBook
{
    [Column("Book_Id")]
    public int BookId { get; set; }
    [Column("Reader_Id")]
    public int ReaderId { get; set; }
    
    
    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; } = null!;
    [ForeignKey(nameof(ReaderId))]
    public virtual Reader Reader { get; set; } = null!;
}