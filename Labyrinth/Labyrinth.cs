using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Labyrinth
    {
        private Tile[,] grid;
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
            

        public Labyrinth(string chemin)
        {
            var lignes = chemin.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            this.height = lignes.Length;
            this.width = lignes[0].Length;

            grid = new Tile[height, width];
        }
    }
}