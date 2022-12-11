using WebApplication3.Models;
using WebApplication3.Classes.Intermediate;
using WebApplication3.Classes.State;

namespace WebApplication3.Classes.Strategy
{
    public interface IStrategy
    {
        public Task Creating(bool type, string key, string a1, string a2, string a3, string a, Context c);
        public IEnumerable<Connecter> Showing(IEnumerable<Cardjson> cardjsons);
    }
}
