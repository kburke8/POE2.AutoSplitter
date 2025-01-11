using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using POE2.AutoSplitter.Component.GameClient;
using POE2.AutoSplitter.Component.Settings;
using POE2.AutoSplitter.Component.Timer;

namespace POE2.AutoSplitter.Component
{
    public class POE2Component : LogicComponent
    {
        public const string NAME = "POE2";

        private readonly ClientReader reader;
        private readonly ComponentSettings settings;
        private readonly SettingsControl control;

        // We'll create a TimerModel for Start/Split logic
        private readonly TimerModel timerModel;

        public POE2Component(LiveSplitState state)
        {
            // Create or load your settings object
            settings = new ComponentSettings();

            // Create a TimerModel that wraps the LiveSplitState
            timerModel = new TimerModel() { CurrentState = state };

            // Create your custom "LoadRemoverSplitter" or anything else
            var remover = new LoadRemoverSplitter(timerModel, settings);

            // Create a LogReader that will watch the logs / memory
            reader = new ClientReader(settings, remover);
            reader.Start();

            // Create your Windows Forms control for the settings UI
            control = new SettingsControl(settings, state);

            // Example: If the user changes the log path, restart the reader
            settings.HandleLogLocationChanged = reader.Start;
        }

        public override string ComponentName => NAME;

        public override void Dispose()
        {
            // Make sure to stop background threads, close file handles, etc.
            reader.Stop();
        }

        // Save your settings to XML
        public override XmlNode GetSettings(XmlDocument document)
        {
            return settings.GetSettings(document);
        }

        // Load your settings from XML
        public override void SetSettings(XmlNode settingsNode)
        {
            settings.SetSettings(settingsNode);
            control.XmlRefresh();
        }

        // Return your Windows Forms user control
        public override Control GetSettingsControl(LayoutMode mode)
        {
            return control;
        }

        // Called every frame; do any per-frame logic here.
        public override void Update(IInvalidator invalidator, LiveSplitState state,
                                    float width, float height, LayoutMode mode)
        {
            // If you have something to do each frame, do it here.
            // E.g., checking for conditions and updating TimerModel if needed
        }
    }
}
