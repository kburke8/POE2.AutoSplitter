using LiveSplit.Model;
using POE2.AutoSplitter.Component.GameClient;
using POE2.AutoSplitter.Component.Timer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace POE2.AutoSplitter.Component
{
    public class PoE2ZoneDetector : IClientEventHandler
    {
        // Regex to detect the zone generation line:
        // e.g. "[DEBUG Client 58512] Generating level 12 area "G1_13_1" with seed 2354313812"
        private static readonly Regex ZoneGenerationRegex = new Regex(
            @"\[DEBUG Client \d+\]\s+Generating level\s+\d+\s+area\s+""(?<zoneCode>[^""]+)""",
            RegexOptions.Compiled
        );

        private readonly LiveSplitState _state;
        private readonly ITimerModel _timer;
        private readonly ClientParser _parser;
        private readonly string _logFile;
        private long _lastReadOffset;

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
            _timer = new TimerModel() { CurrentState = state };
            _parser = new ClientParser(this);
            _logFile = @"C:\Program Files (x86)\Steam\steamapps\common\Path of Exile 2 Beta\logs\Client.txt";
            _lastReadOffset = 0;
        }

        public void HandleLoadStart(long timestamp)
        {
            // Not used yet
        }

        public void HandleLoadEnd(long timestamp, string zoneName)
        {
            // Not used yet
        }

        public void HandleLevelUp(long timestamp, int level)
        {
            // Not used yet
        }

        public void HandleZoneChange(long timestamp, string zoneName)
        {
            // Split on zone change
            if (_state.CurrentPhase == TimerPhase.NotRunning)
            {
                if (zoneName == "The Riverbank")
                {
                    _timer.Start();
                }
            }
            else
            {
                _timer.Split();
            }
        }

        public void Update()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(_logFile);
                for (int i = (int)_lastReadOffset; i < lines.Length; i++)
                {
                    _parser.ProcessLine(lines[i]);
                }
                _lastReadOffset = lines.Length;
            }
            catch (Exception)
            {
                // Ignore file access errors
            }
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
                HandleZoneChange(0, zoneName);
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
