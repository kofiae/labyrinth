using System;
using Labyrinth.Crawl;
using Labyrinth.Tiles;

namespace Labyrinth
{
    public class Explorer
    {
        private readonly ICrawler _crawler;

        public Random Random { get; set; } = new Random();

        public Explorer(ICrawler crawler)
        {
            _crawler = crawler ?? throw new ArgumentNullException(nameof(crawler));
        }
        public bool GetOut(int n)
        {
            if (n <= 0) return false;

            for (int i = 0; i < n; i++)
            {
                if (_crawler.FacingTile is Outside)
                {
                    _crawler.Walk();
                    return true;
                }

                int action = Random.Next(3);

                switch (action)
                {
                    case 0:
                    
                        if (!(_crawler.FacingTile is Wall))
                        {
                            try
                            {
                                _crawler.Walk();
                            }
                            catch
                            {
                            
                                _crawler.Direction.TurnRight();
                            }
                        }
                        else
                        {
                        
                            _crawler.Direction.TurnRight();
                        }
                        break;

                    case 1:
                        _crawler.Direction.TurnLeft();
                        break;

                    case 2:
                        _crawler.Direction.TurnRight();
                        break;
                }

            }

            return false;
        }
    }
}
