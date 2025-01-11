using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms; // If you need forms-based settings

namespace LiveSplit.PoE2AutoSplitter
{
    public class PoE2AutoSplitterComponent : IComponent
    {
        public string ComponentName => "Path of Exile 2 Auto Splitter";

        private readonly LiveSplitState _state;
        private readonly Thread _logThread;
        private volatile bool _threadRunning;

        private readonly PoE2ZoneDetector _zoneDetector;
        private readonly LogReader _logReader; // This is optional if you want to store it here

        // If you have user-configurable settings, you can add a Settings class. This is barebones.
        public PoE2AutoSplitterComponent(LiveSplitState state)
        {
            _state = state;

            // Create a zone detector. This object will handle lines that match "zone generation."
            _zoneDetector = new PoE2ZoneDetector(_state);

            // You can hardcode a path or eventually let the user choose via a settings GUI:
            string logPath = @"G:\SteamLibrary\steamapps\common\Path of Exile 2\logs\client.txt";

            // Create our log reader (not shown here) and link it to the zone detector
            // The LogReader is where we do the "tailing" of client.txt
            _logReader = new LogReader(logPath, _zoneDetector);

            // Start a background thread to poll for new log lines
            _threadRunning = true;
            _logThread = new Thread(LogUpdateLoop)
            {
                IsBackground = true
            };
            _logThread.Start();
        }

        /// <summary>
        /// Continuously reads new lines from client.txt in a background thread.
        /// </summary>
        private void LogUpdateLoop()
        {
            while (_threadRunning)
            {
                _logReader.Update();
                Thread.Sleep(200); // Polling interval
            }
        }

        /// <summary>
        /// This is called every frame by LiveSplit. 
        /// We do our log reading in a separate thread, so we can leave this empty if we want.
        /// </summary>
        public void Update(IInvalidator invalidator, float deltaTime)
        {
            // If you need to do any final checks or UI invalidation, do it here.
        }

        public void Dispose()
        {
            _threadRunning = false;
            _logThread?.Join();
            _logReader?.Dispose();
        }

        // The following are required by IComponent but might not be used in an autosplitter:
        public Control GetSettingsControl(LayoutMode mode) => null;
        public void SetSettings(System.Xml.XmlNode settings) { }
        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
            => document.CreateElement("Settings");
        public void DrawHorizontal(
            System.Drawing.Graphics g, LiveSplitState state, float height, System.Drawing.Region clipRegion)
        { }
        public void DrawVertical(
            System.Drawing.Graphics g, LiveSplitState state, float width, System.Drawing.Region clipRegion)
        { }
    }
}
