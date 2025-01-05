﻿using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Currencies;

public class CurrencySearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [ValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
}