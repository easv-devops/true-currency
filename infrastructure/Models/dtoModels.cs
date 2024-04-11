namespace infrastructure.Models;

public class Currency
{
    public required string Iso { get; set; }
    public decimal RateToUsd { get; set; }
}