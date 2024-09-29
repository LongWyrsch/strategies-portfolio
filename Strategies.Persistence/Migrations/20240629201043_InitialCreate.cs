using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candles",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Exchange = table.Column<string>(type: "TEXT", nullable: false),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    DateDownloaded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Open = table.Column<double>(type: "REAL", nullable: false),
                    High = table.Column<double>(type: "REAL", nullable: false),
                    Low = table.Column<double>(type: "REAL", nullable: false),
                    Close = table.Column<double>(type: "REAL", nullable: false),
                    Ichimoku_ConversionLine = table.Column<double>(type: "REAL", nullable: true),
                    Ichimoku_BaseLine = table.Column<double>(type: "REAL", nullable: true),
                    Ichimoku_LaggingSpan = table.Column<double>(type: "REAL", nullable: true),
                    Ichimoku_LeadingSpanA = table.Column<double>(type: "REAL", nullable: true),
                    Ichimoku_LeadingSpanB = table.Column<double>(type: "REAL", nullable: true),
                    MA_20 = table.Column<double>(type: "REAL", nullable: true),
                    MA_50 = table.Column<double>(type: "REAL", nullable: true),
                    MA_100 = table.Column<double>(type: "REAL", nullable: true),
                    MA_200 = table.Column<double>(type: "REAL", nullable: true),
                    BB_Basis = table.Column<double>(type: "REAL", nullable: true),
                    BB_Upper = table.Column<double>(type: "REAL", nullable: true),
                    BB_Lower = table.Column<double>(type: "REAL", nullable: true),
                    Volume = table.Column<double>(type: "REAL", nullable: false),
                    MACD_Histogram = table.Column<double>(type: "REAL", nullable: true),
                    MACD = table.Column<double>(type: "REAL", nullable: true),
                    MACD_Signal = table.Column<double>(type: "REAL", nullable: true),
                    RSI = table.Column<double>(type: "REAL", nullable: true),
                    RSI_BasedMA = table.Column<double>(type: "REAL", nullable: true),
                    ADX = table.Column<double>(type: "REAL", nullable: true),
                    MF = table.Column<double>(type: "REAL", nullable: true),
                    OBV = table.Column<double>(type: "REAL", nullable: true),
                    ATR = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candles", x => new { x.Symbol, x.Exchange, x.Resolution, x.DateDownloaded, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Exchange = table.Column<string>(type: "TEXT", nullable: false),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    Timeframe = table.Column<string>(type: "TEXT", nullable: false),
                    Fee = table.Column<double>(type: "REAL", nullable: false),
                    Strategy = table.Column<int>(type: "INTEGER", nullable: false),
                    OutlierType = table.Column<string>(type: "TEXT", nullable: false),
                    BuyAndHoldProfit = table.Column<double>(type: "REAL", nullable: true),
                    StrategyProfit = table.Column<double>(type: "REAL", nullable: true),
                    Performance = table.Column<double>(type: "REAL", nullable: true),
                    TradeCount = table.Column<double>(type: "REAL", nullable: false),
                    TradeProfitAverage = table.Column<double>(type: "REAL", nullable: true),
                    TradeProfitStdDeviation = table.Column<double>(type: "REAL", nullable: true),
                    TradeProfitNormality = table.Column<double>(type: "REAL", nullable: true),
                    TradeProfitSkewness = table.Column<double>(type: "REAL", nullable: true),
                    TradeProfitKurtosis = table.Column<double>(type: "REAL", nullable: true),
                    WinRate = table.Column<double>(type: "REAL", nullable: true),
                    BarsPerTradeAverage = table.Column<double>(type: "REAL", nullable: true),
                    AverageTimePerTradeInHours = table.Column<int>(type: "INTEGER", nullable: false),
                    BarsPerTradeStdDeviation = table.Column<double>(type: "REAL", nullable: true),
                    BarsPerTradeSkewness = table.Column<double>(type: "REAL", nullable: true),
                    BarsPerTradeKurtosis = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    ResultsId = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Profit = table.Column<double>(type: "REAL", nullable: false),
                    DurationInHours = table.Column<double>(type: "REAL", nullable: false),
                    IsOutlierStdDev4 = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => new { x.ResultsId, x.Start });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candles");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
