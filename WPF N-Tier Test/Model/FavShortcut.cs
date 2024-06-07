
namespace WPF_N_Tier_Test.Model
{
    public class FavShortcut
    {
        public FavShortcut(int pageIndex, int tabIndex, string name = "fav")
        {
            PageIndex = pageIndex;
            TabIndex = tabIndex;
            Name = name;
        }
        public int PageIndex { get; set; }
        public int TabIndex { get; set; }
        public string Name { get; set; }
        public override bool Equals(object? obj)
        {
            if(!(obj is FavShortcut))
                return base.Equals(obj);
            return (((FavShortcut)obj).PageIndex == this.PageIndex) && (((FavShortcut)obj).TabIndex == this.TabIndex);
        }
    }
}
