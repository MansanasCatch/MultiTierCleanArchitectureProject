using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeInventory.Domain.Entities;

public class Categories
{
    [Key]
    public Guid CategoryId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string CategoryName { get; set; }
}