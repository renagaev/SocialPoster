using SixLabors.Fonts;

namespace SocialPoster.Drawers;

public static class FontSizeHelper
{
    public static (float, string) FindMultilineFontSize(Font font, string text, (int x, int y) boxSize)
    {
        var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var (min, max) = (0f, 500f);
        while (true)
        {
            var center = min + (max - min) / 2;
            font = new Font(font, center);
            var wrapped = new List<string>();
            var curr = "";
            foreach (var word in words)
            {
                var next = (curr + $" {word}").Trim();
                if (TextMeasurer.Measure(next, new TextOptions(font)).Width > boxSize.x)
                {
                    wrapped.Add(curr);
                    curr = word;
                }
                else
                    curr = next;
            }

            if (curr != "") wrapped.Add(curr);
            var joined = string.Join("\n", wrapped);
            var size = TextMeasurer.Measure(joined, new TextOptions(font));
            if (size.Height > boxSize.y || size.Width > boxSize.x)
                max = center;
            else
                min = center;
            if (max - min < 1)
            {
                return (center, joined);
            }
        }
    }

    public static float FindSinglelineFontSize(Font font, string text, (int x, int y) boxSize)
    {
        var (min, max) = (0f, 500f);
        while (true)
        {
            var center = min + (max - min) / 2;
            font = new Font(font, center);
            var size = TextMeasurer.Measure(text, new TextOptions(font));
            if (size.Height > boxSize.y || size.Width > boxSize.x)
                max = center;
            else
                min = center;
            if (max - min < 1)
                return min;
        }
    }
}