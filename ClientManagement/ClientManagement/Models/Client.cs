using System;
using System.Collections.Generic;

namespace ClientManagement.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public Guid LicenceKey { get; set; }

    public string? ClientName { get; set; }

    public DateOnly LicenceStartDate { get; set; }

    public DateOnly LicenceEndDate { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
