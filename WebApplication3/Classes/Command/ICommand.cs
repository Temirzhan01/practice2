using WebApplication3.Classes.Flyweight;

namespace WebApplication3.Classes.Command
{
    public interface ICommand
    {
        public Card Execute(string key, string a1, string a2, string a3, string a4);
        public async Task Undo(int id, bool s) { }
    }
}
