using Strategies.Domain;

namespace Strategies.Persistence;


internal class CandleRepository : GenericRepository<Candle>
{
    public CandleRepository(StrategiesContext context) : base(context)
    {
    }
}