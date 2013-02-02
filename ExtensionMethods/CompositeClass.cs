using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    class CompositeClass
    {
        public static void Main()
        {
            //Composite root = new Composite("root");
            //root.Add(new Leaf("Leaf A"));
            //root.Add(new Leaf("Leaf B"));

            //Composite comp = new Composite("Composite X");
            //comp.Add(new Leaf ("Leaf XA"));
            //comp.Add(new Leaf("Leaf XA"));

            //root.Add(comp);
        }
    }

    public abstract class Component
    {
        protected string name;
        public Object xNode;

        public Component()
        {

        }

        public Component(string name)
        {
            this.name = name;
        }

        public Component(Object xNode)
        {
            this.xNode = xNode;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
        public abstract Component LastChild();
    }

    public class Composite : Component
    {
        private List<Component> _children = new List<Component>();
        public Composite()
            : base()
        {

        }

        public Composite(Object xNode)
            : base(xNode)
        {

        }

        public Composite(string name)
            : base( name)
        {

        }

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override void Display(int depth)
        {
            foreach (Component component in _children)
            {
                component.Display(depth + 2);
            }
        }

        public override Component LastChild()
        {
            if (this._children.Count != 0)
            {
                return this._children.LastOrDefault();
            }
            else
            {
                return this;
            }
        }
    }

    public class Leaf : Component
    {
        public Leaf(string name)
            :base (name)
        {

        }

        public Leaf(Dictionary<string, string> dictionary)
            : base(dictionary)
        {

        }

        public Leaf(Object xNode)
            : base(xNode)
        {

        }       

        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }

        public override Component LastChild()
        {
            throw new NotImplementedException();
        }
    }
}

