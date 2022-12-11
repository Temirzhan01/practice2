﻿using WebApplication3.Classes.State;

namespace WebApplication3.Classes.Flyweight
{
    public class TCardFactory : CardFactory
    {
        public TCardFactory(List<Card> cards) : base( cards) { }
        public override Card GetCard(string key, string a1, string a2, string a3, string a4, bool type, Context cont)
        {
            if (!type)
            {
                cont.ChangeState(new QCardFactory(cards));
                return cont.Create(key, a1, a2, a3, a4, type);
            }
            foreach (Card card in cards)
            {
                if (card.Check(key, a1, a2, a3, a4, type))
                {
                    return card;
                }
            }
            Card c = cont.invoker.PressCreateButton(0, key, a1, a2, a3, a4);
            cards.Add(c);
            return c;
        }
    }
}
