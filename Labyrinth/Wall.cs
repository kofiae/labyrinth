using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class Wall : Tile
    {
        public override bool IsTraversable => false;

        public override void Pass()
        {
            throw new NotImplementedException();
        }
    }
}
