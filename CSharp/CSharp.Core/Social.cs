using System;

namespace CSharp.Core
{
    public class Social : ISocial
    {
        public Social(PostRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));            
        }

        public string Read(string user)
        {
            return string.Empty;
        }

        public void Post(string user, string message)
        {
            throw new NotImplementedException();
        }
    }
}