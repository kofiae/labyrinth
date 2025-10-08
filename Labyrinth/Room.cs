using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class Room : Tile
    {
        private ICollectable? item;
        public override bool IsTraversable => true;

        public ICollectable Item { 
            get => item;
            set => item = value; 
        }

        public override void Pass()
        {
            throw new NotImplementedException();
        }
    }
}
