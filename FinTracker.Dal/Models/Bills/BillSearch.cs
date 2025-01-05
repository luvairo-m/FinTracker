using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Bills;

public class BillSearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [ValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
}