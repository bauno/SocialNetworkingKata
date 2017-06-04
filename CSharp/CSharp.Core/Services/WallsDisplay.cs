using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Services
{
    public class WallsDisplay : Displayable
    {
        protected bool Equals(WallsDisplay other)
        {
            if (_walls.Count() == other._walls.Count())
            {
                var thisWalls = _walls.ToList();
                var otherWalls = _walls.ToList();
                for (int i = 0; i < thisWalls.Count; ++i)
                {
                    if (!thisWalls[i].Equals(otherWalls[i]))
                        return false;
                }
                return true;

            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WallsDisplay) obj);
        }

        public override int GetHashCode()
        {
            return (_walls != null ? _walls.GetHashCode() : 0);
        }


        private readonly IEnumerable<WallView> _walls;

        public WallsDisplay(IEnumerable<WallView> walls)
        {
            _walls = walls;
            
        }

        public void ShowOn(Display display)
        {
            display.Show(_walls);
        }
    }
}