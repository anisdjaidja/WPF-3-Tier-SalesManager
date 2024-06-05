
namespace WPF_N_Tier_Test.Modules.Helpers
{
    public static class Utils
    {
        public static string FormatNumber(double number, int size, string Prefix = "")
        {
            string Q = number.ToString();
            int diff = size - Q.Length;
            if (Prefix != string.Empty){ Q = Q + ' ' + Prefix; };
            if (diff <= 0) { return Q; }
            for (int i = 0; i < diff; i++) { Q = "0" + Q; }
            return Q;
        }
        public static void PreviewNumericInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char last = e.Text[^1];
            e.Handled = !(last == '.') && !double.TryParse(e.Text, out _);
        }
        public static string RoundBackNumber(long num)
        {
            if (num == 0)
                return "0";
            // Ensure number has max 3 significant digits (no rounding up can happen)
            long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            if (i == 0)
                return "0";
            num = num / i * i;

            if (num >= 1000000000)
                return (num / 1000000000D).ToString("0.##") + "B";
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##") + "M";
            if (num >= 1000)
                return (num / 1000D).ToString("0.##") + "K";

            return num.ToString("#,0");
        }

    }
}
