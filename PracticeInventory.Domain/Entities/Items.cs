using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeInventory.Domain.Entities;

public class Items
{
    [Key]
    public Guid ItemId { get; set; }
    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string ItemName { get; set; }
    public virtual Categories Category { get; set; }
}