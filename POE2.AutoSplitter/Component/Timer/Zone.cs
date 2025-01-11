using System.Collections.Generic;

namespace POE2.AutoSplitter.Component.Timer
{
    public class Zone : IZone
    {
        public enum ZoneType { Town, Zone }

        public static readonly List<Zone> ZONES;
        private static readonly Dictionary<Zone, Zone> REQUIRED;
        public static readonly Dictionary<IZone, ZoneType> ZONETYPES;

        public string Name => name;
        public string ZoneCode => zoneCode;
        public int Act => act;

        private readonly string name;
        private readonly string zoneCode;
        private readonly int act;

        static Zone()
        {
            ZONES = new List<Zone>();
            REQUIRED = new Dictionary<Zone, Zone>();
            ZONETYPES = new Dictionary<IZone, ZoneType>();

            // Act 1 Zones
            Zone clearfellEncampment = new Zone("Clearfell Encampment", "G1_town", 1);
            add(new Zone("The Riverbank", "G1_1", 1), ZoneType.Zone);
            add(clearfellEncampment, ZoneType.Town);
            add(new Zone("Clearfell", "G1_2", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Mud Burrow", "G1_3", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Grelwood", "G1_4", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Red Vale", "G1_5", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Grim Tangle", "G1_6", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Cemetery of the Eternals", "G1_7", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Mausoleum of the Praetor", "G1_8", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Tomb of the Consort", "G1_9", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Hunting Grounds", "G1_11", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Freythorn", "G1_12", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Ogham Farmlands", "G1_13_1", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Ogham Village", "G1_13_2", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("The Manor Ramparts", "G1_14", 1), ZoneType.Zone, clearfellEncampment);
            add(new Zone("Ogham Manor", "G1_15", 1), ZoneType.Zone, clearfellEncampment);

            // Act 2 Zones
            Zone ardura = new Zone("The Ardura Caravan", "G2_town", 2);
            add(ardura, ZoneType.Town);
            add(new Zone("Vastiri Outskirts", "G2_1", 2), ZoneType.Zone, ardura);
            add(new Zone("Mawdun Mine", "G2_2", 2), ZoneType.Zone, ardura);
            add(new Zone("Mawdun Quarry", "G2_3", 2), ZoneType.Zone, ardura);
            add(new Zone("Trial of the Sekhemas", "G2_4", 2), ZoneType.Zone, ardura);
            add(new Zone("The Halani Gates", "G2_5", 2), ZoneType.Zone, ardura);
            add(new Zone("Traitor's Passage", "G2_6", 2), ZoneType.Zone, ardura);
            add(new Zone("The Dreadnought", "G2_7", 2), ZoneType.Zone, ardura);
            add(new Zone("Dreadnought Vanguard", "G2_8", 2), ZoneType.Zone, ardura);
            add(new Zone("Mastodon Badlands", "G2_9", 2), ZoneType.Zone, ardura);
            add(new Zone("Valley of the Titans", "G2_10", 2), ZoneType.Zone, ardura);
            add(new Zone("Keth", "G2_11", 2), ZoneType.Zone, ardura);
            add(new Zone("Deshar", "G2_12", 2), ZoneType.Zone, ardura);
            add(new Zone("The Bone Pits", "G2_13", 2), ZoneType.Zone, ardura);
            add(new Zone("The Titan Grotto", "G2_14", 2), ZoneType.Zone, ardura);
            add(new Zone("The Lost City", "G2_15", 2), ZoneType.Zone, ardura);
            add(new Zone("Path of Mourning", "G2_16", 2), ZoneType.Zone, ardura);
            add(new Zone("Buried Shrines", "G2_17", 2), ZoneType.Zone, ardura);
            add(new Zone("The Spires of Deshar", "G2_18", 2), ZoneType.Zone, ardura);

            // Act 3 Zones
            Zone sarnRamparts = new Zone("The Sarn Ramparts", "G3_town", 3);
            add(sarnRamparts, ZoneType.Town);
            add(new Zone("Sandswept Marsh", "G3_1", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Venom Crypts", "G3_2", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Jungle Ruins", "G3_3", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Ziggurat Encampment", "G3_4", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Drowned City", "G3_5", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Apex of Filth", "G3_6", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Azak Bog", "G3_7", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Infested Barrens", "G3_8", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Temple of Kopec", "G3_9", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Molten Vault", "G3_10", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Matlan Waterways", "G3_11", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Chimeral Wetlands", "G3_12", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Utzaal", "G3_13", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Temple of Chaos", "G3_14", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Trial of Chaos", "G3_15", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Jiquani's Machinarium", "G3_16", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Aggorat", "G3_17", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Jiquani's Sanctum", "G3_18", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("The Black Chambers", "G3_19", 3), ZoneType.Zone, sarnRamparts);
            add(new Zone("Ziggurat Encampment (Past)", "G3_town_past", 3), ZoneType.Town, sarnRamparts);

            // Cruel Difficulty (Acts 4-6)
            // Act 4 (Cruel Act 1)
            Zone cruelClearfell = new Zone("Clearfell Encampment", "C_G1_town", 4);
            add(cruelClearfell, ZoneType.Town);
            add(new Zone("The Riverbank", "C_G1_1", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Clearfell", "C_G1_2", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Mud Burrow", "C_G1_3", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Grelwood", "C_G1_4", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Red Vale", "C_G1_5", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Grim Tangle", "C_G1_6", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Cemetery of the Eternals", "C_G1_7", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Mausoleum of the Praetor", "C_G1_8", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Tomb of the Consort", "C_G1_9", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Hunting Grounds", "C_G1_11", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Freythorn", "C_G1_12", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Ogham Farmlands", "C_G1_13_1", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Ogham Village", "C_G1_13_2", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("The Manor Ramparts", "C_G1_14", 4), ZoneType.Zone, cruelClearfell);
            add(new Zone("Ogham Manor", "C_G1_15", 4), ZoneType.Zone, cruelClearfell);

            // Act 5 (Cruel Act 2)
            Zone cruelArdura = new Zone("The Ardura Caravan", "C_G2_town", 5);
            add(cruelArdura, ZoneType.Town);
            add(new Zone("Vastiri Outskirts", "C_G2_1", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Mawdun Mine", "C_G2_2", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Mawdun Quarry", "C_G2_3", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Trial of the Sekhemas", "C_G2_4", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Halani Gates", "C_G2_5", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Traitor's Passage", "C_G2_6", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Dreadnought", "C_G2_7", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Dreadnought Vanguard", "C_G2_8", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Mastodon Badlands", "C_G2_9", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Valley of the Titans", "C_G2_10", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Keth", "C_G2_11", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Deshar", "C_G2_12", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Bone Pits", "C_G2_13", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Titan Grotto", "C_G2_14", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Lost City", "C_G2_15", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Path of Mourning", "C_G2_16", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("Buried Shrines", "C_G2_17", 5), ZoneType.Zone, cruelArdura);
            add(new Zone("The Spires of Deshar", "C_G2_18", 5), ZoneType.Zone, cruelArdura);

            // Act 6 (Cruel Act 3)
            Zone cruelSarnRamparts = new Zone("The Sarn Ramparts", "C_G3_town", 6);
            add(cruelSarnRamparts, ZoneType.Town);
            add(new Zone("Sandswept Marsh", "C_G3_1", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Venom Crypts", "C_G3_2", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Jungle Ruins", "C_G3_3", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Ziggurat Encampment", "C_G3_4", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Drowned City", "C_G3_5", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Apex of Filth", "C_G3_6", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Azak Bog", "C_G3_7", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Infested Barrens", "C_G3_8", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Temple of Kopec", "C_G3_9", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Molten Vault", "C_G3_10", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Matlan Waterways", "C_G3_11", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Chimeral Wetlands", "C_G3_12", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Utzaal", "C_G3_13", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Temple of Chaos", "C_G3_14", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Trial of Chaos", "C_G3_15", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Jiquani's Machinarium", "C_G3_16", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Aggorat", "C_G3_17", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Jiquani's Sanctum", "C_G3_18", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("The Black Chambers", "C_G3_19", 6), ZoneType.Zone, cruelSarnRamparts);
            add(new Zone("Ziggurat Encampment (Past)", "C_G3_town_past", 6), ZoneType.Town, cruelSarnRamparts);
        }

        private static void add(Zone zone, ZoneType type, Zone requirement = null)
        {
            ZONES.Add(zone);
            ZONETYPES[zone] = type;
            if (requirement != null)
            {
                REQUIRED[zone] = requirement;
            }
        }

        // Creates a zone representation using the zone name.
        public static IZone Parse(string zoneName, HashSet<IZone> encountered)
        {
            // First try to match by friendly name
            foreach (Zone zone in ZONES)
            {
                if (zone.name.Equals(zoneName))
                {
                    return zone;
                }
            }

            // Then try to match by zone code
            foreach (Zone zone in ZONES)
            {
                if (zone.zoneCode.Equals(zoneName))
                {
                    return zone;
                }
            }

            // If no match found, create a new zone with unknown code
            return new Zone(zoneName, "unknown", 1);
        }

        private Zone(string name, string zoneCode, int act)
        {
            this.name = name;
            this.zoneCode = zoneCode;
            this.act = act;
        }

        public string Serialize()
        {
            return ToString();
        }

        public string SplitName()
        {
            return name;
        }

        public override string ToString()
        {
            return $"{name} (Act {act})";
        }

        public override bool Equals(object obj)
        {
            Zone zone = obj as Zone;
            if (zone == null)
            {
                return false;
            }
            return name.Equals(zone.name) && zoneCode.Equals(zone.zoneCode);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() * 23 + zoneCode.GetHashCode();
        }
    }
}
