using System;
using System.Collections.Generic;
using System.Linq;
using NLua;

namespace Lou.Services {
    public static class TelemetryEventUtil {
        public static TelemetryEvent ConvertGameEvent(LuaTable table) {
            var e = new TelemetryEvent();
            e.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            e.type = "" + table["type"];
            if (table["actions"] != null) {
                e.actions = ExtractActions(table["actions"] as LuaTable);
            }

            if (table["players"] != null) {
                e.players = ExtractPlayers(table["players"] as LuaTable);
            }

            if (table["mobiles"] != null) {
                e.mobiles = ExtractMobiles(table["mobiles"] as LuaTable);
            }

            if (table["objects"] != null) {
                e.objects = ExtractObjects(table["objects"] as LuaTable);
                e.totalObjectValue = e.objects.Aggregate(0.0, (acc, val) => acc + val.value);
            }

            if (table["locations"] != null) {
                e.locations = ExtractLocations(table["locations"] as LuaTable);
            }

            return e;
        }

        public static List<Action> ExtractActions(LuaTable table) {
            var actions = new List<Action>();
            foreach (LuaTable action in table.Values) {
                if (action["type"] == null) {
                    continue;
                }

                var a = new Action();
                a.type = "" + action["type"];
                if (action["actorId"] != null) {
                    a.actorId = "" + action["actorId"];
                }

                if (action["targetId"] != null) {
                    a.targetId = "" + action["targetId"];
                }

                if (action["magnitude"] != null && float.TryParse(action["magnitude"] as string, out float mag)) {
                    a.magnitude = mag;
                }

                if (action["tags"] != null) {
                    a.tags = ExtractTags(action["tags"] as LuaTable);
                }

                actions.Add(a);
            }

            table.Dispose();
            return actions;
        }

        public static List<Player> ExtractPlayers(LuaTable table) {
            var players = new List<Player>();
            foreach (LuaTable player in table.Values) {
                if (player["userId"] == null) {
                    continue;
                }

                var p = new Player();
                p.userId = "" + player["userId"];

                if (player["ip"] != null) {
                    p.ip = "" + player["ip"];
                }

                if (player["id"] != null) {
                    p.id = "" + player["id"];
                }

                if (player["accessLevel"] != null) {
                    p.accessLevel = "" + player["accessLevel"];
                }

                if (player["factionId"] != null) {
                    p.factionId = "" + player["factionId"];
                }

                if (player["guildId"] != null) {
                    p.guildId = "" + player["guildId"];
                }

                if (player["npcGuildId"] != null) {
                    p.npcGuildId = "" + player["npcGuildId"];
                }

                if (player["notoriety"] != null) {
                    p.notoriety = "" + player["notoriety"];
                }

                if (player["age"] != null && float.TryParse(player["age"] as string, out float age)) {
                    p.age = age;
                }

                if (player["characterAge"] != null &&
                    float.TryParse(player["characterAge"] as string, out float characterAge)) {
                    p.characterAge = characterAge;
                }

                if (player["murderCount"] != null && int.TryParse(player["murderCount"] as string, out int mc)) {
                    p.murderCount = mc;
                }

                if (player["murderShortTermCount"] != null &&
                    int.TryParse(player["murderShortTermCount"] as string, out int mstc)) {
                    p.murderShortTermCount = mstc;
                }

                if (player["murderLongTermCount"] != null &&
                    int.TryParse(player["murderLongTermCount"] as string, out int mltc)) {
                    p.murderLongTermCount = mltc;
                }

                if (player["totalSkillLevel"] != null &&
                    float.TryParse(player["totalSkillLevel"] as string, out float tsl)) {
                    p.totalSkillLevel = tsl;
                }

                if (player["character"] != null) {
                    p.character = "" + player["character"];
                }

                if (player["skills"] != null) {
                    p.skills = ExtractSkills(player["skills"] as LuaTable);
                }

                if (player["stats"] != null) {
                    p.stats = ExtractStats(player["stats"] as LuaTable);
                }

                if (player["equipment"] != null) {
                    p.equipment = ExtractEquipment(player["equipment"] as LuaTable);
                }

                if (player["enchants"] != null) {
                    p.enchants = new List<string>();
                    foreach (var enchant in (player["enchants"] as LuaTable).Values) {
                        p.enchants.Add("" + enchant);
                    }
                }

                if (player["plotControllers"] != null) {
                    p.plotControllers = new List<string>();
                    foreach (var plotController in (player["plotControllers"] as LuaTable).Values) {
                        p.plotControllers.Add("" + plotController);
                    }
                }

                if (player["tags"] != null) {
                    p.tags = ExtractTags(player["tags"] as LuaTable);
                }

                players.Add(p);
            }
            
            table.Dispose();

            return players;
        }

