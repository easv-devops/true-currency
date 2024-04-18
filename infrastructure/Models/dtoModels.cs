namespace infrastructure.Models;

public class Currency
{
    public required string Iso { get; set; }
    public decimal RateToUsd { get; set; }
}

public class HistoryDto
{
    public DateTime Date { get; set; }
    public string Source { get; set; }
    public string Target { get; set; }
    public decimal Value { get; set; }
    public decimal Result { get; set; }
}