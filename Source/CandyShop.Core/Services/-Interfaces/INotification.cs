namespace CandyShop.Core.Services
{
    public interface INotification
    {
        void Add(string notification);
        bool HasNotification();
        string GetNotification();
    }
}
