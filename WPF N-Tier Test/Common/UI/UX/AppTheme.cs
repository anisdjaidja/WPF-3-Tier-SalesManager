using System.Windows;
using System.Windows.Media;

namespace WPF_N_Tier_Test.Common.UI.UX
{

    public class AppTheme
    {
        public AppTheme()
        {
        }


        public static string blendHex(string primary, string secondary)
        {
            var p = hextoRGB(primary);
            var s = hextoRGB(secondary);
            List<int> blent = new();
            for (int i = 0; i < 3; i++)
            {
                blent.Add((p[i] * 20 + s[i]) / 21);
            }
            return RGBtoHex(blent);
        }
        public static string RGBtoHex(List<int> rgb)
        {
            string result = "#";
            result += string.Format("{0:X2}{1:X2}{2:X2}", rgb[0], rgb[1], rgb[2]);
            return result;
        }
        public static List<int> hextoRGB(string hex)
        {
            List<int> result = new();
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hex);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);

            result.Add(r); result.Add(g); result.Add(b);
            return result;
        }
        public static Brush BrushFromString(string? colorCode)
        {
            return new BrushConverter().ConvertFrom(colorCode) as Brush;
        }
    }
}
