using Strategies.Domain;

namespace Strategies.Persistence;


internal class ResultsRepository : GenericRepository<Results>
{
    public ResultsRepository(StrategiesContext context) : base(context)
    {
    }
}