        public static Skills ExtractSkills(LuaTable table) {
            var skills = new Skills();
            foreach (var skillName in table.Keys) {
                LuaTable skill = table[skillName] as LuaTable;
                if (skillName == null || !float.TryParse(skill["level"] as string, out float level)) continue;
                var s = new Skill { level = level};
                switch (skillName) {
                    case "Necromancy":
                        skills.necromancy = s;
                        break;
                    case "Archery":
                        skills.archery = s;
                        break;
                    case "Tinkering":
                        skills.tinkering = s;
                        break;
                    case "Wrestling":
                        skills.wrestling = s;
                        break;
                    case "Herding":
                        skills.herding = s;
                        break;
                    case "Inscription":
                        skills.inscription = s;
                        break;
                    case "Begging":
                        skills.begging = s;
                        break;
                    case "Veterinary":
                        skills.veterinary = s;
                        break;
                    case "Cartography":
                        skills.cartography = s;
                        break;
                    case "Magery":
                        skills.magery = s;
                        break;
                    case "Fletching":
                        skills.fletching = s;
                        break;
                    case "Tracking":
                        skills.tracking = s;
                        break;
                    case "Blacksmithy":
                        skills.blacksmithy = s;
                        break;
                    case "Mining":
                        skills.mining = s;
                        break;
                    case "Healing":
                        skills.healing = s;
                        break;
                    case "TasteId":
                        skills.tasteId = s;
                        break;
                    case "Parrying":
                        skills.parrying = s;
                        break;
                    case "Tailoring":
                        skills.tailoring = s;
                        break;
                    case "Stealth":
                        skills.stealth = s;
                        break;
                    case "EvalInt":
                        skills.evalInt = s;
                        break;
                    case "Provocation":
                        skills.provocation = s;
                        break;
                    case "Tactics":
                        skills.tactics = s;
                        break;
                    case "Cooking":
                        skills.cooking = s;
                        break;
                    case "Fencing":
                        skills.fencing = s;
                        break;
                    case "MaceFighting":
                        skills.maceFighting = s;
                        break;
                    case "ForensicEval":
                        skills.forensicEval = s;
                        break;
                    case "Meditation":
                        skills.meditation = s;
                        break;
                    case "Fishing":
                        skills.fishing = s;
                        break;
                    case "Stealing":
                        skills.stealing = s;
                        break;
                    case "Poisoning":
                        skills.poisoning = s;
                        break;
                    case "ItemIdentification":
                        skills.itemIdentification = s;
                        break;
                    case "Snooping":
                        skills.snooping = s;
                        break;
                    case "MagicResist":
                        skills.magicResist = s;
                        break;
                    case "DetectHidden":
                        skills.detectHidden = s;
                        break;
                    case "Alchemy":
                        skills.alchemy = s;
                        break;
                    case "Anatomy":
                        skills.anatomy = s;
                        break;
                    case "Discordance":
                        skills.discordance = s;
                        break;
                    case "Carpentry":
                        skills.carpentry = s;
                        break;
                    case "Swordsmanship":
                        skills.swordsmanship = s;
                        break;
                    case "Musicianship":
                        skills.musicianship = s;
                        break;
                    case "SpiritSpeak":
                        skills.spiritSpeak = s;
                        break;
                    case "Lumberjack":
                        skills.lumberjack = s;
                        break;
                    case "Hiding":
                        skills.hiding = s;
                        break;
                    case "Lockpicking":
                        skills.lockpicking = s;
                        break;
                    case "ArmsLore":
                        skills.armsLore = s;
                        break;
                    case "Peacemaking":
                        skills.peacemaking = s;
                        break;
                    case "AnimalLore":
                        skills.animalLore = s;
                        break;
                    case "AnimalTaming":
                        skills.animalTaming = s;
                        break;
                    case "Camping":
                        skills.camping = s;
                        break;
                }
            }

            table.Dispose();
            return skills;
        }

        public static Stats ExtractStats(LuaTable table) {
            var stats = new Stats();
            if (table["strength"] != null && int.TryParse(table["strength"] as string, out int str)) {
                stats.strength = str;
            }

            if (table["dexterity"] != null && int.TryParse(table["dexterity"] as string, out int dex)) {
                stats.dexterity = dex;
            }

            if (table["intelligence"] != null && int.TryParse(table["intelligence"] as string, out int intel)) {
                stats.intelligence = intel;
            }

            if (table["baseStrength"] != null && int.TryParse(table["baseStrength"] as string, out int baseStr)) {
                stats.baseStrength = baseStr;
            }

            if (table["baseDexterity"] != null && int.TryParse(table["baseDexterity"] as string, out int baseDex)) {
                stats.baseDexterity = baseDex;
            }

            if (table["baseIntelligence"] != null &&
                int.TryParse(table["baseIntelligence"] as string, out int baseIntel)) {
                stats.baseIntelligence = baseIntel;
            }

            table.Dispose();
            return stats;
        }

