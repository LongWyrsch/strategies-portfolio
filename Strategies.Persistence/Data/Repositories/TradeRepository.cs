using Strategies.Domain;

namespace Strategies.Persistence;


internal class TradeRepository : GenericRepository<Trade>
{
    public TradeRepository(StrategiesContext context) : base(context)
    {
    }
}