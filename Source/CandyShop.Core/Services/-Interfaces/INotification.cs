using System.Collections.Generic;

namespace CandyShop.Core.Services
{
    public interface INotification
    {
        void Add(string notification);
        bool HasNotification();
        IEnumerable<string> GetNotification();
    }
}
