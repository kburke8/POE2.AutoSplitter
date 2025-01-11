using System;
using System.Text.RegularExpressions;

namespace POE2.AutoSplitter.Component.GameClient
{
    public class ClientParser
    {
        // Format: 2024/12/06 18:07:26 375906171 2caa1679
        private static readonly string TIMESTAMP_SECTION = @"^(\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2} \d+ [a-f0-9]+)";

        private static readonly Regex ZONE_ENTERED = new Regex(
            TIMESTAMP_SECTION + @" \[DEBUG Client \d+\] Generating level \d+ area ""(?<zone>C?_?[\w_]+)"" with seed \d+"
);


        // Detect level ups
        private static readonly Regex LEVEL_UP = new Regex(
            TIMESTAMP_SECTION + @" : .* is now level (\d+)$"
        );

        private IClientEventHandler splitter;

        public ClientParser(IClientEventHandler splitter)
        {
            this.splitter = splitter;
        }

        public void ProcessLine(string s)
        {
            // Check for zone entry
            Match match = ZONE_ENTERED.Match(s);
            if (match.Success)
            {
                GroupCollection groups = match.Groups;
                string zoneName = groups["zone"].Value;
                var timestamp = ParseTimestamp(groups[1].Value);
                splitter.HandleLoadEnd(timestamp, zoneName);
                return;
            }


            // Check for level ups
            match = LEVEL_UP.Match(s);
            if (match.Success)
            {
                GroupCollection groups = match.Groups;
                splitter.HandleLevelUp(ParseTimestamp(groups[1].Value), int.Parse(groups[2].Value));
            }
        }

        private long ParseTimestamp(string timestamp)
        {
            // Parse timestamp format: 2024/12/06 18:07:26 375906171 2caa1679
            // We'll use the millisecond value (375906171 in this example)
            string[] parts = timestamp.Split(' ');
            return long.Parse(parts[2]);
        }
    }
}
