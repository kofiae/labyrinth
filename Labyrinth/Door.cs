using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class Door : Tile
    {
        private Key? key;
        private bool isOpen;

        public Door()
        {
            this.key = new Key();
            this.isOpen = false;
        }

        public Door(Key key)
        {
            this.key = key;
            this.isOpen = false;
        }

        public override bool IsTraversable => isOpen;

        public Key Key
        {
            get { return key; }
        }

        public bool IsOpen
        {
            get { return isOpen; }
        }
        public void Unlock(Key key)
        {
            if (this.key != null && this.key == key)
            {
                isOpen = true;
            }
            else
            {
                throw new InvalidOperationException("La clé ne correspond pas à la porte.");
            }
        }


        public override void Pass()
        {
            throw new NotImplementedException();
        }
    }
}
