namespace AIO
{
    public interface ICacheableHandler
    {
        void OnCheckIn();

        void OnCheckOut();
    }
}
