using WebApplication3.Classes.Composite;

namespace WebApplication3.Classes.Iterator
{
    public interface Iterator
    {
        public abstract bool HasNext();
        public abstract Component Next();
        public abstract Component CurrentItem();
        public abstract Component First();
    }
}
