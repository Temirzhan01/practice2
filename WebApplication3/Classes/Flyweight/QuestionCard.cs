using WebApplication3.Classes.Strategy;

namespace WebApplication3.Classes.Flyweight
{
    public class QuestionCard : Card
    {
        public List<string> values;
        public QuestionCard(string a1, string a2, string a3, string a4, string key) : base(a1, a2, a3, a4, key)
        {
            Random random = new Random();
            this.Id = random.Next(10000, 100000);
            this.key = key;
            values = new List<string>() { a1, a2, a3, a4 };
        }
        public override bool Check(string k, string a1, string a2, string a3, string a4, bool type) 
        {
            List<string> strings = new List<string>() { a1, a2, a3, a4 };
            bool fl = true;
            for (int i = 0; i < 4; i++) 
            {
                if (this.values[i] != strings[i]) { fl = false;  } 
            }
            if (this.key == k && fl == true && type == false) { return true; }
            else return false;
        }
    }
}
