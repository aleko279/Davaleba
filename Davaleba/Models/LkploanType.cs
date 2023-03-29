using System;
using System.Collections.Generic;

namespace Davaleba.Models;

public partial class LkploanType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<LoanApplication> LoanApplications { get; } = new List<LoanApplication>();
}
