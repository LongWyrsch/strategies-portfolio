namespace Strategies.Domain;

public class Candle
{
    public string Symbol { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string Resolution { get; set; } = string.Empty;
    public DateTime DateDownloaded { get; set; }
    public DateTime Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public double? Ichimoku_ConversionLine { get; set; }
    public double? Ichimoku_BaseLine { get; set; }
    public double? Ichimoku_LaggingSpan { get; set; }
    public double? Ichimoku_LeadingSpanA { get; set; }
    public double? Ichimoku_LeadingSpanB { get; set; }
    public double? MA_20 { get; set; }
    public double? MA_50 { get; set; }
    public double? MA_100 { get; set; }
    public double? MA_200 { get; set; }
    public double? BB_Basis { get; set; }
    public double? BB_Upper { get; set; }
    public double? BB_Lower { get; set; }
    public double Volume { get; set; }
    public double? MACD_Histogram { get; set; }
    public double? MACD { get; set; }
    public double? MACD_Signal { get; set; }
    public double? RSI { get; set; }
    public double? RSI_BasedMA { get; set; }
    public double? ADX { get; set; }
    public double? MF { get; set; }
    public double? OBV { get; set; }
    public double? ATR { get; set; }
}