using WebApplication3.Classes.Flyweight;

namespace WebApplication3.Classes.Command
{
    public class QCommands : ICommand
    {
        Receiver r;
        public QCommands(Receiver r)
        {
            this.r = r;
        }
        public Card Execute(string key, string a1, string a2, string a3, string a4)
        {
            return r.Createquestioncard(key, a1, a2, a3, a4);
        }
        public async Task Undo(int id, bool s)
        {
            await r.UndoQuestionCard(id, s);
        }
    }
}
