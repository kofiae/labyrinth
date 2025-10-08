using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public abstract class Tile
    {   
        public abstract bool IsTraversable { get; }

        public abstract void Pass();
    }
}
