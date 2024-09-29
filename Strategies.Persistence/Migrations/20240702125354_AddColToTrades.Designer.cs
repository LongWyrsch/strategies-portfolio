﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Strategies.Persistence;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    [DbContext(typeof(StrategiesContext))]
    [Migration("20240702125354_AddColToTrades")]
    partial class AddColToTrades
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("Strategies.Domain.Candle", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<string>("Exchange")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resolution")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateDownloaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double?>("ADX")
                        .HasColumnType("REAL");

                    b.Property<double?>("ATR")
                        .HasColumnType("REAL");

                    b.Property<double?>("BB_Basis")
                        .HasColumnType("REAL");

                    b.Property<double?>("BB_Lower")
                        .HasColumnType("REAL");

                    b.Property<double?>("BB_Upper")
                        .HasColumnType("REAL");

                    b.Property<double>("Close")
                        .HasColumnType("REAL");

                    b.Property<double>("High")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ichimoku_BaseLine")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ichimoku_ConversionLine")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ichimoku_LaggingSpan")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ichimoku_LeadingSpanA")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ichimoku_LeadingSpanB")
                        .HasColumnType("REAL");

                    b.Property<double>("Low")
                        .HasColumnType("REAL");

                    b.Property<double?>("MACD")
                        .HasColumnType("REAL");

                    b.Property<double?>("MACD_Histogram")
                        .HasColumnType("REAL");

                    b.Property<double?>("MACD_Signal")
                        .HasColumnType("REAL");

                    b.Property<double?>("MA_100")
                        .HasColumnType("REAL");

                    b.Property<double?>("MA_20")
                        .HasColumnType("REAL");

                    b.Property<double?>("MA_200")
                        .HasColumnType("REAL");

                    b.Property<double?>("MA_50")
                        .HasColumnType("REAL");

                    b.Property<double?>("MF")
                        .HasColumnType("REAL");

                    b.Property<double?>("OBV")
                        .HasColumnType("REAL");

                    b.Property<double>("Open")
                        .HasColumnType("REAL");

                    b.Property<double?>("RSI")
                        .HasColumnType("REAL");

                    b.Property<double?>("RSI_BasedMA")
                        .HasColumnType("REAL");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.HasKey("Symbol", "Exchange", "Resolution", "DateDownloaded", "Date");

                    b.ToTable("Candles");
                });

            modelBuilder.Entity("Strategies.Domain.Results", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AverageTimePerTradeInHours")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("BarsPerTradeAverage")
                        .HasColumnType("REAL");

                    b.Property<double?>("BarsPerTradeKurtosis")
                        .HasColumnType("REAL");

                    b.Property<double?>("BarsPerTradeSkewness")
                        .HasColumnType("REAL");

                    b.Property<double?>("BarsPerTradeStdDeviation")
                        .HasColumnType("REAL");

                    b.Property<double?>("BuyAndHoldProfit")
                        .HasColumnType("REAL");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Fee")
                        .HasColumnType("REAL");

                    b.Property<string>("OutlierRemovalMethod")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Performance")
                        .HasColumnType("REAL");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Strategy")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("StrategyProfit")
                        .HasColumnType("REAL");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Timeframe")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TradeCount")
                        .HasColumnType("REAL");

                    b.Property<double?>("TradeProfitAverage")
                        .HasColumnType("REAL");

                    b.Property<double?>("TradeProfitKurtosis")
                        .HasColumnType("REAL");

                    b.Property<double?>("TradeProfitNormality")
                        .HasColumnType("REAL");

                    b.Property<double?>("TradeProfitSkewness")
                        .HasColumnType("REAL");

                    b.Property<double?>("TradeProfitStdDeviation")
                        .HasColumnType("REAL");

                    b.Property<double?>("WinRate")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Strategies.Domain.Trade", b =>
                {
                    b.Property<string>("ResultsId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<double>("DurationInHours")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOutlierStdDev4")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Profit")
                        .HasColumnType("REAL");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Strategy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ResultsId", "Start");

                    b.ToTable("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
