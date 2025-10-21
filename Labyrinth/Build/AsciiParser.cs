using Labyrinth.Tiles;

namespace Labyrinth.Build
{
    public class AsciiParser
    {
        public static (Tile[,] tiles, int startX, int startY) Parse(string ascii_map)
        {
            var lines = ascii_map.Split("\n,\r\n".Split(','), StringSplitOptions.None);
            var width = lines[0].Length;
            var tiles = new Tile[width, lines.Length];
            
            int? startX = null;
            int? startY = null;
            
            using var km = new Keymaster();

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (lines[y].Length != width)
                {
                    throw new ArgumentException("Invalid map: all lines must have the same length.");
                }
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y] = lines[y][x] switch
                    {
                        ' ' => new Room(),
                        'x' => HandleStartPosition(x, y, ref startX, ref startY),
                        '+' or '-' or '|' => Wall.Singleton,
                        '/' => km.NewDoor(),
                        'k' => km.NewKeyRoom(),
                        _ => throw new ArgumentException($"Invalid map: unknown character '{lines[y][x]}' at line {y}, col {x}.")
                    };
                }
            }
            
            if (!startX.HasValue || !startY.HasValue)
            {
                throw new ArgumentException("Invalid map: no starting position 'x' found.");
            }
            
            return (tiles, startX.Value, startY.Value);
        }
        
        private static Room HandleStartPosition(int x, int y, ref int? startX, ref int? startY)
        {
            startX = x;
            startY = y;
            return new Room();
        }
    }
}
