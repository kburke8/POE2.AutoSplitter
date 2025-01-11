using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(LiveSplit.PoE2AutoSplitter.PoE2AutoSplitterFactory))]

namespace LiveSplit.PoE2AutoSplitter
{
    public class PoE2AutoSplitterFactory : IComponentFactory
    {
        public string ComponentName => "Path of Exile 2 Auto Splitter";
        public string Description => "Automatically splits based on PoE2 zone changes from client.txt logs.";
        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state)
        {
            return new PoE2AutoSplitterComponent(state);
        }

        public string UpdateName => ComponentName;
        public string UpdateURL => ""; // If you host updates
        public Version Version => new Version(1, 0, 0);
        public string XMLURL => "";    // If you provide an XML for auto-update
    }
}
