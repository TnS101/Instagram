using Domain.Database;

namespace Application.ApplicationLayer.Services
{
    public class ContextService
    {
        public ContextService(DbContext context)
        {
            this.Context = context;
        }

        protected DbContext Context { get; }
    }
}
