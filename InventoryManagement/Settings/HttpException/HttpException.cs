using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Settings.HttpException
{
    public class HttpException : Exception
    {
        public int StatusCode { get; private set; }
        public List<Notification> Notifications { get; private set; }

        public HttpException(int statusCode, IEnumerable<Notification> notifications)
            : base(string.Join(", ", notifications.Select(n => n.Message)))
        {
            StatusCode = statusCode;
            Notifications = notifications.ToList();
        }

        public HttpException(int statusCode, IEnumerable<Notification> notifications, Exception inner)
            : base(string.Join(", ", notifications.Select(n => n.Message)), inner)
        {
            StatusCode = statusCode;
            Notifications = notifications.ToList();
        }
    }
}