using System.Text;
using System.Xml;
using Color = System.Drawing.Color;

const string themePath = @"C:\Users\djfla\source\repos\TestThemeProject\TestThemeProject\CustomTheme.vstheme";
const string outputTheme = @"C:\Users\djfla\source\repos\TestThemeProject\TestThemeProject\CustomTheme2.vstheme";

var themeXml = new XmlDocument();
themeXml.Load(new FileStream(themePath, FileMode.Open));

var colorNodes = themeXml.SelectNodes(@"//*[@" + "Source" + "]");

List<Tuple<int, int, int>> usedColors = new();
var rng = new Random();

foreach (XmlNode colorNode in colorNodes)
{
    colorNode.Attributes["Source"].Value = "FF" + GetRandomHexValueString();

    string GetRandomHexValueString()
    {
        var r = rng.Next(0, 256);
        var g = rng.Next(0, 256);
        var b = rng.Next(0, 256);

        var colorTuple = new Tuple<int, int, int>(r, g, b);

        if (usedColors.Contains(colorTuple))
            GetRandomHexValueString();

        usedColors.Add(colorTuple);

        Color newColor = System.Drawing.Color.FromArgb(r, g, b);

        string hex = newColor.R.ToString("X2") + newColor.G.ToString("X2") + newColor.B.ToString("X2");
        return hex;
    }
}

using var xmlTextWriter = new XmlTextWriter(outputTheme, Encoding.UTF8);
themeXml.WriteTo(xmlTextWriter);