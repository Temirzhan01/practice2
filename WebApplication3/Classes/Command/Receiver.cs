using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.Singleton;
using WebApplication3.Classes.Composite;
using WebApplication3.Models;
using Newtonsoft.Json;

namespace WebApplication3.Classes.Command
{
    public class Receiver
    {
        public Card Createtextcard(string key, string a1, string a2, string a3, string a4) 
        {
            return new TextCard(a1, a2, a3, a4, key);
        }
        public Card Createquestioncard(string key, string a1, string a2, string a3, string a4) 
        {
            return new QuestionCard(a1, a2, a3, a4, key);
        }
        public async Task UndoTextcard(int id, bool s) 
        {
            if (s)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    IEnumerable<Cardjson> cardjsons = db.Cardjsons.Where(x => x.userId == UserInfo.Id).ToList();
                    foreach (Cardjson item in cardjsons)
                    {
                        if (item.type)
                        {
                            TextCard cardt = JsonConvert.DeserializeObject<TextCard>(item.jsoncard)!;
                            if (cardt.Id == id)
                            {
                                db.Cardjsons.Remove(item);
                                await db.SaveChangesAsync();
                                break;
                            }
                        }
                    }
                }
            }
            else 
            {
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
                                if (card.Id == id) 
                                {
                                    c.Remove(cur);
                                    break;
                                }
                                cur = item.Next();
                            }
                        }
                        c = i.Next();
                    }
                }
            }
        }
        public async Task UndoQuestionCard(int id, bool s) 
        {
            if(s)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    IEnumerable<Cardjson> cardjsons = db.Cardjsons.Where(x => x.userId == UserInfo.Id).ToList();
                    foreach (Cardjson item in cardjsons)
                    {
                        if (!item.type)
                        {
                            QuestionCard cardq = JsonConvert.DeserializeObject<QuestionCard>(item.jsoncard)!;
                            if (cardq.Id == id)
                            {
                                db.Cardjsons.Remove(item);
                                await db.SaveChangesAsync();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (MainRoot.main.Count >= 1)
                {
                    Iterator.Iterator i = MainRoot.main.CreateIterator();
                    Component c = i.First();
                    while (i.HasNext())
                    {
                        Iterator.Iterator item = c.CreateIterator();
                        Component cur = item.First();
                        if (c.Name() == "questions")
                        {
                            while (item.HasNext())
                            {
                                QuestionCard card = (QuestionCard)cur.GetC();
                                if (card.Id == id)
                                {
                                    c.Remove(cur);
                                    break;
                                }
                                cur = item.Next();
                            }
                        }
                        c = i.Next();
                    }
                }
            }
        }
    }
}
