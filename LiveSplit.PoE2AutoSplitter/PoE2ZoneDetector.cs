using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LiveSplit.PoE2AutoSplitter
{
    public class PoE2ZoneDetector
    {
        // Regex to detect the zone generation line:
        // e.g. "[DEBUG Client 58512] Generating level 12 area "G1_13_1" with seed 2354313812"
        private static readonly Regex ZoneGenerationRegex = new Regex(
            @"\[DEBUG Client \d+\]\s+Generating level\s+\d+\s+area\s+""(?<zoneCode>[^""]+)""",
            RegexOptions.Compiled
        );

        private readonly LiveSplitState _state;
        private bool _hasStartedRun;

        /// <summary>
        /// Zone code -> friendly name
        /// </summary>
        private static readonly Dictionary<string, string> ZoneMap = new Dictionary<string, string>()
        {
            { "G1_1",    "The Riverbank" },
            { "G1_town", "Clearfell Encampment" },
            { "G1_2",    "Clearfell" },
            { "G1_3",    "Mud Burrow" },
            { "G1_4",    "Grelwood" },
            { "G1_5",    "Red Vale" },
            { "G1_6",    "Grim Tangle" },
            { "G1_7",    "Cemetery of the Eternals" },
            { "G1_8",    "Mausoleum of the Praetor" },
            { "G1_9",    "Tomb of the Consort" },
            { "G1_11",   "Hunting Grounds" },
            { "G1_12",   "Freythorn" },
            { "G1_13_1", "Ogham Farmlands" },
            { "G1_13_2", "Ogham Village" },
            { "G1_14",   "The Manor Ramparts" },
            { "G1_15",   "Ogham Manor" },
            // etc. Add more zones or acts as needed
        };

        // Only split on certain zones (user can modify as needed)
        private static readonly HashSet<string> KeyZones = new HashSet<string>
        {
            "Ogham Farmlands",
            "Ogham Village",
            "Ogham Manor",
            "Clearfell Encampment"
        };

        public PoE2ZoneDetector(LiveSplitState state)
        {
            _state = state;
            _hasStartedRun = false;
        }

        /// <summary>
        /// Called by LogReader whenever a new line is appended to client.txt
        /// </summary>
        public void OnNewLogLine(string line)
        {
            var match = ZoneGenerationRegex.Match(line);
            if (!match.Success)
                return;

            string zoneCode = match.Groups["zoneCode"].Value;
            string strippedCode = StripDifficultyPrefix(zoneCode);
            string zoneName = ConvertZoneCode(strippedCode);

            // If it's a zone we care about, either start the run or split
            if (ShouldSplitOnZone(zoneName))
            {
                HandleSplitOrStart(zoneName);
            }
        }

        /// <summary>
        /// If the run hasn't started, start it. Otherwise, do a split.
        /// </summary>
        private void HandleSplitOrStart(string zoneName)
        {
            if (!_hasStartedRun)
            {
                // Start the run if it's not already running
                if (!_state.Run.HasStarted)
                {
                    _state.Start();
                }
                _hasStartedRun = true;
            }
            else
            {
                // If the timer is running, we do a split
                if (_state.CurrentPhase == TimerPhase.Running)
                {
                    _state.Split();
                }
            }
        }

        private string StripDifficultyPrefix(string zoneCode)
        {
            // e.g. "C_G1_13_1" -> "G1_13_1"
            if (zoneCode.StartsWith("C_"))
                return zoneCode.Substring(2);
            return zoneCode;
        }

        private string ConvertZoneCode(string zoneCode)
        {
            return ZoneMap.TryGetValue(zoneCode, out var zoneName)
                ? zoneName
                : zoneCode; // fallback if not in map
        }

        private bool ShouldSplitOnZone(string zoneName)
        {
            // If you want to split on every zone, just "return true;"
            return KeyZones.Contains(zoneName);
        }
    }
}
