using WebApplication3.Classes.Strategy;

namespace WebApplication3.Classes.Flyweight
{
    public abstract class Card
    {
        public string key;
        public int Id;
        public Card(string a1, string a2, string a3, string a4, string key) { }
        public abstract bool Check(string k, string a1, string a2, string a3, string a4, bool type);
    }
}
