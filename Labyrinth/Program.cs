using System;
using Labyrinth.Crawl;
using Labyrinth.Tiles;

namespace Labyrinth
{
    public class Program
    {
        private static int _lastX;
        private static int _lastY;
        private static Direction _lastDir;

        private static string[] _mapLines = Array.Empty<string>();

        public static void Main(string[] args)
        {
            string asciiMap =
                "#########\n" +
                "#   /   #\n" +
                "# ### # #\n" +
                "# k   # #\n" +
                "# ###   #\n" +
                "#   x   #\n" +
                "#########\n";

            var labyrinth = new Labyrinth(asciiMap);
            Console.Clear();
            Console.CursorVisible = false;

            _mapLines = SplitLines(labyrinth.ToString());
            Console.SetCursorPosition(0, 0);
            Console.Write(labyrinth.ToString());

            var crawler = labyrinth.NewCrawler();
            _lastX = crawler.X;
            _lastY = crawler.Y;
            _lastDir = crawler.Direction;

            var explorer = new Explorer(crawler);
            explorer.PositionChanged += OnPositionChanged;
            explorer.DirectionChanged += OnDirectionChanged;

            DrawArrow(_lastX, _lastY, _lastDir);

            bool escaped = explorer.GetOut(n: 1000);

            Console.SetCursorPosition(0, _mapLines.Length + 1);
            Console.CursorVisible = true;
            Console.WriteLine(escaped ? "Sortie atteinte 🎉" : "Limite de pas atteinte sans sortir.");
        }

        private static void OnPositionChanged(object? sender, CrawlingEventArgs e)
        {
            RestoreMapChar(_lastX, _lastY);
            DrawArrow(e.X, e.Y, e.Direction);
            _lastX = e.X;
            _lastY = e.Y;
            _lastDir = e.Direction;
        }

        private static void OnDirectionChanged(object? sender, CrawlingEventArgs e)
        {
            DrawArrow(e.X, e.Y, e.Direction);
            _lastDir = e.Direction;
        }

        private static void DrawArrow(int x, int y, Direction dir)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(dir switch
            {
                Direction.North => '^',
                Direction.East  => '>',
                Direction.South => 'v',
                Direction.West  => '<',
                _ => '?'
            });
        }

        private static void RestoreMapChar(int x, int y)
        {
            if (y < 0 || y >= _mapLines.Length) return;
            var line = _mapLines[y];
            if (x < 0 || x >= line.Length) return;

            Console.SetCursorPosition(x, y);
            Console.Write(line[x]);
        }

        private static string[] SplitLines(string s)
        {
            var raw = s.Replace("\r\n", "\n").Split('\n');
            if (raw.Length > 0 && raw[^1].Length == 0)
            {
                Array.Resize(ref raw, raw.Length - 1);
            }
            return raw;
        }
    }
}
