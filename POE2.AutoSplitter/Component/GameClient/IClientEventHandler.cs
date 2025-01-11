namespace POE2.AutoSplitter.Component.GameClient
{
    public interface IClientEventHandler
    {
        void HandleLoadStart(long timestamp);
        void HandleLoadEnd(long timestamp, string zoneName);
        void HandleLevelUp(long timestamp, int level);
        void HandleZoneChange(long timestamp, string zoneName);
    }
}