using System.Diagnostics.CodeAnalysis;

namespace Labyrinth.Items
{
    /// <summary>
    /// Inventory of collectable items for rooms and players.
    /// </summary>
    /// <param name="item">Optional initial item in the inventory.</param>
    public abstract class Inventory(ICollectable? item = null)
    {
        /// <summary>
        /// True if the room has an item, false otherwise.
        /// </summary>
        public bool HasItems => _items.Count > 0;

        /// <summary>
        /// Gets the types of the items in the inventory.
        /// </summary>
        public IEnumerable<Type> ItemTypes => _items.Select(i => i.GetType());

        /// <summary>
        /// Moves the nth item from another inventory to this one.
        /// </summary>
        /// <param name="from">The inventory from which the item is taken. The item is removed from this inventory.</param>
        /// <param name="nth">The index of the item to move (default is 0 for the first item).</param>
        /// <exception cref="InvalidOperationException">Thrown if the source inventory has no items.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
        public void MoveItemFrom(Inventory from, int nth = 0)
        {
            if (!from.HasItems)
            {
                throw new InvalidOperationException("No item to take from the source inventory");
            }
            if (nth < 0 || nth >= from._items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(nth), "Index out of range");
            }

            var item = from._items[nth];
            from._items.RemoveAt(nth);
            _items.Add(item);
        }

        protected List<ICollectable> _items = item != null ? new List<ICollectable> { item } : new List<ICollectable>();
    }
}
