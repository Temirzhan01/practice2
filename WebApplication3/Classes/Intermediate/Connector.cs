using WebApplication3.Classes.Flyweight;

namespace WebApplication3.Classes.Intermediate
{
    public class Connecter
    {
        public List<TextCard> tcards;
        public List<QuestionCard> qcards;
        public Connecter(List<TextCard> tcards, List<QuestionCard> qcards)
        {
            this.tcards = tcards;
            this.qcards = qcards;
        }
    }
}
