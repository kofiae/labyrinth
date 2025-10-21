using Labyrinth.Items;
using Labyrinth.Tiles;

namespace Labyrinth.Crawl
{
    /// <summary>
    /// Labyrinth crawler.
    /// </summary>
    internal class LabyrinthCrawler : ICrawler
    {
        private readonly Tile[,] _tiles;
        
        public LabyrinthCrawler(Tile[,] tiles, int startX, int startY)
        {
            _tiles = tiles;
            X = startX;
            Y = startY;
            Direction = Direction.North;
        }

        /// <summary>
        /// Gets the current X position.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the current Y position.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the current direction.
        /// </summary>
        public Direction Direction { get; private set; }

        /// <summary>
        /// Gets the tile in front of the crawler.
        /// </summary>
        public Tile FacingTile
        {
            get
            {
                int nextX = X + Direction.DeltaX;
                int nextY = Y + Direction.DeltaY;

                if (nextX < 0 || nextX >= _tiles.GetLength(0) ||
                    nextY < 0 || nextY >= _tiles.GetLength(1))
                {
                    return Outside.Singleton;
                }

                return _tiles[nextX, nextY];
            }
        }

        /// <summary>
        /// Pass the tile in front of the crawler and move into it.
        /// </summary>
        /// <returns>An inventory of the collectable items in the place reached.</returns>
        public Inventory Walk()
        {
            var tile = FacingTile;
            var inventory = tile.Pass();
            
            X += Direction.DeltaX;
            Y += Direction.DeltaY;
            
            return inventory;
        }
    }
}