        public static List<Mobile> ExtractMobiles(LuaTable table) {
            var mobiles = new List<Mobile>();
            foreach (LuaTable mobile in table.Values) {
                if (mobile["id"] == null) {
                    continue;
                }

                var m = new Mobile();
                m.id = "" + mobile["id"];

                if (mobile["clientId"] != null) {
                    m.clientId = "" + mobile["clientId"];
                }

                if (mobile["templateId"] != null) {
                    m.templateId = "" + mobile["templateId"];
                }

                if (mobile["type"] != null) {
                    m.type = "" + mobile["type"];
                }

                if (mobile["name"] != null) {
                    m.name = "" + mobile["name"];
                }

                if (mobile["tags"] != null) {
                    m.tags = ExtractTags(mobile["tags"] as LuaTable);
                }

                mobiles.Add(m);
            }

            table.Dispose();
            return mobiles;
        }

        public static List<Location> ExtractLocations(LuaTable table) {
            var locations = new List<Location>();
            foreach (LuaTable location in table.Values) {
                if (location["x"] == null || location["z"] == null) {
                    continue;
                }

                var l = new Location();
                l.x = float.Parse(location["x"] as string);
                l.z = float.Parse(location["z"] as string);

                if (location["y"] != null) {
                    l.y = float.Parse(location["y"] as string);
                }

                if (location["region"] != null) {
                    l.region = "" + location["region"];
                }

                if (location["subregion"] != null) {
                    l.subregion = "" + location["subregion"];
                }

                if (location["worldName"] != null) {
                    l.worldName = "" + location["worldName"];
                }

                if (location["tags"] != null) {
                    l.tags = ExtractTags(location["tags"] as LuaTable);
                }

                locations.Add(l);
            }

            table.Dispose();
            return locations;
        }

        public static List<Object> ExtractObjects(LuaTable table) {
            var objects = new List<Object>();
            foreach (LuaTable obj in table.Values) {
                if (obj["id"] == null) {
                    continue;
                }

                var o = new Object();
                o.id = "" + obj["id"];

                if (obj["clientId"] != null) {
                    o.clientId = "" + obj["clientId"];
                }

                if (obj["templateId"] != null) {
                    o.templateId = "" + obj["templateId"];
                }

                if (obj["name"] != null) {
                    o.name = "" + obj["name"];
                }

                if (obj["ownerId"] != null) {
                    o.ownerId = "" + obj["ownerId"];
                }

                if (obj["isCurrency"] != null) {
                    o.isCurrency = obj["isCurrency"] is bool ? (bool) obj["isCurrency"] : false;
                }

                if (obj["count"] != null && int.TryParse(obj["count"] as string, out int count)) {
                    o.count = count;
                }

                if (obj["value"] != null && double.TryParse(obj["value"] as string, out double value)) {
                    o.value = value;
                }

                if (obj["tags"] != null) {
                    o.tags = ExtractTags(obj["tags"] as LuaTable);
                }

                objects.Add(o);
            }
            
            table.Dispose();
            return objects;
        }

        public static EquipmentObject ExtractEquipmentObject(LuaTable obj) {
            var e = new EquipmentObject();
            e.id = "" + obj["id"];

            if (obj["clientId"] != null) {
                e.clientId = "" + obj["clientId"];
            }

            if (obj["templateId"] != null) {
                e.templateId = "" + obj["templateId"];
            }

            if (obj["name"] != null) {
                e.name = "" + obj["name"];
            }

            obj.Dispose();
            return e;
        }

        public static Equipment ExtractEquipment(LuaTable table) {
            var equipment = new Equipment();
            foreach (var slotName in table.Keys) {
                LuaTable obj = table[slotName] as LuaTable;
                if (obj["id"] == null) {
                    continue;
                }

                var equipObj = ExtractEquipmentObject(obj);
                
                switch (slotName) {
                    case "Head":
                        equipment.head = equipObj;
                        break;
                    case "Cloak":
                        equipment.cloak = equipObj;
                        break;
                    case "Neck":
                        equipment.cloak = equipObj;
                        break;
                    case "Earrings":
                        equipment.earrings = equipObj;
                        break;
                    case "Ring":
                        equipment.ring = equipObj;
                        break;
                    case "Chest":
                        equipment.chest = equipObj;
                        break;
                    case "Legs":
                        equipment.legs = equipObj;
                        break;
                    case "RightHand":
                        equipment.rightHand = equipObj;
                        break;
                    case "LeftHand":
                        equipment.leftHand = equipObj;
                        break;
                }
            }

            table.Dispose();
            return equipment;
        }

        public static List<Tag> ExtractTags(LuaTable tags) {
            var t = new List<Tag>();
            foreach (LuaTable tag in tags.Values) {
                var newTag = new Tag {name = "" + tag["name"]};
                if (tag["value"] != null) {
                    newTag.value = "" + tag["value"];
                }

                t.Add(newTag);
            }

            tags.Dispose();
            return t;
        }
    }
}