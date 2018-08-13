using SocialNetwork.Core.Services.Interfaces;
using SocialNetwork.Core.Views;
using LanguageExt;

namespace SocialNetwork.Core.Services
{
    public class WallDisplay : Displayable
    {
        protected bool Equals(WallDisplay other)
        {
            return Equals(_wall, other._wall);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WallDisplay) obj);
        }

        public override int GetHashCode()
        {
            return (_wall != null ? _wall.GetHashCode() : 0);
        }

        private readonly WallView _wall;

        public WallDisplay(WallView wall)
        {
            _wall = wall;
        }
        
        public override Unit ShowOn(Display display)
        {
            display.Show(_wall);
            return Unit.Default;
        }
    }
}