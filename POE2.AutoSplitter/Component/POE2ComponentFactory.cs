using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using POE2.AutoSplitter.Component;

[assembly: ComponentFactory(typeof(POE2ComponentFactory))]

namespace POE2.AutoSplitter.Component
{
    public class POE2ComponentFactory : IComponentFactory
    {
        public ComponentCategory Category => ComponentCategory.Timer;
        public string ComponentName => POE2Component.NAME;

        public string Description => "Load Time Removal and Auto Splitting for Path of Exile 2.";

        public IComponent Create(LiveSplitState state)
        {
            return new POE2Component(state);
        }

        public string UpdateName => "Path of Exile 2";

        //TODO: UPDATE URL
        public string UpdateURL => "";
        public Version Version => new Version(1, 0, 0);
        public string XMLURL => UpdateURL + "updates.xml";
    }
}
