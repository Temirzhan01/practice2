using WebApplication3.Classes.Strategy;

namespace WebApplication3.Classes.Flyweight
{
    public class TextCard : Card
    {
        public string value;
        public TextCard(string a1, string a2, string a3, string a4, string key) : base(a1, a2, a3, a4, key)
        {
            Random random = new Random();
            this.Id = random.Next(10000, 100000);
            this.key = key;
            value = a1;
        }
        public override bool Check(string k, string a1, string a2, string a3, string a4, bool type) 
        {
            if (this.key == k && this.value == a1 && type == true) 
            {
                return true;
            } 
            else return false;
        }
    }
}
