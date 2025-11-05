using System;
using Labyrinth.Crawl;
using Labyrinth.Tiles;

namespace Labyrinth
{
    public class Explorer
    {
        private readonly ICrawler _crawler;

    
        public Random Random { get; set; } = new Random();

    
        public event EventHandler<CrawlingEventArgs>? PositionChanged;

    
        public event EventHandler<CrawlingEventArgs>? DirectionChanged;

        public Explorer(ICrawler crawler)
        {
            _crawler = crawler ?? throw new ArgumentNullException(nameof(crawler));
        }

    
        public bool GetOut(int n)
        {
            if (n <= 0) return false;

            EmitDirectionChanged();
            EmitPositionChanged();

            for (int i = 0; i < n; i++)
            {
                if (_crawler.FacingTile is Outside)
                {
                    _crawler.Walk();
                    EmitPositionChanged();
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
                                EmitPositionChanged();
                            }
                            catch
                            {
                                
                                _crawler.Direction.TurnRight();
                                EmitDirectionChanged();
                            }
                        }
                        else
                        {
                            
                            _crawler.Direction.TurnRight();
                            EmitDirectionChanged();
                        }
                        break;

                    case 1:
                        _crawler.Direction.TurnLeft();
                        EmitDirectionChanged();
                        break;

                    case 2:
                        _crawler.Direction.TurnRight();
                        EmitDirectionChanged();
                        break;
                }
            }

            return false;
        }

        private void EmitPositionChanged() =>
            PositionChanged?.Invoke(this, new CrawlingEventArgs(_crawler.X, _crawler.Y, _crawler.Direction));

        private void EmitDirectionChanged() =>
            DirectionChanged?.Invoke(this, new CrawlingEventArgs(_crawler.X, _crawler.Y, _crawler.Direction));
    }
}
