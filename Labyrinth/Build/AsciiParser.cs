using System;
using Labyrinth.Tiles;

namespace Labyrinth.Build
{
    public class AsciiParser
    {
        public Tile[,] Parse(string ascii_map)
        {
            if (ascii_map is null) throw new ArgumentNullException(nameof(ascii_map));
            var lines = ascii_map.Split("\n,\r\n".Split(','), StringSplitOptions.None);
            if (lines.Length == 0) return new Tile[0, 0]; // <-- correction : tableau multidimensionnel vide
            var width = lines[0].Length;
            var tiles = new Tile[width, lines.Length];

            using var km = new Keymaster();

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                var line = lines[y] ?? string.Empty;
                if (line.Length < width)
                    line = line.PadRight(width, ' ');

                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    var ch = line[x];
                    tiles[x, y] = ch switch
                    {
                        'x' => NewStartPos(x, y),
                        ' ' => new Room(),
                        '+' or '-' or '|' => Wall.Singleton,
                        '/' => km.NewDoor(),
                        'k' => km.NewKeyRoom(),
                        _ => throw new ArgumentException($"Invalid map: unknown character '{ch}' at line {y}, col {x}.")
                    };
                }
            }

            return tiles;
        }
        public EventHandler<StartEventArgs>? StartPositionFound;

        private Room NewStartPos(int x, int y)
        {
            StartPositionFound?.Invoke(this, new StartEventArgs(x, y));
            return new Room();
        }
    }
}
