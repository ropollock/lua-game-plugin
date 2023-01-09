using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lou.Services {
    public class TelemetryEvent {
        public string type;
        public string timestamp;
        [JsonProperty("total_object_value")]
        public double totalObjectValue;
        public List<Location> locations;
        public List<Action> actions;
        public List<Object> objects;
        public List<Player> players;
        public List<Mobile> mobiles;
    }

    public class Tag {
        public string name;
        public string value;
    }

    public class Location {
        public string region;
        public string subregion;

        [JsonProperty("world_name")]
        public string worldName;

        public float x;
        public float y;
        public float z;
        public List<Tag> tags;
    }

    public class Action {
        public string type;

        [JsonProperty("actor_id")]
        public string actorId;

        [JsonProperty("target_id")]
        public string targetId;

        public float magnitude;
        public List<Tag> tags;
    }

    public class Player {
        public string id;
        public string ip;

        [JsonProperty("user_id")]
        public string userId;
        
        [JsonProperty("access_level")]
        public string accessLevel;
        
        [JsonProperty("faction_id")]
        public string factionId;
        
        [JsonProperty("guild_id")]
        public string guildId;
        
        [JsonProperty("npc_guild_id")]
        public string npcGuildId;

        public string notoriety;

        public string character;

        [JsonProperty("murder_count")]
        public int murderCount;
        
        [JsonProperty("murder_short_term_count")]
        public int murderShortTermCount;
        
        [JsonProperty("murder_long_term_count")]
        public int murderLongTermCount;

        public float age;
        
        [JsonProperty("character_age")]
        public float characterAge;

        [JsonProperty("total_skill_level")]
        public float totalSkillLevel;

        public Skills skills;

        public List<string> enchants;

        [JsonProperty("plot_controllers")]
        public List<string> plotControllers;

        public Equipment equipment;

        public List<Tag> tags;

        public Stats stats;
    }

    public class Stats {
        public int strength;
        public int dexterity;
        public int intelligence;
        [JsonProperty("base_strength")]
        public int baseStrength;
        [JsonProperty("base_dexterity")]
        public int baseDexterity;
        [JsonProperty("base_intelligence")]
        public int baseIntelligence;
    }
    
    public class Skill {
        public float level;
    }

    public class Skills {
        public Skill necromancy;
        public Skill archery;
        public Skill tinkering;
        public Skill wrestling;
        public Skill herding;
        public Skill inscription;
        public Skill begging;
        public Skill veterinary;
        public Skill cartography;
        public Skill magery;
        public Skill fletching;
        public Skill tracking;
        public Skill blacksmithy;
        public Skill mining;
        public Skill healing;
        [JsonProperty("taste_id")]
        public Skill tasteId;
        public Skill parrying;
        public Skill tailoring;
        public Skill stealth;
        [JsonProperty("eval_int")]
        public Skill evalInt;
        public Skill provocation;
        public Skill tactics;
        public Skill cooking;
        public Skill fencing;
        [JsonProperty("mace_fighting")]
        public Skill maceFighting;
        [JsonProperty("forensic_eval")]
        public Skill forensicEval;
        public Skill meditation;
        public Skill fishing;
        public Skill stealing;
        public Skill poisoning;
        [JsonProperty("item_identification")]
        public Skill itemIdentification;
        public Skill snooping;
        [JsonProperty("magic_resist")]
        public Skill magicResist;
        [JsonProperty("detect_hidden")]
        public Skill detectHidden;
        public Skill alchemy;
        public Skill anatomy;
        public Skill discordance;
        public Skill carpentry;
        public Skill swordsmanship;
        public Skill musicianship;
        [JsonProperty("spirit_speak")]
        public Skill spiritSpeak;
        public Skill lumberjack;
        public Skill hiding;
        public Skill lockpicking;
        [JsonProperty("arms_lore")]
        public Skill armsLore;
        public Skill peacemaking;
        [JsonProperty("animal_lore")]
        public Skill animalLore;
        [JsonProperty("animal_taming")]
        public Skill animalTaming;
        public Skill camping;
    }

    public class EquipmentObject {
        public string id;
        public string name;
        public string tooltip;
        [JsonProperty("client_id")]
        public string clientId;
        [JsonProperty("template_id")]
        public string templateId;
    }
    
    public class Equipment {
        [JsonProperty("right_hand")]
        public EquipmentObject rightHand;
        [JsonProperty("left_hand")]
        public EquipmentObject leftHand;
        public EquipmentObject head;
        public EquipmentObject chest;
        public EquipmentObject legs;
        public EquipmentObject cloak;
        public EquipmentObject ring;
        public EquipmentObject earrings;
        public EquipmentObject neck;
    }

    public class Object {
        public string id;

        [JsonProperty("client_id")]
        public string clientId;

        [JsonProperty("template_id")]
        public string templateId;

        public string name;

        [JsonProperty("owner_id")]
        public string ownerId;

        public int count;

        public double value;

        [JsonProperty("is_currency")]
        public bool isCurrency;

        public List<Tag> tags;
    }

    public class Mobile {
        public string id;

        public string type;

        [JsonProperty("client_id")]
        public string clientId;

        [JsonProperty("template_id")]
        public string templateId;

        public string name;
        public List<Tag> tags;
    }
}