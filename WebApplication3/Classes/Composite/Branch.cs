using WebApplication3.Classes.Iterator;

namespace WebApplication3.Classes.Composite
{
    public class Branch : Component
    {
        List<Component> children = new List<Component>();
        int index = 0;
        public Branch(string name) : base(name) { }
        public override void Add(Component c) 
        {
            if (!Check(c.Name())) 
            {
                children.Add(c);
            }
        }
        public override void Remove(Component c) 
        {
            children.Remove(c);
        }
        public override bool Check(string s)
        {
            foreach(Component item in children) 
            {
                if (item.Name() == s) { return true; }
            }
            return false;
        }
        public override void Clear()
        {
            children.Clear();
        }
        public override Iterator.Iterator CreateIterator()
        {
            return new CompositeIterator(this);
        }
        public override int Count
        {
            get { return children.Count; }
            protected set { }
        }
        public override Component this[int index]
        {
            get { return children[index]; }
        }
    }
}
