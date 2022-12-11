using WebApplication3.Classes.Composite;

namespace WebApplication3.Classes.Iterator
{
    public class CompositeIterator : Iterator
    {
        private readonly Component component;
        private int current = 0;
        public CompositeIterator(Component aggregate)
        {
            this.component = aggregate;
        }
        public Component First()
        {
            if (component.Count == 0) 
            {
                return null;
            }
            return component[0];
        }
        public bool HasNext()
        {
            return current < component.Count;
        }
        public Component Next()
        {
            Component ret = null;
            current++;
            if (current < component.Count)
            {
                ret = component[current];
            }
            return ret;
        }
        public Component CurrentItem()
        {
            return component[current];
        }
    }
}