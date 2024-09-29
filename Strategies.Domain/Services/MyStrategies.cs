using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Skender.Stock.Indicators;
using Strategies.Domain.Persistence;
using Strategies.Domain.Presentation;


namespace Strategies.Domain;
public class MyStrategies
{
    private readonly IMessageService _messageService;
    private readonly Dictionary<int, Func<List<Candle>, List<Quote>, double?, IEnumerable<(bool Buy, bool Sell, double? StopLoss)?>>> _strategies;

    public MyStrategies(IMessageService messageService)
    {
        _messageService = messageService;

        _strategies = new Dictionary<int, Func<List<Candle>, List<Quote>, double?, IEnumerable<(bool Buy, bool Sell, double? StopLoss)?>>>
        {
            {1, Strategy1},
            {2, Strategy2},
            {3, Strategy3},
            {4, Strategy4},
            {5, Strategy5},
            {6, Strategy6},
            {7, Strategy7},
            {8, Strategy8},
            {9, Strategy9},
            {10, Strategy10},
            {11, Strategy11},
            {12, Strategy12},
            {13, Strategy13},
            {14, Strategy14},
            {15, Strategy15},
            {16, Strategy16},
            {17, Strategy17},
            {18, Strategy18},
            {19, Strategy19},
            {20, Strategy20},
            {21, Strategy21},
            {22, Strategy22},
            {23, Strategy23},
            {24, Strategy24},
            {25, Strategy25},
            {26, Strategy26},
            {27, Strategy27},
            {28, Strategy28},
            {29, Strategy29},
            {30, Strategy30},
            {31, Strategy31},
            {32, Strategy32},
            {33, Strategy33},
            {34, Strategy34},
            {35, Strategy35},
            {36, Strategy36},
            {37, Strategy37},
            {38, Strategy38},
            {39, Strategy39},
            {40, Strategy40},
            {41, Strategy41},
            {42, Strategy42},
            {43, Strategy43},
            {44, Strategy44},
            {45, Strategy45},
            {46, Strategy46},
            {47, Strategy47},
            {48, Strategy48},
            {49, Strategy49},
            {50, Strategy50},
            {51, Strategy51},
            {52, Strategy52},
            {53, Strategy53},
            {54, Strategy54},
            {55, Strategy55},
            {56, Strategy56},
            {57, Strategy57},
            {58, Strategy58},
            {59, Strategy59},
            {60, Strategy60},
            {61, Strategy61},
            {62, Strategy62},
            {63, Strategy63},
            {64, Strategy64},
            {65, Strategy65},
            {66, Strategy66},
            {67, Strategy67},
            {68, Strategy68},
            {69, Strategy69},
            {70, Strategy70},
            {71, Strategy71},
            {72, Strategy72},
            {73, Strategy73},
            {74, Strategy74},
            {75, Strategy75},
            {76, Strategy76},
            {77, Strategy77},
            {78, Strategy78},
            {79, Strategy79},
            {80, Strategy80},
            {81, Strategy81},
            {82, Strategy82},
            {83, Strategy83},
            {84, Strategy84},
            {85, Strategy85},
            {86, Strategy86},
            {87, Strategy87},
            {88, Strategy88},
            {89, Strategy89},
            {90, Strategy90},
            {91, Strategy91},
            {92, Strategy92},
            {93, Strategy93},
            {94, Strategy94},
            {95, Strategy95},
            {96, Strategy96},
            {97, Strategy97},
            {98, Strategy98},
            {99, Strategy99},
            {100, Strategy100},
            {101, Strategy101},
            {102, Strategy102},
            {103, Strategy103},
            {104, Strategy104},
            {105, Strategy105},
            {106, Strategy106},
            {107, Strategy107},
            {108, Strategy108},
            {109, Strategy109},
            {110, Strategy110},
            {111, Strategy111},
            {112, Strategy112},
            {113, Strategy113},
            {114, Strategy114},
            {115, Strategy115},
            {116, Strategy116},
            {117, Strategy117},
            {118, Strategy118},
            {119, Strategy119},
            {120, Strategy120},
            {121, Strategy121},
            {122, Strategy122},
            {123, Strategy123},
            {124, Strategy124},
            {125, Strategy125},
            {126, Strategy126},
            {127, Strategy127},
            {128, Strategy128},
            {129, Strategy129},
            {130, Strategy130},
            {131, Strategy131},
            {132, Strategy132},
            {133, Strategy133},
            {134, Strategy134},
            {135, Strategy135},
            {136, Strategy136},
            {137, Strategy137},
            {138, Strategy138},
            {139, Strategy139},
            {140, Strategy140},
            {141, Strategy141},
            {142, Strategy142},
            {143, Strategy143},
            {144, Strategy144},
            {145, Strategy145},
            {146, Strategy146},
            {147, Strategy147},
            {148, Strategy148},
            {149, Strategy149},
            {150, Strategy150},
            {151, Strategy151},
            {152, Strategy152},
            {153, Strategy153},
            {154, Strategy154},
            {155, Strategy155},
            {156, Strategy156},
            {157, Strategy157},
            {158, Strategy158},
            {159, Strategy159},
            {160, Strategy160},
            {161, Strategy161},
            {162, Strategy162},
            {163, Strategy163},
            {164, Strategy164},
            {165, Strategy165},
            {166, Strategy166},
            {167, Strategy167},
            {168, Strategy168},
            {169, Strategy169},
            {170, Strategy170},
            {171, Strategy171},
            {172, Strategy172},
            {173, Strategy173},
            {174, Strategy174},
            {175, Strategy175},
            {176, Strategy176},
            {177, Strategy177},
            {178, Strategy178},
            {179, Strategy179},
            {180, Strategy180},
            {181, Strategy181},
            {182, Strategy182},
            {183, Strategy183},
            {184, Strategy184},
            {185, Strategy185},
            {186, Strategy186},
            {187, Strategy187},
            {188, Strategy188},
            {189, Strategy189},
            {190, Strategy190},
            {191, Strategy191},
            {192, Strategy192},
            {193, Strategy193},
            {194, Strategy194},
            {195, Strategy195},
            {196, Strategy196},
            {197, Strategy197},
            {198, Strategy198},
            {199, Strategy199},
            {200, Strategy200},
            {201, Strategy201},
            {202, Strategy202},
            {203, Strategy203},
            {204, Strategy204},
            {205, Strategy205},
            {206, Strategy206},
            {207, Strategy207},
            {208, Strategy208},
            {209, Strategy209},
            {210, Strategy210},
            {211, Strategy211},
            {212, Strategy212},
            {213, Strategy213},
            {214, Strategy214},
            {215, Strategy215},
            {216, Strategy216},
            {217, Strategy217},
            {218, Strategy218},
            {219, Strategy219},
            {220, Strategy220},
            {221, Strategy221},
            {222, Strategy222},
            {223, Strategy223},
            {224, Strategy224},
            {225, Strategy225},
            {226, Strategy226},
            {227, Strategy227},
            {228, Strategy228},
            {229, Strategy229},
            {230, Strategy230},
            {231, Strategy231},
            {232, Strategy232},
            {233, Strategy233},
            {234, Strategy234},
            {235, Strategy235},
            {236, Strategy236},
            {237, Strategy237},
            {238, Strategy238},
            {239, Strategy239},
            {240, Strategy240},
            {241, Strategy241},
            {242, Strategy242},
            {243, Strategy243},
            {244, Strategy244},
            {245, Strategy245},
            {246, Strategy246},
            {247, Strategy247},
            {248, Strategy248},
            {249, Strategy249},
            {250, Strategy250},
            {251, Strategy251},
            {252, Strategy252},
            {253, Strategy253},
            {254, Strategy254},
            {255, Strategy255},
            {256, Strategy256},
            {257, Strategy257},
            {258, Strategy258},
            {259, Strategy259},
            {260, Strategy260},
            {261, Strategy261},
            {262, Strategy262},
            {263, Strategy263},
            {264, Strategy264},
            {265, Strategy265},
            {266, Strategy266},
            {267, Strategy267},
            {268, Strategy268},
            {269, Strategy269},
            {270, Strategy270},
            {271, Strategy271},
            {272, Strategy272},
            {273, Strategy273},
            {274, Strategy274},
            {275, Strategy275},
            {276, Strategy276},
            {277, Strategy277},
            {278, Strategy278},
            {279, Strategy279},
            {280, Strategy280},
            {281, Strategy281},
            {282, Strategy282},
            {283, Strategy283},
            {284, Strategy284},
            {285, Strategy285},
            {286, Strategy286},
            {287, Strategy287},
            {288, Strategy288},
            {289, Strategy289},
            {290, Strategy290},
            {291, Strategy291},
            {292, Strategy292},
            {293, Strategy293},
            {294, Strategy294},
            {295, Strategy295},
            {296, Strategy296},
            {297, Strategy297},
            {298, Strategy298},
            {299, Strategy299},
            {300, Strategy300},
            {301, Strategy301},
            {302, Strategy302},
            {303, Strategy303},
            {304, Strategy304},
            {305, Strategy305},
            {306, Strategy306},
            {307, Strategy307},
            {308, Strategy308},
            {309, Strategy309},
            {310, Strategy310},
            {311, Strategy311},
            {312, Strategy312},
            {313, Strategy313},
            {314, Strategy314},
            {315, Strategy315},
            {316, Strategy316},
            {317, Strategy317},
            {318, Strategy318},
            {319, Strategy319},
        };
    }

