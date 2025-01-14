﻿using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Bills;

public class BillSearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [StringValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
    
    [Column("CurrencyId")]
    public Guid? CurrencyId { get; set; }
}