using WebApplication3.Classes.Flyweight;

namespace WebApplication3.Classes.Command
{
    public class Invoker
    {
        List<ICommand> commands = new List<ICommand>() { new TCommands(new Receiver()), new QCommands(new Receiver())};
        public Invoker() { }
        public Card PressCreateButton(int i, string key, string a1, string a2, string a3, string a4) 
        {
            return commands[i].Execute(key, a1, a2, a3, a4);
        }
        public async Task PressUndoButton(int i, int id, bool s)
        {
            await commands[i].Undo(id, s);
        }
    }
}
