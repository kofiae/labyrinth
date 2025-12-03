using Labyrinth.Crawl;
using Labyrinth.Items;
using Labyrinth.Sys;
using Labyrinth.Tiles;

namespace Labyrinth
{
    public class RandExplorer(ICrawler crawler, IEnumRandomizer<RandExplorer.Actions> rnd)
    {
        private readonly ICrawler _crawler = crawler;
        private readonly IEnumRandomizer<Actions> _rnd = rnd;
        
        public enum Actions
        {
            TurnLeft,
            Walk
        }

        public int GetOut(int n)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0, "n must be strictly positive");
            MyInventory bag = new ();

            for( ; n > 0 && _crawler.FacingTile is not Outside; n--)
            {
                EventHandler<CrawlingEventArgs>? changeEvent;

                if (_crawler.FacingTile.IsTraversable
                    && _rnd.Next() == Actions.Walk)
                {
                    var roomInventory = _crawler.Walk();
                    while (roomInventory.HasItems)
                    {
                        bag.MoveItemFrom(roomInventory);
                    }
                    changeEvent = PositionChanged;
                }
                else
                {
                    _crawler.Direction.TurnLeft();
                    changeEvent = DirectionChanged;
                }
                
                if (_crawler.FacingTile is Door door && door.IsLocked && bag.HasItems)
                {
                    TryOpenDoor(door, bag);
                }
                
                changeEvent?.Invoke(this, new CrawlingEventArgs(_crawler));
            }
            return n;
        }

        private void TryOpenDoor(Door door, MyInventory bag)
        {
            if (!bag.HasItems)
            {
                return;
            }
            
            int totalItems = bag.Items.Count();
            int attempts = 0;
            
            while (door.IsLocked && attempts < totalItems)
            {
                var firstItemBefore = bag.Items.First();
                
                door.Open(bag);
                
                if (door.IsLocked && bag.HasItems && bag.Items.First() == firstItemBefore)
                {
                    var temp = new MyInventory();
                    temp.MoveItemFrom(bag, 0);
                    bag.MoveItemFrom(temp, 0);
                }
                
                attempts++;
            }
        }

        public event EventHandler<CrawlingEventArgs>? PositionChanged;

        public event EventHandler<CrawlingEventArgs>? DirectionChanged;
    }

}