    public List<int> GetStrategyIds()
    {
        return _strategies.Keys.ToList();
    }

    public (List<Results> ResultList, List<Trade> TradeList) Run(int strategyId, List<Candle> candles, double fee = 0.001, double? stopLoss = null)
    {
        if (_strategies.TryGetValue(strategyId, out var strategy) == false)
        {
            _messageService.WriteLine($"[red]Strategy {strategyId} not found[/]");
            throw new Exception($"Strategy {strategyId} not found");
        }

        try
        {
            List<Quote> quotes = Utilities.ConvertToQuotes(candles);
            string symbol = candles.First().Symbol;
            string exchange = candles.First().Exchange;
            string resolution = candles.First().Resolution;
            string timeFrame = candles[0].Date + " to " + candles[^1].Date;

            // Set up the metrics
            var trader = new Trader(symbol, exchange, resolution, timeFrame, fee, strategyId, candles[0].Open, candles[^1].Open);

            List<(bool Buy, bool Sell, double? StopLoss)?> conditions = strategy(candles, quotes, stopLoss).ToList();

            for (int i = 0; i < quotes.Count; i++)
            {
                if (conditions[i] == null) continue;
                trader.Trade(
                    openPrice: (double)quotes.ElementAt(i).Open,
                    lowPrice: (double)quotes.ElementAt(i).Low,
                    barIndex: i,
                    conditions[i].Value.Buy,
                    conditions[i].Value.Sell,
                    quotes.ElementAt(i).Date,
                    stopLoss: conditions[i]?.StopLoss
                );
            }

            return trader.GenerateResults();
        }
        catch (Exception ex)
        {
            _messageService.WriteLine($"[red]Error running strategy {strategyId}: {ex.Message}[/]");
            throw;
        }
    }

