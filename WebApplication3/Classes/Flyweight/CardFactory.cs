using WebApplication3.Classes.State;

namespace WebApplication3.Classes.Flyweight
{
    public abstract class CardFactory
    {
        protected List<Card> cards = new List<Card>();
        public CardFactory(List<Card> cards) 
        {
            this.cards = cards;
        }
        public abstract Card GetCard(string key, string a1, string a2, string a3, string a4, bool type, Context cont);
    }
}

