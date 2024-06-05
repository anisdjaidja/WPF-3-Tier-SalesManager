using System.Windows;
using System.Windows.Controls;

namespace WPF_N_Tier_Test.Common.UI.UX
{
    internal class ReporterStackPanel : StackPanel
    {
        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent)
        {
            var collection = new ObservableUIElementCollection(this, logicalParent);

            return collection;
        }



    }

    public class ObservableUIElementCollection : UIElementCollection
    {
        public ObservableUIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
            : base(visualParent, logicalParent)
        {
        }

        public event EventHandler<CollectionChangedArgs> RaiseAddUIElement;

        public override int Add(UIElement element)
        {
            OnRiseAddUIElementEvent(new CollectionChangedArgs(CollectionChangedArgs.ChangeType.Added, 0, element));
            return base.Add(element);
        }

        protected virtual void OnRiseAddUIElementEvent(CollectionChangedArgs e)
        {
            // copy to avoid race condition
            EventHandler<CollectionChangedArgs> handler = RaiseAddUIElement;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        public override void Insert(int index, UIElement element)
        {
            OnRiseAddUIElementEvent(new CollectionChangedArgs(CollectionChangedArgs.ChangeType.Inserted, index, element));
            base.Insert(index, element);
        }
        public override void Remove(UIElement element)
        {
            OnRiseAddUIElementEvent(new CollectionChangedArgs(CollectionChangedArgs.ChangeType.Removed, base.IndexOf(element), element));
            base.Remove(element);
        }
    }
    public class CollectionChangedArgs
    {
        public int index;
        public UIElement element;
        public ChangeType change;
        public enum ChangeType
        {
            Added,
            Removed,
            Inserted
        }
        public CollectionChangedArgs(ChangeType _change, int _index, UIElement _element)
        {
            this.change = _change;
            this.index = _index;
            this.element = _element;
        }
    }
}
