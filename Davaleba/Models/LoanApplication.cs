using System;
using System.Collections.Generic;

namespace Davaleba.Models;

public partial class LoanApplication
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? LoanTypeId { get; set; }

    public double? Amount { get; set; }

    public string? Currency { get; set; }

    public string? Period { get; set; }

    public int? StatusId { get; set; }

    public virtual LkploanType? LoanType { get; set; }

    public virtual LkploanStatus? Status { get; set; }

    public virtual User? User { get; set; }
}
