using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Categories;

public class CategorySearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [StringValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
}