    private static IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> AboveMovingAverage(List<Candle> candles, List<Quote> quotes, string fastSeriesType, int? fastSeriesLength, string slowSeriesType, int slowSeriesLength)
    {
        if (fastSeriesType != "Close" && fastSeriesLength == null) throw new ArgumentException("fastSeries length must be provided if fastSeries type is not Close");

        List<double?> fastSeries = fastSeriesType switch
        {
            "Close" => quotes.Select(q => (double?)q.Close).ToList(),
            "SMA" => quotes.GetSma((int)fastSeriesLength).Select(s => s.Sma).ToList(),
            "EMA" => quotes.GetEma((int)fastSeriesLength).Select(s => s.Ema).ToList(),
            _ => throw new ArgumentException("Invalid series1 type")
        };

        List<double?> slowSeries = slowSeriesType switch
        {
            "Close" => quotes.Select(q => (double?)q.Close).ToList(),
            "SMA" => quotes.GetSma(slowSeriesLength).Select(s => s.Sma).ToList(),
            "EMA" => quotes.GetEma(slowSeriesLength).Select(s => s.Ema).ToList(),
            _ => throw new ArgumentException("Invalid slowSeries type")
        };

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (fastSeries[i - 1] == null || slowSeries[i - 1] == null) return null;

            bool aboveMA = fastSeries[i - 1] > slowSeries[i - 1];
            bool buyCondition = aboveMA;
            bool sellCondition = !aboveMA;

            return (buyCondition, sellCondition, null);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy1(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "SMA", 20);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy2(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "SMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy3(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "SMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy4(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy5(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "EMA", 20);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy6(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "EMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy7(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "EMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy8(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "Close", null, "EMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy9(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 20, "SMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy10(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 20, "SMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy11(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 20, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy12(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 50, "SMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy13(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 50, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy14(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "SMA", 100, "SMA", 200);
    }

    private static IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> IncreasingDelta(List<Candle> candles, List<Quote> quotes, string fastSeriesType, int fastSeriesLength, string slowSeriesType, int slowSeriesLength)
    {
        List<double?> fastSeries = fastSeriesType switch
        {
            "SMA" => quotes.GetSma(fastSeriesLength).Select(s => s.Sma).ToList(),
            "EMA" => quotes.GetEma(fastSeriesLength).Select(s => s.Ema).ToList(),
            _ => throw new ArgumentException("Invalid series1 type")
        };

        List<double?> slowSeries = slowSeriesType switch
        {
            "SMA" => quotes.GetSma(slowSeriesLength).Select(s => s.Sma).ToList(),
            "EMA" => quotes.GetEma(slowSeriesLength).Select(s => s.Ema).ToList(),
            _ => throw new ArgumentException("Invalid series2 type")
        };

        List<double?> delta = [];
        for (int i = 0; i < quotes.Count; i++)
            delta.Add(fastSeries[i] - slowSeries[i]);

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (delta[i - 2] == null || delta[i - 1] == null) return null;

            bool increasingDelta = delta[i - 2] < delta[i - 1];
            bool buyCondition = increasingDelta;
            bool sellCondition = !increasingDelta;

            return (buyCondition, sellCondition, null);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy15(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 20, "SMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy16(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 20, "SMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy17(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 20, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy18(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 50, "SMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy19(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 50, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy20(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "SMA", 100, "SMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy21(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 20, "EMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy22(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 20, "EMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy23(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 20, "EMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy24(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 50, "EMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy25(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 50, "EMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy26(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return AboveMovingAverage(candles, quotes, "EMA", 100, "EMA", 200);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy27(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        List<double?> fastEmaResults = quotes.GetEma(100).Select(ema => ema.Ema).ToList();
        List<double?> slowEmaResults = quotes.GetEma(200).Select(ema => ema.Ema).ToList();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (fastEmaResults[i - 2] == null || fastEmaResults[i - 1] == null || slowEmaResults[i - 2] == null || slowEmaResults[i - 1] == null) return null;

            bool fastSlowEmaCrossOver = fastEmaResults[i - 2] <= slowEmaResults[i - 2] && fastEmaResults[i - 1] > slowEmaResults[i - 1];
            bool fastSlowEmaCrossUnder = fastEmaResults[i - 2] > slowEmaResults[i - 2] && fastEmaResults[i - 1] <= slowEmaResults[i - 1];
            bool buyCondition = fastSlowEmaCrossOver;
            bool sellCondition = fastSlowEmaCrossUnder;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy28(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "EMA", 20, "EMA", 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy29(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "EMA", 20, "EMA", 100);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy30(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return IncreasingDelta(candles, quotes, "EMA", 50, "EMA", 100);
    }

    private static IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> HmaGoesUp(List<Candle> candles, List<Quote> quotes, int period1, int? period2)
    {
        List<double?> hmaResults1 = quotes.GetHma(period1).Select(hma => hma.Hma).ToList();
        List<double?> hmaResults2 = period2 == null ? Enumerable.Repeat<double?>(null, candles.Count).ToList() : quotes.GetHma((int)period2).Select(hma => hma.Hma).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (hmaResults1[i - 2] == null || hmaResults1[i - 1] == null) return null;
            if (period2 != null)
                if (hmaResults2[i - 2] == null || hmaResults2[i - 1] == null) return null;

            bool hma1Increasing = hmaResults1[i - 2] < hmaResults1[i - 1];
            bool hma2Increasing = period2 == null ? true : hmaResults2[i - 2] < hmaResults2[i - 1]; // if period2 is null, set to true to ignore it

            bool buyCondition = hma1Increasing && hma2Increasing;
            bool sellCondition = !hma1Increasing || !hma2Increasing;

            return (buyCondition, sellCondition, null);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy31(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 9, null);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy32(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 20, null);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy33(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 50, null);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy34(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 100, null);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy35(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 9, 20);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy36(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 9, 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy37(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        return HmaGoesUp(candles, quotes, 20, 50);
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy38(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (macdResults.ElementAt(i - 1).Macd == null) return null;

            bool macdPositive = macdResults.ElementAt(i - 1).Macd > 0;
            bool buyCondition = macdPositive;
            bool sellCondition = !macdPositive;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy39(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (macdResults.ElementAt(i - 1).Signal == null) return null;

            bool signalPositive = macdResults.ElementAt(i - 1).Signal > 0;
            bool buyCondition = signalPositive;
            bool sellCondition = !signalPositive;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy40(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (macdResults.ElementAt(i - 1).Macd == null || macdResults.ElementAt(i - 1).Signal == null) return null;

            bool macdAboveSignal = macdResults.ElementAt(i - 1).Macd > macdResults.ElementAt(i - 1).Signal;
            bool buyCondition = macdAboveSignal;
            bool sellCondition = !macdAboveSignal;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy41(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (macdResults.ElementAt(i - 1).Macd == null || macdResults.ElementAt(i - 1).Signal == null) return null;

            bool macdSignalCrossOver = macdResults.ElementAt(i - 2).Macd <= macdResults.ElementAt(i - 2).Signal && macdResults.ElementAt(i - 1).Macd > macdResults.ElementAt(i - 1).Signal;
            bool macdSignalCrossUnder = macdResults.ElementAt(i - 2).Macd > macdResults.ElementAt(i - 2).Signal && macdResults.ElementAt(i - 1).Macd <= macdResults.ElementAt(i - 1).Signal;
            bool buyCondition = macdSignalCrossOver;
            bool sellCondition = macdSignalCrossUnder;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy42(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy38 = Strategy38(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy38[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy38[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy38[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy43(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy39[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy39[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy44(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy38 = Strategy38(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy38[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy38[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy38[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy45(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy39[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy39[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy46(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy26[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy26[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy47(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy26[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy26[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy48(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy24 = Strategy24(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy24[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy24[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy24[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy49(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy24 = Strategy24(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy24[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy24[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy24[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy50(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy51(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy52(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy39[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy39[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy53(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null || strategy39[i] == null) return null;

            bool buyCondition = strategy27[i].Value.Buy && strategy39[i].Value.Buy;
            bool sellCondition = strategy27[i].Value.Sell || strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy54(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy55(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy27[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy27[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy56(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy1 = Strategy1(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy1[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy1[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy1[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy57(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy2 = Strategy2(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy2[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy2[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy2[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy58(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy3 = Strategy3(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy3[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy3[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy3[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy59(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy5 = Strategy5(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy5[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy5[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy5[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy60(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy6 = Strategy6(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy6[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy6[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy6[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy61(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy7 = Strategy7(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy7[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy7[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy7[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy62(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy9 = Strategy9(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy9[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy9[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy9[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy63(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy10 = Strategy10(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy10[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy10[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy10[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy64(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy11 = Strategy11(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy11[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy11[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy11[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy65(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy12 = Strategy12(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy12[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy12[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy12[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy66(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy13 = Strategy13(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy13[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy13[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy13[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy67(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy15 = Strategy15(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy15[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy15[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy15[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy68(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy16 = Strategy16(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy16[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy16[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy16[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy69(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy17 = Strategy17(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy17[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy17[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy17[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy70(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy21 = Strategy21(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy21[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy21[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy21[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy71(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy22 = Strategy22(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy22[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy22[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy22[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy72(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy23 = Strategy23(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy23[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy23[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy23[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy73(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy24 = Strategy24(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy24[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy24[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy24[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy74(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy25 = Strategy25(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy25[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy25[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy25[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy75(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy28 = Strategy28(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy28[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy28[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy28[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy76(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy29 = Strategy29(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy29[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy29[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy29[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy77(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy30 = Strategy30(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy30[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy30[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy30[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy78(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy31 = Strategy31(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy31[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy31[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy31[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy79(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy32 = Strategy32(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy32[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy32[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy32[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy80(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy33 = Strategy33(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy33[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy33[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy33[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy81(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy34 = Strategy34(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy34[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy34[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy34[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy82(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy35 = Strategy35(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy35[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy35[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy35[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy83(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy36 = Strategy36(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy36[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy36[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy36[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy84(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy37 = Strategy37(candles, quotes, stopLoss).ToList();
        var strategy19 = Strategy19(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy37[i] == null || strategy19[i] == null) return null;

            bool buyCondition = strategy37[i].Value.Buy && strategy19[i].Value.Buy;
            bool sellCondition = strategy37[i].Value.Sell || strategy19[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy85(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy1 = Strategy1(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy1[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy1[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy1[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy86(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy2 = Strategy2(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy2[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy2[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy2[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy87(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy3 = Strategy3(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy3[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy3[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy3[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy88(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy5 = Strategy5(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy5[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy5[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy5[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy89(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy6 = Strategy6(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy6[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy6[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy6[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy90(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy7 = Strategy7(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy7[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy7[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy7[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy91(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy9 = Strategy9(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy9[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy9[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy9[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy92(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy10 = Strategy10(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy10[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy10[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy10[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy93(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy11 = Strategy11(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy11[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy11[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy11[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy94(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy12 = Strategy12(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy12[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy12[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy12[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy95(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy13 = Strategy13(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy13[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy13[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy13[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy96(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy15 = Strategy15(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy15[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy15[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy15[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy97(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy16 = Strategy16(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy16[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy16[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy16[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy98(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy17 = Strategy17(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy17[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy17[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy17[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy99(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy21 = Strategy21(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy21[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy21[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy21[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy100(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy22 = Strategy22(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy22[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy22[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy22[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy101(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy23 = Strategy23(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy23[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy23[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy23[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy102(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy48 = Strategy48(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy48[i] == null) return null;

            bool buyCondition = strategy48[i].Value.Buy;
            bool sellCondition = strategy48[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy103(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy25 = Strategy25(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy25[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy25[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy25[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy104(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy28 = Strategy28(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy28[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy28[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy28[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy105(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy29 = Strategy29(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy29[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy29[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy29[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy106(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy30 = Strategy30(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy30[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy30[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy30[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy107(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy31 = Strategy31(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy31[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy31[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy31[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy108(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy32 = Strategy32(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy32[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy32[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy32[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy109(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy33 = Strategy33(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy33[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy33[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy33[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy110(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy34 = Strategy34(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy34[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy34[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy34[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy111(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy35 = Strategy35(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy35[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy35[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy35[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy112(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy36 = Strategy36(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy36[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy36[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy36[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy113(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy40 = Strategy40(candles, quotes, stopLoss).ToList();
        var strategy37 = Strategy37(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy40[i] == null || strategy37[i] == null) return null;

            bool buyCondition = strategy40[i].Value.Buy && strategy37[i].Value.Buy;
            bool sellCondition = strategy40[i].Value.Sell || strategy37[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy114(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy1 = Strategy1(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy1[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy1[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy1[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy115(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy2 = Strategy2(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy2[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy2[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy2[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy116(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy3 = Strategy3(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy3[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy3[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy3[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy117(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy5 = Strategy5(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy5[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy5[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy5[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy118(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy6 = Strategy6(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy6[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy6[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy6[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy119(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy7 = Strategy7(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy7[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy7[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy7[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy120(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy9 = Strategy9(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy9[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy9[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy9[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy121(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy10 = Strategy10(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy10[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy10[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy10[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy122(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy11 = Strategy11(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy11[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy11[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy11[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy123(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy12 = Strategy12(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy12[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy12[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy12[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy124(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy13 = Strategy13(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy13[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy13[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy13[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy125(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy15 = Strategy15(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy15[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy15[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy15[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy126(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy16 = Strategy16(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy16[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy16[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy16[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy127(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy17 = Strategy17(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy17[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy17[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy17[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy128(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy21 = Strategy21(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy21[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy21[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy21[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy129(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy22 = Strategy22(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy22[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy22[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy22[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy130(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy23 = Strategy23(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy23[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy23[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy23[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy131(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy49 = Strategy49(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy49[i] == null) return null;

            bool buyCondition = strategy49[i].Value.Buy;
            bool sellCondition = strategy49[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy132(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy25 = Strategy25(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy25[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy25[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy25[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy133(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy28 = Strategy28(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy28[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy28[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy28[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy134(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy29 = Strategy29(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy29[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy29[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy29[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy135(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy30 = Strategy30(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy30[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy30[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy30[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy136(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy31 = Strategy31(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy31[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy31[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy31[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy137(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy32 = Strategy32(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy32[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy32[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy32[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy138(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy33 = Strategy33(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy33[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy33[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy33[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy139(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy34 = Strategy34(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy34[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy34[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy34[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy140(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy35 = Strategy35(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy35[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy35[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy35[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy141(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy36 = Strategy36(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy36[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy36[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy36[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy142(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy41 = Strategy41(candles, quotes, stopLoss).ToList();
        var strategy37 = Strategy37(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy41[i] == null || strategy37[i] == null) return null;

            bool buyCondition = strategy41[i].Value.Buy && strategy37[i].Value.Buy;
            bool sellCondition = strategy41[i].Value.Sell || strategy37[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy143(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (macdResults.ElementAt(i - 2).Histogram == null || macdResults.ElementAt(i - 1).Histogram == null) return null;

            bool histIncreasing = macdResults.ElementAt(i - 2).Histogram < macdResults.ElementAt(i - 1).Histogram;
            bool buyCondition = histIncreasing;
            bool sellCondition = !histIncreasing;

            return (buyCondition, sellCondition, null);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy144(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy38 = Strategy38(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy38[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy38[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy38[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy145(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy39[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy39[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy146(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy1 = Strategy1(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy1[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy1[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy1[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy147(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy2 = Strategy2(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy2[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy2[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy2[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy148(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy3 = Strategy3(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy3[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy3[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy3[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy149(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy5 = Strategy5(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy5[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy5[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy5[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy150(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy6 = Strategy6(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy6[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy6[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy6[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy151(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy7 = Strategy7(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy7[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy7[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy7[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy152(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy9 = Strategy9(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy9[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy9[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy9[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy153(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy10 = Strategy10(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy10[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy10[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy10[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy154(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy11 = Strategy11(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy11[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy11[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy11[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy155(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy12 = Strategy12(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy12[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy12[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy12[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy156(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy13 = Strategy13(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy13[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy13[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy13[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy157(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy15 = Strategy15(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy15[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy15[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy15[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy158(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy16 = Strategy16(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy16[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy16[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy16[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy159(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy17 = Strategy17(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy17[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy17[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy17[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy160(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy21 = Strategy21(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy21[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy21[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy21[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy161(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy22 = Strategy22(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy22[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy22[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy22[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy162(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy23 = Strategy23(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy23[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy23[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy23[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy163(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy24 = Strategy24(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy24[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy24[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy24[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy164(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy25 = Strategy25(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy25[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy25[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy25[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy165(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy28 = Strategy28(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy28[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy28[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy28[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy166(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy29 = Strategy29(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy29[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy29[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy29[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy167(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy30 = Strategy30(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy30[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy30[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy30[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy168(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy31 = Strategy31(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy31[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy31[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy31[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy169(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy32 = Strategy32(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy32[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy32[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy32[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy170(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy33 = Strategy33(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy33[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy33[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy33[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy171(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy34 = Strategy34(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy34[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy34[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy34[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy172(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy35 = Strategy35(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy35[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy35[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy35[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy173(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy36 = Strategy36(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy36[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy36[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy36[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy174(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy143 = Strategy143(candles, quotes, stopLoss).ToList();
        var strategy37 = Strategy37(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy143[i] == null || strategy37[i] == null) return null;

            bool buyCondition = strategy143[i].Value.Buy && strategy37[i].Value.Buy;
            bool sellCondition = strategy143[i].Value.Sell || strategy37[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy175(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy176(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy177(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy178(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy179(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy180(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy181(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell;

            return (buyCondition, sellCondition, 0.98);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy182(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy183(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy184(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy185(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy186(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy187(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy188(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell;

            return (buyCondition, sellCondition, 0.95);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy189(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy190(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy191(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy192(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy193(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy194(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy195(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell;

            return (buyCondition, sellCondition, 0.90);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy196(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy197(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy198(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy199(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy200(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy201(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy202(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell;

            return (buyCondition, sellCondition, 0.80);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy203(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<AtrStopResult> atrStopResults = quotes.GetATRTrailingStopModified(21, 3);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (atrStopResults.ElementAt(i).AtrStop == null) return null;

            bool closeAboveAtr = quotes.ElementAt(i - 1).Close > atrStopResults.ElementAt(i - 1).AtrStop;
            bool buyCondition = closeAboveAtr;
            bool sellCondition = !closeAboveAtr;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy204(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<AtrStopResult> atrStopResults = quotes.GetATRTrailingStopModified(21, 4);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (atrStopResults.ElementAt(i).AtrStop == null) return null;

            bool closeAboveAtr = quotes.ElementAt(i - 1).Close > atrStopResults.ElementAt(i - 1).AtrStop;
            bool buyCondition = closeAboveAtr;
            bool sellCondition = !closeAboveAtr;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy205(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<AtrStopResult> atrStopResults = quotes.GetATRTrailingStopModified(21, 5);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (atrStopResults.ElementAt(i).AtrStop == null) return null;

            bool closeAboveAtr = quotes.ElementAt(i - 1).Close > atrStopResults.ElementAt(i - 1).AtrStop;
            bool buyCondition = closeAboveAtr;
            bool sellCondition = !closeAboveAtr;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy206(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy205 = Strategy205(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy205[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy205[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy205[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy207(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy205 = Strategy205(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy205[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy205[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy205[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy208(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy205 = Strategy205(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy205[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy205[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy205[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy209(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy205 = Strategy205(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy205[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy205[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy205[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy210(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy204 = Strategy204(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy204[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy204[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy204[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy211(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy203 = Strategy203(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy203[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy203[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy203[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy212(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy203 = Strategy203(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy203[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy203[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy203[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy213(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (rsiResults.ElementAt(i - 1).Rsi == null) return null;

            bool ranging = rsiResults.ElementAt(i - 1).Rsi > 40 && rsiResults.ElementAt(i - 1).Rsi < 60;
            bool buyCondition = !ranging;
            bool sellCondition = ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy214(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (rsiResults.ElementAt(i - 1).Rsi == null) return null;

            bool ranging = rsiResults.ElementAt(i - 1).Rsi > 30 && rsiResults.ElementAt(i - 1).Rsi < 70;
            bool buyCondition = !ranging;
            bool sellCondition = ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy215(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy216(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy217(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy218(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy219(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy220(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy221(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy213 = Strategy213(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy213[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy213[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy213[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy222(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy214[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy223(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy214 == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy224(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy214[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy225(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy214[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy226(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy214[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy227(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy214 == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy228(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy214 = Strategy214(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy214 == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy214[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy214[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy229(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<AdxResult> adxResults = quotes.GetAdxModified(14, 14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (adxResults.ElementAt(i - 1).Adx == null) return null;

            bool isRanging = adxResults.ElementAt(i - 1).Adx < 25;
            bool buyCondition = !isRanging;
            bool sellCondition = isRanging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy230(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<AdxResult> adxResults = quotes.GetAdxModified(25, 25);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (adxResults.ElementAt(i - 1).Adx == null) return null;

            bool isRanging = adxResults.ElementAt(i - 1).Adx < 25;
            bool buyCondition = !isRanging;
            bool sellCondition = isRanging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy231(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<AdxResult> adxResults = quotes.GetAdxModified(40, 40);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (adxResults.ElementAt(i - 1).Adx == null) return null;

            bool isRanging = adxResults.ElementAt(i - 1).Adx < 25;
            bool buyCondition = !isRanging;
            bool sellCondition = isRanging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy232(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy233(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy234(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy235(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy236(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy237(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy238(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy229 = Strategy229(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy229[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy229[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy229[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy239(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy240(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy241(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy242(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy243(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy244(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy245(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy230 = Strategy230(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy230[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy230[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy230[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy246(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy247(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy248(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy249(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy250(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy251(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy252(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy231 = Strategy231(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy231[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy231[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy231[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy253(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (rsiResults.ElementAt(i).Rsi == null) return null;

            bool hasMomentum = rsiResults.ElementAt(i - 1).Rsi > 50;
            bool buyCondition = hasMomentum;
            bool sellCondition = !hasMomentum;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy254(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (rsiResults.ElementAt(i).Rsi == null) return null;

            bool hasMomentum = rsiResults.ElementAt(i - 1).Rsi > 60;
            bool buyCondition = hasMomentum;
            bool sellCondition = !hasMomentum;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy255(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy256(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy257(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy258(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy259(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy260(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy261(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy253 = Strategy253(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy253[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy253[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy253[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy262(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy263(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy264(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy265(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy266(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy267(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy268(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy254 = Strategy254(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy254[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy254[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy254[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy269(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<MfiResult> mfiResults = quotes.GetMfi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (mfiResults.ElementAt(i).Mfi == null) return null;

            bool ranging = mfiResults.ElementAt(i - 1).Mfi > 40 && mfiResults.ElementAt(i - 1).Mfi < 60;
            bool buyCondition = !ranging;
            bool sellCondition = ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy270(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<MfiResult> mfiResults = quotes.GetMfi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (mfiResults.ElementAt(i).Mfi == null) return null;

            bool ranging = mfiResults.ElementAt(i - 1).Mfi > 30 && mfiResults.ElementAt(i - 1).Mfi < 70;
            bool buyCondition = !ranging;
            bool sellCondition = ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy271(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy272(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy273(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy274(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy275(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy276(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy277(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy269 = Strategy269(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy269[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy269[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy269[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy278(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy279(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy280(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy281(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy282(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy283(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy284(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy270 = Strategy270(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy270[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy270[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy270[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy285(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<MfiResult> mfiResults = quotes.GetMfi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (mfiResults.ElementAt(i).Mfi == null) return null;

            bool hasMomentum = mfiResults.ElementAt(i - 1).Mfi > 50;
            bool buyCondition = hasMomentum;
            bool sellCondition = !hasMomentum;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy286(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        IEnumerable<MfiResult> mfiResults = quotes.GetMfi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (mfiResults.ElementAt(i).Mfi == null) return null;

            bool hasMomentum = mfiResults.ElementAt(i - 1).Mfi > 60;
            bool buyCondition = hasMomentum;
            bool sellCondition = !hasMomentum;

            return (buyCondition, sellCondition, stopLoss);
        });
    }

    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy287(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy288(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy289(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy290(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy291(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy292(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy293(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy285 = Strategy285(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy285[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy285[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy285[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy294(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy55 = Strategy55(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy55[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy55[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy55[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy295(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy160 = Strategy160(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy160[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy160[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy160[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy296(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy26 = Strategy26(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy26[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy26[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy26[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy297(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy53 = Strategy53(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy53[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy53[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy53[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy298(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy39 = Strategy39(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy39[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy39[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy39[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy299(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy110 = Strategy110(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy110[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy110[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy110[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy300(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        var strategy103 = Strategy103(candles, quotes, stopLoss).ToList();
        var strategy286 = Strategy286(candles, quotes, stopLoss).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy103[i] == null || strategy286[i] == null) return null;

            bool buyCondition = strategy103[i].Value.Buy && strategy286[i].Value.Buy;
            bool sellCondition = strategy103[i].Value.Sell || strategy286[i].Value.Sell;

            return (buyCondition, sellCondition, stopLoss);
        });
    }



    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy301(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<SmaResult> fastSmaResults = quotes.GetObv().GetSma(50);
        IEnumerable<SmaResult> slowSmaResults = quotes.GetObv().GetSma(200);
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi(14);


        List<double?> smaDelta = [];
        for (int i = 0; i < quotes.Count; i++)
            smaDelta.Add(fastSmaResults.ElementAt(i).Sma - slowSmaResults.ElementAt(i).Sma);


        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                fastEmaResults.ElementAt(i - 2).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 2).Ema == null ||
                rsiResults.ElementAt(i - 1).Rsi == null ||
                smaDelta.ElementAt(i - 1) == null ||
                smaDelta.ElementAt(i - 2) == null
            ) return null;


            bool ranging = rsiResults.ElementAt(i - 1).Rsi > 30 && rsiResults.ElementAt(i - 1).Rsi < 70;
            bool emaCrossOver = fastEmaResults.ElementAt(i - 2).Ema <= slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool emaCrossUnder = fastEmaResults.ElementAt(i - 2).Ema > slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema <= slowEmaResults.ElementAt(i - 1).Ema;
            bool increasingSmaDelta = smaDelta.ElementAt(i - 2) < smaDelta.ElementAt(i - 1);
            bool buyCondition = emaCrossOver && increasingSmaDelta && !ranging;
            bool sellCondition = emaCrossUnder || !increasingSmaDelta || ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy302(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetObv().GetMacd();
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi(14);


        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                macdResults.ElementAt(i - 1).Signal == null ||
                rsiResults.ElementAt(i - 1).Rsi == null ||
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                fastEmaResults.ElementAt(i - 2).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 2).Ema == null
            ) return null;

            bool ranging = rsiResults.ElementAt(i - 1).Rsi > 30 && rsiResults.ElementAt(i - 1).Rsi < 70;
            bool emaCrossOver = fastEmaResults.ElementAt(i - 2).Ema <= slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool emaCrossUnder = fastEmaResults.ElementAt(i - 2).Ema > slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema <= slowEmaResults.ElementAt(i - 1).Ema;
            bool signalPositive = macdResults.ElementAt(i - 1).Signal > 0;
            bool buyCondition = emaCrossOver && signalPositive && !ranging;
            bool sellCondition = emaCrossUnder || !signalPositive || ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy303(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<SmaResult> fastSmaResults = quotes.GetObv().GetSma(50);
        IEnumerable<SmaResult> slowSmaResults = quotes.GetObv().GetSma(200);
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);
        IEnumerable<AdxResult> adxResults = quotes.GetAdxModified(40);

        List<double?> smaDelta = [];
        for (int i = 0; i < quotes.Count; i++)
            smaDelta.Add(fastSmaResults.ElementAt(i).Sma - slowSmaResults.ElementAt(i).Sma);


        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                fastEmaResults.ElementAt(i - 2).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 2).Ema == null ||
                adxResults.ElementAt(i - 1).Adx == null ||
                smaDelta.ElementAt(i - 1) == null ||
                smaDelta.ElementAt(i - 2) == null
            ) return null;



            bool ranging = adxResults.ElementAt(i - 1).Adx < 25;
            bool emaCrossOver = fastEmaResults.ElementAt(i - 2).Ema <= slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool emaCrossUnder = fastEmaResults.ElementAt(i - 2).Ema > slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema <= slowEmaResults.ElementAt(i - 1).Ema;
            bool increasingSmaDelta = smaDelta.ElementAt(i - 2) < smaDelta.ElementAt(i - 1);
            bool buyCondition = emaCrossOver && increasingSmaDelta && !ranging;
            bool sellCondition = emaCrossUnder || !increasingSmaDelta || ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy304(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetObv().GetMacd();
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);
        IEnumerable<AdxResult> adxResults = quotes.GetAdxModified(40);


        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                macdResults.ElementAt(i - 1).Signal == null ||
                adxResults.ElementAt(i - 1).Adx == null ||
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                fastEmaResults.ElementAt(i - 2).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 2).Ema == null
            ) return null;

            bool ranging = adxResults.ElementAt(i - 1).Adx < 25;
            bool emaCrossOver = fastEmaResults.ElementAt(i - 2).Ema <= slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool emaCrossUnder = fastEmaResults.ElementAt(i - 2).Ema > slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema <= slowEmaResults.ElementAt(i - 1).Ema;
            bool signalPositive = macdResults.ElementAt(i - 1).Signal > 0;
            bool buyCondition = emaCrossOver && signalPositive && !ranging;
            bool sellCondition = emaCrossUnder || !signalPositive || ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy305(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (fastEmaResults.ElementAt(i - 1).Ema == null || slowEmaResults.ElementAt(i - 1).Ema == null) return null;

            bool fastAboveSlowEMA = fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool buyCondition = fastAboveSlowEMA;
            bool sellCondition = !fastAboveSlowEMA;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy306(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetObv().GetMacd();
        IEnumerable<EmaResult> fastEmaResults = quotes.GetObv().GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetObv().GetEma(200);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                macdResults.ElementAt(i - 1).Signal == null ||
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                fastEmaResults.ElementAt(i - 2).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 2).Ema == null
            ) return null;

            bool emaCrossOver = fastEmaResults.ElementAt(i - 2).Ema <= slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool emaCrossUnder = fastEmaResults.ElementAt(i - 2).Ema > slowEmaResults.ElementAt(i - 2).Ema && fastEmaResults.ElementAt(i - 1).Ema <= slowEmaResults.ElementAt(i - 1).Ema;
            bool signalPositive = macdResults.ElementAt(i - 1).Signal > 0;
            bool buyCondition = emaCrossOver && signalPositive;
            bool sellCondition = emaCrossUnder || !signalPositive;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy307(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetObv().GetMacd();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (macdResults.ElementAt(i - 1).Signal == null) return null;

            bool signalPositive = macdResults.ElementAt(i - 1).Signal > 0;
            bool buyCondition = signalPositive;
            bool sellCondition = !signalPositive;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy308(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<MacdResult> macdResults = quotes.GetObv().GetMacd();
        IEnumerable<HmaResult> hmaResults = quotes.GetObv().GetHma(100);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (
                hmaResults.ElementAt(i - 1).Hma == null ||
                hmaResults.ElementAt(i - 2).Hma == null ||
                macdResults.ElementAt(i - 1).Macd == null ||
                macdResults.ElementAt(i - 1).Signal == null
            ) return null;

            bool macdAboveSignal = macdResults.ElementAt(i - 1).Macd > macdResults.ElementAt(i - 1).Signal;
            bool hmaIncreasing = hmaResults.ElementAt(i - 2).Hma < hmaResults.ElementAt(i - 1).Hma;
            bool buyCondition = hmaIncreasing && macdAboveSignal;
            bool sellCondition = !hmaIncreasing || !macdAboveSignal;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy309(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<EmaResult> fastEmaResults = quotes.GetEma(100);
        IEnumerable<EmaResult> slowEmaResults = quotes.GetEma(200);
        IEnumerable<RsiResult> rsiResults = quotes.GetRsi(14);

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 1) return null;
            if (
                fastEmaResults.ElementAt(i - 1).Ema == null ||
                slowEmaResults.ElementAt(i - 1).Ema == null ||
                rsiResults.ElementAt(i - 1).Rsi == null
            ) return null;

            bool ranging = rsiResults.ElementAt(i - 1).Rsi > 40 && rsiResults.ElementAt(i - 1).Rsi < 60;
            bool fastAboveSlowEMA = fastEmaResults.ElementAt(i - 1).Ema > slowEmaResults.ElementAt(i - 1).Ema;
            bool buyCondition = fastAboveSlowEMA && !ranging;
            bool sellCondition = !fastAboveSlowEMA || ranging;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy310(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Set up indicators
        IEnumerable<HeikinAshiResult> heikinAshiResultResultsResults = quotes.GetHeikinAshi();

        // Set up buy/sell conditions
        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (i < 2) return null;
            if (heikinAshiResultResultsResults.ElementAt(i).Open == null || heikinAshiResultResultsResults.ElementAt(i).Close == null) return null;

            bool green = heikinAshiResultResultsResults.ElementAt(i - 1).Close > heikinAshiResultResultsResults.ElementAt(i - 1).Open;
            bool buyCondition = green;
            bool sellCondition = !green;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy311(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the hourly, 20 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 3360; //20weeks = 3360h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy312(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 3h, 20 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 1120; //20weeks = 1120*3h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy313(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 6h, 20 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 560; //20weeks = 560*6h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy314(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the hourly, 50 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 8400; //50weeks = 8400h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy315(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 3h, 50 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 2800; //50weeks = 2800*3h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy316(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 6h, 50 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 1400; //50weeks = 1400*6h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy317(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the hourly, 100 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 16800; //100weeks = 16800h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy318(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 3h, 100 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 5600; //100weeks = 5600*3h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
    private IEnumerable<(bool Buy, bool Sell, double? StopLoss)?> Strategy319(List<Candle> candles, List<Quote> quotes, double? stopLoss)
    {
        // Targetting signals on the 6h, 100 EMA on the weekly
        var strategy27 = Strategy27(candles, quotes, stopLoss).ToList();

        var tagetLength = 2800; //100weeks = 2800*6h
        var closeAboveEma = AboveMovingAverage(candles, quotes, "Close", null, "EMA", tagetLength).ToList();

        return candles.Select<Candle, (bool, bool, double? StopLoss)?>((c, i) =>
        {
            if (strategy27[i] == null) return null;

            bool closeAboveEmaBuyCondition = i<tagetLength? true : closeAboveEma[i].Value.Buy;
            bool closeAboveEmaSellCondition = i<tagetLength? false : closeAboveEma[i].Value.Sell;

            bool buyCondition = strategy27[i].Value.Buy && closeAboveEmaBuyCondition;
            bool sellCondition = strategy27[i].Value.Sell || closeAboveEmaSellCondition;

            return (buyCondition, sellCondition, stopLoss);
        });
    }
}