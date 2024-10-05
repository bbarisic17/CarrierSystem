﻿using System;
namespace King.Carrier.AccountingDomain.Receipts;

public class Receipt
{
    public int Id { get; set; }
    public string SerialNumber { get; set; } = default!;
    public DateTime CreatedTimestamp { get; set; }
}
