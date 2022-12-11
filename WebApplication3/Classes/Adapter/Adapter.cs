using Newtonsoft.Json;
using WebApplication3.Classes.Flyweight;

namespace WebApplication3.Classes.Adapter
{
    public static class Adapter
    {
        public static string Converter(Card card) 
        {
            return JsonConvert.SerializeObject(card);
        }
    }
}
