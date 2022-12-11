using WebApplication3.Classes.Composite;
using WebApplication3.Classes.Singleton;
using WebApplication3.Classes.Intermediate;
using WebApplication3.Classes.Iterator;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.State;

namespace WebApplication3.Classes.Strategy
{
    public class NotFull : IStrategy
    {
        public async Task Creating(bool type, string key, string a1, string a2, string a3, string a4, Context c)
        {
            Card card = c.Create(key, a1, a2, a3, a4, type);
            Component branch;
            if (type)
            {
                branch = new Branch("texts");
                Component leaf = new Leaf(key, card);
                MainRoot.main.Add(branch);
                if (MainRoot.main.Count < 1) { branch.Add(leaf); MainRoot.main.Add(branch); }
                else if (MainRoot.main[0].Name() == "texts") { MainRoot.main[0].Add(leaf); }
                else if (MainRoot.main.Count > 1 && MainRoot.main[1].Name() == "texts") { MainRoot.main[1].Add(leaf); }
            }
            else
            {
                branch = new Branch("questions");
                Component leaf = new Leaf(key, card);
                MainRoot.main.Add(branch);
                if (MainRoot.main.Count < 1) { branch.Add(leaf); MainRoot.main.Add(branch); }
                else if (MainRoot.main[0].Name() == "questions") { MainRoot.main[0].Add(leaf); }
                else if (MainRoot.main.Count > 1 && MainRoot.main[1].Name() == "questions") { MainRoot.main[1].Add(leaf); }
            }
            
        }
        public IEnumerable<Connecter> Showing(IEnumerable<Cardjson> cardjsons)
        {
            List<TextCard> textlist = new List<TextCard>();
            List<QuestionCard> questionlist = new List<QuestionCard>();
            List<Connecter> connecters = new List<Connecter>();
            if (MainRoot.main.Count >= 1) 
            {
                Iterator.Iterator i = MainRoot.main.CreateIterator();
                Component c = i.First();
                while (i.HasNext())
                {
                    Iterator.Iterator item = c.CreateIterator();
                    Component cur = item.First();
                    if (c.Name() == "texts")
                    {
                        while (item.HasNext())
                        {
                            TextCard card = (TextCard)cur.GetC();
                            textlist.Add(card);
                            cur = item.Next();
                        }
                    }
                    else 
                    {
                        while (item.HasNext())
                        {
                            QuestionCard card = (QuestionCard)cur.GetC();
                            questionlist.Add(card);
                            cur = item.Next();
                        }
                    }
                    c = i.Next();
                }
            }
            connecters.Add(new Connecter(textlist, questionlist));
            return connecters.AsEnumerable();
        }
    }
}
