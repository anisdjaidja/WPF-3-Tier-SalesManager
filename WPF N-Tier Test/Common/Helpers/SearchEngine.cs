namespace WPF_N_Tier_Test.Modules.Helpers
{
    public class SearchEngine
    {
        public string OriginalKey = string.Empty;
        public List<string> Subsets;
        public SearchEngine(string key)
        {
            OriginalKey = key;
            Subsets = new List<string>
            {
                OriginalKey
            };
            ChoppKey(OriginalKey);
        }
        public void ChoppKey(string key)
        {
            int PieceSize = 2;
            string Key = key.ToString();
            if (string.IsNullOrEmpty(Key) ) { return; }
            int runlegnth = Key.Length;
            if (Key.Length < PieceSize) { return; }
            if (runlegnth > 10) { runlegnth = 10; }
                string piece = string.Empty;
                for(int j  = 0; j < PieceSize; j++)
                {
                    piece += Key[j];
                }
                Subsets.Add(piece);
            ChoppKey(Key.Substring(1));
            return;

        }
    }
}
