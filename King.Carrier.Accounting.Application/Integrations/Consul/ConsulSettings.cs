namespace King.Carrier.AccountingApplication.Integrations.Consul;

public class ConsulSettings
{
    public const string SectionName = "ConsulSettings";
    public string Url { get; set; } = default!;
}
