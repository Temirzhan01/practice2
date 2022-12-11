using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.Iterator;

namespace WebApplication3.Classes.Composite
{
    public class Leaf : Component
    {
        List<Component> children = new List<Component>();
        public Card card;
        public Leaf(string name, Card card) : base(name)
        {
            this.card = card;
        }
        public override Card GetC()
        {
            return this.card;
        }
        public override int Count
        {
            get { return 0; }
            protected set { }
        }
        public override Component this[int index]
        {
            get { return children[index]; }
        }
    }
}
