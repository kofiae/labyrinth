using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class Room : Tile
    {
        private Collectable? item;
        public override bool IsTraversable => true;

        public Collectable Item { get => Item; set => Item = value; }

        public override void Pass()
        {
            throw new NotImplementedException();
        }
    }
}
