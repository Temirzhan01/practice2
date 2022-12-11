using WebApplication3.Classes.Composite;
using WebApplication3.Classes.Singleton;
using WebApplication3.Classes.Adapter;
using WebApplication3.Classes.Intermediate;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.State;

namespace WebApplication3.Classes.Strategy
{
    public class Full : IStrategy
    {
        public async Task Creating(bool type, string key, string a1, string a2, string a3, string a4, Context c)
        {
            Card card = c.Create(key, a1, a2, a3, a4, type);
            string json = Adapter.Adapter.Converter(card);
            Cardjson cj = new Cardjson();
            cj.userId = UserInfo.Id;
            cj.jsoncard = json;
            cj.type = type;
            using (ApplicationContext context = new ApplicationContext()) 
            {
                context.Cardjsons.Add(cj);
                await context.SaveChangesAsync();
            }
        }
        public IEnumerable<Connecter> Showing(IEnumerable<Cardjson> cardjsons)
        {
            List<TextCard> textlist = new List<TextCard>();
            List<QuestionCard> questionlist = new List<QuestionCard>();
            foreach (Cardjson item in cardjsons) 
            {
                if (item.type)
                {
                    TextCard cardt = JsonConvert.DeserializeObject<TextCard>(item.jsoncard)!;
                    textlist.Add(cardt);                    
                }
                else
                {
                    QuestionCard cardq = JsonConvert.DeserializeObject<QuestionCard>(item.jsoncard)!;
                    questionlist.Add(cardq);
                }
            }
            List<Connecter> connecters = new List<Connecter>();
            connecters.Add(new Connecter(textlist, questionlist));
            return connecters.AsEnumerable();
        }
    }
}
