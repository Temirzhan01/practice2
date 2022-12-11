using WebApplication3.Classes.Strategy;
using WebApplication3.Classes.Intermediate;
using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.Command;
using WebApplication3.Models;

namespace WebApplication3.Classes.State
{
    public class Context
    {
        public CardFactory factory;
        public Invoker invoker;
        public Context(CardFactory cf, Invoker inv) 
        {
            this.invoker = inv;
            this.factory = cf; 
        }
        public void ChangeState(CardFactory cf) 
        {
            this.factory = cf;
        }
        public Card Create(string key, string a1, string a2, string a3, string a4, bool type) 
        {
            return factory.GetCard(key, a1, a2, a3, a4, type, this);
        }
    }
}
