using Labyrinth.Items;
using Labyrinth.Tiles;

namespace Labyrinth.Build
{
    /// <summary>
    /// Manage the creation of doors and key rooms ensuring each door has a corresponding key room.
    /// Supports arbitrary distribution and ordering of doors and keys.
    /// </summary>
    public sealed class Keymaster : IDisposable
    {
        /// <summary>
        /// Ensure all created doors have a corresponding key room and vice versa.
        /// </summary>
        /// <exception cref="InvalidOperationException">Some keys are missing or are not placed.</exception>
        public void Dispose()
        {
            if (keysPool.HasItems || emptyKeyRooms.Count > 0)
            {
                throw new InvalidOperationException("Unmatched key/door creation");
            }
        }

        /// <summary>
        /// Create a new door and add its key to the pool. Keys are placed into pending key rooms when available.
        /// </summary>
        /// <returns>Created door</returns>
        public Door NewDoor()
        {
            var door = new Door();

            door.LockAndTakeKey(keysPool);
            PlaceKeys();
            return door;
        }

        /// <summary>
        /// Create a new room reserved to host a key and add it to the pending list. Keys are placed when available.
        /// </summary>
        /// <returns>Created key room</returns>
        public Room NewKeyRoom()
        {
            var room = new Room();
            emptyKeyRooms.Enqueue(room);
            PlaceKeys();
            return room;
        }

        private void PlaceKeys()
        {
            while (keysPool.HasItems && emptyKeyRooms.Count > 0)
            {
                var room = emptyKeyRooms.Dequeue();
                room.Pass().MoveItemFrom(keysPool);
            }
        }

        private readonly MyInventory keysPool = new();
        private readonly Queue<Room> emptyKeyRooms = new();
    }
}