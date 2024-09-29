// using System.Diagnostics;

// using Strategies.Domain.Persistence;
// namespace Strategies.Domain;

// public class SupabaseMigration
// {
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly Supabase.Client _supabaseClient;

//     public SupabaseMigration(IUnitOfWork unitOfWork, Supabase.Client supabaseClient)
//     {
//         _unitOfWork = unitOfWork;
//         _supabaseClient = supabaseClient;
//     }

//     public async Task TransferDataToSupabase()
//     {
//         Console.WriteLine("Starting data transfer...");
//         // Retrieve data from local database
//         var candles = _unitOfWork.Repository<Candle>().GetAllQueryableAsNoTracking().Take(2).ToList();
//         var trades = _unitOfWork.Repository<Trade>().GetAllQueryableAsNoTracking().Take(2).ToList();
//         var results = _unitOfWork.Repository<Results>().GetAllQueryableAsNoTracking().Take(2).ToList();

//         Console.WriteLine($"Retrieved {candles.Count} candles, {trades.Count} trades, and {results.Count} results.");
//         // cast all candle in candles to SupabaseCandle
//         List<SupabaseCandle> supabaseCandles = candles.Select(c => (SupabaseCandle)c).ToList();
//         List<SupabaseTrade> supabaseTrades = trades.Select(t => (SupabaseTrade)t).ToList();
//         List<SupabaseResults> supabaseResults = results.Select(r => (SupabaseResults)r).ToList();


//         Colors colors = new Colors
//         {
//             Color = "Red",
//             Number = 12
//         };

//         await _supabaseClient.From<Colors>().Insert(new Colors { Color = "Yellow", Number = 13 });
//         Debugger.Break();

//         Console.WriteLine("Inserting candles into Supabase...");
//         // console write any candle in supabaseCandles wheere the symbol is empty, null or non-set.
//         foreach (var candle in supabaseCandles)
//         {
//             Console.WriteLine($"Symbol is empty for candle: {candle.Symbol}, {candle.Exchange}, {candle.Resolution}, {candle.DateDownloaded}, {candle.Date}");
//         }

//         // create a supabasecandle object
//         var testSupabaseCandle = new SupabaseCandle
//         {
//             Symbol = "BTC",
//             Exchange = "BINANCE",
//             Resolution = "1h",
//             DateDownloaded = DateTime.Now,
//             Date = DateTime.Now,
//             Open = 1.0,
//             High = 2.0,
//             Low = 0.5,
//             Close = 1.5,
//             Ichimoku_ConversionLine = null,
//             Ichimoku_BaseLine = null,
//             Ichimoku_LaggingSpan = null,
//             Ichimoku_LeadingSpanA = null,
//             Ichimoku_LeadingSpanB = null,
//             MA_20 = null,
//             MA_50 = null,
//             MA_100 = null,
//             MA_200 = null,
//             BB_Basis = null,
//             BB_Upper = null,
//             BB_Lower = null,
//             Volume = 1000,
//             MACD_Histogram = null,
//             MACD = null,
//             MACD_Signal = null,
//             RSI = null,
//             RSI_BasedMA = null,
//             ADX = null,
//             MF = null,
//             OBV = null,
//             ATR = null,
//         };


//         // var responseCandles = await _supabaseClient.From<SupabaseCandle>().Insert(supabaseCandles);
//         // for each candle in supabaseCandles, insert the candle into the supabaseCandles table
//         foreach (var candle in supabaseCandles)
//         {
//             // var responseCandles = await _supabaseClient.From<SupabaseCandle>().Insert(candle);
//             if (!string.IsNullOrEmpty(candle.Symbol))
//             {
//                 try
//                 {
//                     var responseCandles = await _supabaseClient.From<SupabaseCandle>().Insert(testSupabaseCandle);
//                 }
//                 catch (Postgrest.Exceptions.PostgrestException ex)
//                 {
//                     Console.WriteLine($"Error inserting candle: {ex.Message}");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine($"Skipping candle with null or empty symbol: {candle.Symbol}, {candle.Exchange}, {candle.Resolution}, {candle.DateDownloaded}, {candle.Date}");
//             }
//         }
//         // if (!responseCandles.ResponseMessage.IsSuccessStatusCode)
//         // {
//         //     // Console.WriteLine($"Failed to insert candle: {candle.Symbol}, {candle.Exchange}, {candle.Resolution}, {candle.DateDownloaded}, {candle.Date} Error: {response.Content}");
//         //     Console.WriteLine($"Error: {responseCandles.ResponseMessage.ReasonPhrase}");
//         //     Console.WriteLine($"Response content: {responseCandles}");
//         // }

        

//         // try
//         // {
//         //     Console.WriteLine("Inserting candles into Supabase...");
//         //     var responseCandles = await _supabaseClient.From<SupabaseCandle>().Insert(supabaseCandles);
//         //     if (!responseCandles.ResponseMessage.IsSuccessStatusCode)
//         //     {
//         //         Console.WriteLine($"Error: {responseCandles.ResponseMessage.ReasonPhrase}");
//         //         Console.WriteLine($"Response content: {responseCandles}");
//         //     }
//         // }
//         // catch (Exception ex)
//         // {
//         //     Console.WriteLine($"Exception occurred while inserting candles: {ex.Message}");
//         //     if (ex.InnerException != null)
//         //     {
//         //         Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
//         //     }
//         //     Console.WriteLine(ex.StackTrace);
//         // }




//         Console.WriteLine("Inserting trades into Supabase...");
//         var responseTrades = await _supabaseClient.From<SupabaseTrade>().Insert(supabaseTrades);
//         // if (!responseTrades.ResponseMessage.IsSuccessStatusCode)
//         // {
//         //     Console.WriteLine($"Failed to insert trade: {trade.ResultsId}, {trade.Start} Error: {response.Content}");
//         // }

//         Console.WriteLine("Inserting results into Supabase...");
//         var responseResults = await _supabaseClient.From<SupabaseResults>().Insert(supabaseResults);
//         // if (!responseResults.ResponseMessage.IsSuccessStatusCode)
//         // {
//         //     Console.WriteLine($"Failed to insert result: {result.Id}, Error: {response.Content}");
//         // }

//         Console.WriteLine("Data transfer complete.");
//     }
// }
