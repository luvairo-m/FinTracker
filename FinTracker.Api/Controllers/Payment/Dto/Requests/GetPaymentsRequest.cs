﻿using System;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public class GetPaymentsRequest
{
    public Guid? Id { get; set; }
    
    public decimal? MinAmount { get; set; }
    
    public decimal? MaxAmount { get; set; }
    
    public OperationType[] Types { get; set; }

    public DateTime? MinDate { get; set; }

    public DateTime? MaxDate { get; set; }

    public int[] Months { get; set; }

    public int[] Years { get; set; }

    public Guid? BillId { get; set; }
}