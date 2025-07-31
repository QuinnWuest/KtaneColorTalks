using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class crazyTalkWithAKScript : MonoBehaviour
{
    private static readonly string[] _words = { "ABIDE", "ABORT", "ABOUT", "ABOVE", "ABYSS", "ACIDS", "ACORN", "ACRES", "ACTED", "ACTOR", "ACUTE", "ADDER", "ADDLE", "ADIEU", "ADIOS", "ADMIN", "ADMIT", "ADOPT", "ADORE", "ADORN", "ADULT", "AFFIX", "AFTER", "AGILE", "AGING", "AGORA", "AGREE", "AHEAD", "AIDED", "AIMED", "AIOLI", "AIRED", "AISLE", "ALARM", "ALBUM", "ALIAS", "ALIBI", "ALIEN", "ALIGN", "ALIKE", "ALIVE", "ALLAY", "ALLEN", "ALLOT", "ALLOY", "ALOFT", "ALONE", "ALONG", "ALOOF", "ALOUD", "ALPHA", "ALTAR", "ALTER", "AMASS", "AMAZE", "AMBLE", "AMINO", "AMISH", "AMISS", "AMONG", "AMPLE", "AMUSE", "ANGLE", "ANGLO", "ANGRY", "ANGST", "ANIME", "ANION", "ANISE", "ANKLE", "ANNEX", "ANNOY", "ANNUL", "ANTIC", "ANVIL", "AORTA", "APNEA", "APPLE", "APRON", "AREAS", "ARENA", "ARGUE", "ARISE", "ARMED", "ARMOR", "AROSE", "ASHEN", "ASHES", "ASIAN", "ASIDE", "ASSET", "ASTER", "ASTIR", "ATOLL", "ATOMS", "ATONE", "ATTIC", "AUDIO", "AUDIT", "AUGUR", "AUNTY", "AVAIL", "AVIAN", "AVOID", "AWAIT", "AWAKE", "AWARE", "AWASH", "AXIAL", "AXIOM", "AXION", "AZTEC", "BIBLE", "BIDET", "BIGHT", "BILGE", "BILLS", "BINGE", "BINGO", "BIOME", "BIRCH", "BIRDS", "BIRTH", "BISON", "BITER", "BLADE", "BLAME", "BLAND", "BLARE", "BLAZE", "BLEAT", "BLEED", "BLEEP", "BLIMP", "BLIND", "BLING", "BLINK", "BLISS", "BLITZ", "BLOND", "BLOOM", "BLOOP", "BLUES", "BLUNT", "BLUSH", "BOGGY", "BOGUS", "BOLTS", "BONDS", "BONED", "BONER", "BONES", "BONNY", "BONUS", "BOOST", "BOOTH", "BOOTS", "BORAX", "BORED", "BORER", "BORNE", "BORON", "BOTCH", "BOUGH", "BOULE", "BRACE", "BRAID", "BRAIN", "BRAKE", "BRAND", "BRASH", "BRASS", "BRAVE", "BRAWL", "BRAWN", "BRAZE", "BREAD", "BREAK", "BREAM", "BREED", "BRIAR", "BRIBE", "BRICK", "BRIDE", "BRIEF", "BRIER", "BRINE", "BRING", "BRINK", "BRINY", "BRISK", "BROIL", "BRONX", "BROOM", "BROTH", "BRUNT", "BRUSH", "BRUTE", "BUCKS", "BUDDY", "BUDGE", "BUGGY", "BUILD", "BUILT", "BULBS", "BULGE", "BULLS", "BUMPY", "BUNCH", "BUNNY", "BUSES", "BUZZY", "BYLAW", "BYWAY", "CABBY", "CABIN", "CABLE", "CACHE", "CAIRN", "CAKES", "CALLS", "CALVE", "CALYX", "CAMPS", "CAMPY", "CANAL", "CANED", "CANNY", "CANON", "CARDS", "CARVE", "CASED", "CASES", "CASTE", "CATCH", "CAULK", "CAUSE", "CAVES", "CEASE", "CEDED", "CELLS", "CENTS", "CHAFE", "CHAFF", "CHAIN", "CHAIR", "CHALK", "CHAMP", "CHANT", "CHAOS", "CHAPS", "CHARM", "CHART", "CHARY", "CHASE", "CHASM", "CHEAP", "CHEAT", "CHECK", "CHEMO", "CHESS", "CHEST", "CHICK", "CHIDE", "CHILD", "CHILI", "CHILL", "CHIME", "CHINA", "CHIPS", "CHORD", "CHORE", "CHOSE", "CHUCK", "CHUNK", "CHUTE", "CINCH", "CITED", "CITES", "CIVET", "CIVIC", "CIVIL", "CLADE", "CLAIM", "CLANK", "CLASH", "CLASS", "CLAWS", "CLEAN", "CLEAR", "CLEAT", "CLICK", "CLIFF", "CLIMB", "CLING", "CLONE", "CLOSE", "CLOTH", "CLOUD", "CLOUT", "CLOVE", "CLUBS", "CLUCK", "CLUES", "CLUNG", "CLUNK", "COINS", "COLIC", "COLON", "COLOR", "COMAL", "COMES", "COMIC", "COMMA", "CONCH", "CONIC", "CORAL", "CORGI", "CORNY", "CORPS", "COSTS", "COTTA", "COUCH", "COUGH", "COULD", "COUNT", "COVEN", "COYLY", "CRACK", "CRANE", "CRANK", "CRASH", "CRASS", "CRATE", "CRAVE", "CREAK", "CREAM", "CREED", "CREWS", "CRIED", "CRIES", "CRIME", "CRONE", "CROPS", "CROSS", "CRUDE", "CRUEL", "CRUSH", "CUBBY", "CUBIT", "CUMIN", "CUTIE", "CYCLE", "CYNIC", "CZECH", "DACHA", "DAILY", "DALLY", "DANCE", "DATED", "DATES", "DATUM", "DEALS", "DEALT", "DEATH", "DEBIT", "DEBTS", "DEBUG", "DEBUT", "DECAF", "DECAL", "DECOR", "DECOY", "DEEDS", "DEIST", "DELFT", "DELVE", "DEMUR", "DENIM", "DENSE", "DESKS", "DETER", "DETOX", "DEUCE", "DEVIL", "DICED", "DIETS", "DIGIT", "DIMLY", "DINAR", "DINER", "DINGY", "DIRTY", "DISCO", "DISCS", "DISKS", "DITCH", "DITTY", "DITZY", "DIVAN", "DIVED", "DIVER", "DIVOT", "DIVVY", "DOGGY", "DOGMA", "DOING", "DOLLS", "DOMED", "DONOR", "DONUT", "DOORS", "DORIC", "DOSED", "DOSES", "DOTTY", "DOUGH", "DOUSE", "DRAFT", "DRAIN", "DRAMA", "DRANK", "DREAM", "DRESS", "DRIED", "DRIER", "DRIFT", "DRILL", "DRILY", "DRINK", "DRIVE", "DROLL", "DRONE", "DROPS", "DROVE", "DRUGS", "DRUMS", "DRUNK", "DRYER", "DUCAT", "DUCKS", "DUMMY", "DUNCE", "DUNES", "DUTCH", "DUVET", "DWARF", "DWELL", "DYING", "EAGLE", "EARED", "EARLY", "EARTH", "EASED", "EASEL", "EATEN", "ECLAT", "EDEMA", "EDICT", "EDIFY", "EGRET", "EIDER", "EIGHT", "ELATE", "ELDER", "ELECT", "ELIDE", "ELITE", "ELUDE", "EMCEE", "EMOTE", "ENACT", "ENEMA", "ENNUI", "ENSUE", "ENTER", "ENVOY", "ETHIC", "ETHOS", "ETUDE", "EVADE", "EVICT", "EXACT", "EXALT", "EXAMS", "EXILE", "EXIST", "EXTRA", "EXUDE", "FAILS", "FAINT", "FAIRY", "FAITH", "FALLS", "FALSE", "FAMED", "FANCY", "FATAL", "FATED", "FATTY", "FATWA", "FAULT", "FAVOR", "FECAL", "FEINT", "FETAL", "FIBRE", "FIFTH", "FIFTY", "FIGHT", "FILCH", "FILED", "FILET", "FILLE", "FILLS", "FILLY", "FILMS", "FILMY", "FILTH", "FINAL", "FINDS", "FINED", "FINNY", "FIRED", "FIRMS", "FIRST", "FISTS", "FLAIL", "FLAIR", "FLATS", "FLEET", "FLING", "FLIRT", "FLOOR", "FLORA", "FLOUT", "FLUID", "FLUNG", "FLYBY", "FOGGY", "FOIST", "FOLIC", "FOLIO", "FOLLY", "FONTS", "FORAY", "FORGO", "FORMS", "FORTE", "FORTH", "FORTY", "FORUM", "FOUNT", "FOVEA", "FRAIL", "FRAUD", "FREED", "FRIED", "FRILL", "FRISK", "FRONT", "FRUIT", "FUELS", "FULLY", "FUNNY", "FUSED", "FUTON", "FUZZY", "GHOUL", "GIANT", "GIDDY", "GIFTS", "GIMPY", "GIRLS", "GIRLY", "GIRTH", "GIVEN", "GIZMO", "GLAND", "GLEAM", "GLEAN", "GLIAL", "GLINT", "GLOOM", "GLORY", "GLUED", "GLUON", "GOING", "GOLLY", "GOOFY", "GOOPY", "GRAFT", "GRAIN", "GRAND", "GRANT", "GRASS", "GRATE", "GRAVE", "GRAVY", "GREAT", "GREED", "GREEN", "GREET", "GRILL", "GRIME", "GRIMY", "GRIND", "GRIPS", "GROIN", "GROOM", "GROSS", "GROUT", "GRUEL", "GRUMP", "GRUNT", "GUANO", "GUARD", "GUAVA", "GUEST", "GUILD", "GUILT", "GUISE", "GULLS", "GULLY", "GUMMY", "GUNKY", "GUNNY", "GUSHY", "GUSTY", "GUTSY", "GYRUS", "HABIT", "HAIKU", "HAIRS", "HAIRY", "HALAL", "HALVE", "HAMMY", "HANDS", "HANDY", "HANGS", "HARDY", "HAREM", "HARPY", "HARSH", "HASTE", "HASTY", "HATCH", "HATED", "HATES", "HAUNT", "HAVEN", "HAZEL", "HEADS", "HEADY", "HEARD", "HEARS", "HEART", "HEATH", "HEAVE", "HEAVY", "HEELS", "HEIRS", "HEIST", "HELIX", "HELLO", "HENRY", "HILLS", "HILLY", "HINDI", "HINDU", "HINTS", "HIRED", "HITCH", "HOBBY", "HOIST", "HOLLY", "HOMED", "HONOR", "HORNS", "HORSE", "HOSEL", "HOTLY", "HOUND", "HUBBY", "HUGGY", "HULLO", "HUMAN", "HUMID", "HUMOR", "ICHOR", "ICILY", "ICING", "ICONS", "IDEAL", "IDEAS", "IDIOM", "IDIOT", "IDLED", "IDYLL", "IGLOO", "ILIAC", "ILIUM", "IMAGO", "IMBUE", "IMPLY", "INANE", "INCAN", "INCUS", "INDEX", "INDIA", "INDIE", "INFRA", "INGOT", "INLAY", "INLET", "INPUT", "INSET", "INTRO", "INUIT", "IONIC", "IRISH", "IRONY", "ISLET", "ISSUE", "ITEMS", "IVORY", "JOINS", "JOINT", "JOULE", "JUMBO", "JUNTA", "KANJI", "KARAT", "KARMA", "KIDDO", "KILLS", "KINDA", "KINDS", "KINGS", "KITTY", "KNAVE", "KNEES", "KNELT", "KNIFE", "KNOBS", "KNOLL", "KNOTS", "KUDOS", "KUDZU", "LABOR", "LACED", "LACKS", "LADLE", "LAITY", "LAMBS", "LAMPS", "LANDS", "LANES", "LAPIN", "LAPSE", "LARGE", "LARVA", "LASER", "LASSO", "LATCH", "LATER", "LATHE", "LATIN", "LATTE", "LAUGH", "LAWNS", "LAYER", "LAYUP", "LEACH", "LEADS", "LEAFY", "LEANT", "LEAPT", "LEARN", "LEASE", "LEASH", "LEAVE", "LEDGE", "LEECH", "LEGGY", "LEMMA", "LEMON", "LEMUR", "LIANA", "LIDAR", "LIEGE", "LIFTS", "LIGHT", "LIKEN", "LIKES", "LILAC", "LIMBO", "LIMBS", "LIMIT", "LINED", "LINEN", "LINER", "LINES", "LINGO", "LINKS", "LIONS", "LIPID", "LISTS", "LITER", "LITRE", "LIVED", "LIVEN", "LIVER", "LIVES", "LIVID", "LLAMA", "LOBBY", "LOFTY", "LOGIC", "LOGON", "LOLLY", "LONER", "LOONY", "LOOPS", "LOOPY", "LOOSE", "LORDS", "LORRY", "LOSER", "LOSES", "LOTTO", "LOTUS", "LOUSE", "LOVED", "LOVER", "LOVES", "LOYAL", "LUCID", "LUCRE", "LUMEN", "LUMPS", "LUMPY", "LUNAR", "LUNCH", "LUNGE", "LUNGS", "LUSTY", "LYING", "LYMPH", "LYNCH", "LYRIC", "MADAM", "MADLY", "MAGIC", "MAGMA", "MAINS", "MAJOR", "MALAY", "MALTA", "MAMBO", "MANGO", "MANGY", "MANIA", "MANIC", "MANLY", "MANOR", "MASKS", "MATCH", "MATED", "MATHS", "MATTE", "MAVEN", "MAXIM", "MAYAN", "MAYOR", "MEALS", "MEANS", "MEANT", "MEATY", "MEDAL", "MEDIA", "MEDIC", "MEETS", "MELON", "MESON", "METAL", "MICRO", "MIDST", "MIGHT", "MILES", "MILLS", "MIMIC", "MINCE", "MINDS", "MINED", "MINES", "MINOR", "MINTY", "MINUS", "MIRED", "MIRTH", "MISTY", "MITRE", "MOGUL", "MOIST", "MOLAR", "MOLDY", "MONTH", "MOONY", "MOORS", "MOOSE", "MORAL", "MORAY", "MORPH", "MOTEL", "MOTIF", "MOTOR", "MOTTO", "MOULD", "MOUND", "MOUNT", "MOUSE", "MOUTH", "MOVED", "MOVIE", "MUCUS", "MUDDY", "MUGGY", "MULCH", "MULTI", "MUMMY", "MUNCH", "MUSED", "MUSIC", "MUSTY", "MUTED", "MUZZY", "MYTHS", "NADIR", "NAILS", "NAIVE", "NAMED", "NAMES", "NANNY", "NARCO", "NASAL", "NATAL", "NATTY", "NAVAL", "NAVEL", "NEATH", "NECKS", "NEEDS", "NEEDY", "NEIGH", "NESTS", "NEWLY", "NEXUS", "NICER", "NICHE", "NIECE", "NIFTY", "NIGHT", "NIGRA", "NINTH", "NITRO", "NOBLE", "NOBLY", "NOISE", "NOMAD", "NOMES", "NONCE", "NOOSE", "NORMS", "NORSE", "NORTH", "NOSES", "NOTCH", "NOTED", "NOTES", "NOVEL", "NUDGE", "NUTTY", "NYLON", "NYMPH", "OFFER", "OFTEN", "OILED", "OLDER", "OLDIE", "OLIVE", "OMANI", "ONION", "ONSET", "OOMPH", "OPINE", "OPIUM", "OPTIC", "ORBIT", "ORDER", "OTHER", "OTTER", "OUGHT", "OUNCE", "OUTDO", "OUTER", "OVOID", "OVULE", "OXBOW", "OXIDE", "PHASE", "PHONE", "PHOTO", "PIANO", "PIECE", "PIGGY", "PILAF", "PILED", "PILLS", "PILOT", "PINCH", "PINTS", "PISTE", "PITCH", "PIVOT", "PIXEL", "PIXIE", "PLACE", "PLAIN", "PLAIT", "PLANE", "PLANS", "PLANT", "PLATE", "PLAYS", "PLEAS", "PLEAT", "PLOTS", "PLUMB", "PLUME", "PLUMP", "POINT", "POLAR", "POLIO", "POLLS", "POLYP", "PONDS", "POOLS", "PORCH", "PORTS", "POSED", "POSIT", "POSTS", "POUCH", "PREEN", "PRICE", "PRICY", "PRIDE", "PRIMA", "PRIME", "PRIMP", "PRINT", "PRION", "PRIOR", "PRISE", "PRISM", "PRIVY", "PRIZE", "PROMO", "PRONE", "PRONG", "PROOF", "PROSE", "PROUD", "PROVE", "PRUDE", "PRUNE", "PUDGY", "PULLS", "PUNCH", "PUNIC", "PYLON", "QUAIL", "QUALM", "QUASI", "QUEEN", "QUELL", "QUILL", "QUILT", "QUINT", "RABBI", "RADAR", "RADIO", "RAGGY", "RAIDS", "RAILS", "RAINY", "RAISE", "RALLY", "RANCH", "RANGE", "RANGY", "RATED", "RATIO", "RATTY", "RAZOR", "REACH", "REACT", "READS", "REALM", "REARM", "RECAP", "RECON", "RECTO", "REDLY", "REEDY", "REHAB", "REINS", "RELIC", "REMIT", "RENTS", "RESTS", "RETRO", "RHINO", "RIDGE", "RIFLE", "RIGHT", "RIGID", "RIGOR", "RILED", "RINGS", "RINSE", "RIOTS", "RISEN", "RISES", "RISKS", "RITZY", "RIVAL", "RIVEN", "RIVET", "ROBOT", "ROILY", "ROLLS", "ROMAN", "ROOFS", "ROOMS", "ROOTS", "ROSIN", "ROTOR", "ROUGE", "ROUGH", "ROUTE", "ROYAL", "RUDDY", "RUGBY", "RUINS", "RULED", "RUMBA", "RUMMY", "RUMOR", "RUNIC", "RUNNY", "RUNTY", "SABLE", "SADLY", "SAFER", "SAGGY", "SAILS", "SAINT", "SALAD", "SALES", "SALLY", "SALON", "SALSA", "SALTS", "SALTY", "SALVE", "SAMBA", "SANDS", "SANDY", "SATED", "SATIN", "SATYR", "SAUCE", "SAUCY", "SAUDI", "SAUNA", "SAVED", "SAVER", "SAVOR", "SAVVY", "SAXON", "SCALD", "SCALE", "SCALP", "SCALY", "SCAMP", "SCANT", "SCAPE", "SCARE", "SCARF", "SCARP", "SCARS", "SCARY", "SCENE", "SCENT", "SCHMO", "SCOFF", "SCOLD", "SCONE", "SCOOP", "SCOOT", "SCOPE", "SCORE", "SCORN", "SCOTS", "SCOUR", "SCOUT", "SCRAM", "SCRAP", "SCREE", "SCREW", "SCRIM", "SCRIP", "SCRUB", "SCRUM", "SCUBA", "SCULL", "SEALS", "SEAMS", "SEAMY", "SEATS", "SEDAN", "SEEDS", "SEEDY", "SEEMS", "SEGUE", "SEIZE", "SELLS", "SENDS", "SENSE", "SETUP", "SEXES", "SHADE", "SHADY", "SHAFT", "SHAKE", "SHALE", "SHALL", "SHAME", "SHANK", "SHAPE", "SHARD", "SHARE", "SHARP", "SHAVE", "SHAWL", "SHEAF", "SHEAR", "SHEEN", "SHEET", "SHELF", "SHELL", "SHIFT", "SHILL", "SHINE", "SHINY", "SHIPS", "SHIRE", "SHIRT", "SHONE", "SHOOT", "SHOPS", "SHORE", "SHORN", "SHORT", "SHOTS", "SHOUT", "SHOVE", "SHUNT", "SHUSH", "SIDES", "SIDLE", "SIEGE", "SIGHT", "SIGIL", "SIGNS", "SILLY", "SILTY", "SINCE", "SINEW", "SINGE", "SINGS", "SINUS", "SITAR", "SITES", "SIXTH", "SIXTY", "SIZED", "SIZES", "SKALD", "SKANK", "SKATE", "SKEIN", "SKIER", "SKIES", "SKIFF", "SKILL", "SKIMP", "SKINS", "SKIRT", "SKULL", "SLABS", "SLAIN", "SLAKE", "SLANG", "SLANT", "SLASH", "SLATE", "SLEEP", "SLEET", "SLICE", "SLIDE", "SLIME", "SLIMY", "SLING", "SLINK", "SLOPE", "SLOSH", "SLOTH", "SLOTS", "SLUMP", "SLUSH", "SLYLY", "SMALL", "SMART", "SMASH", "SMEAR", "SMELL", "SMELT", "SMILE", "SMITE", "SNAIL", "SNAKE", "SNARE", "SNARL", "SNEER", "SNIDE", "SNIFF", "SNIPE", "SNOOP", "SNORE", "SNORT", "SNOUT", "SOFTY", "SOGGY", "SOILS", "SOLAR", "SOLID", "SOLVE", "SONAR", "SONGS", "SONIC", "SOOTH", "SORRY", "SORTS", "SOUGH", "SOULS", "SOUTH", "STAFF", "STAGE", "STAIN", "STAIR", "STAKE", "STALE", "STALL", "STAMP", "STAND", "START", "STASH", "STATE", "STEAD", "STEAL", "STEAM", "STEEL", "STEEP", "STEER", "STEMS", "STENO", "STIFF", "STILE", "STILL", "STILT", "STING", "STINK", "STINT", "STOIC", "STOLE", "STOMP", "STONE", "STONY", "STOOL", "STOOP", "STOPS", "STORE", "STORK", "STORM", "STORY", "STOUT", "STOVE", "STRAP", "STRAW", "STREP", "STREW", "STRIP", "STRUM", "STRUT", "STUCK", "STUDY", "STUMP", "STUNT", "SUAVE", "SUEDE", "SUITE", "SUITS", "SULLY", "SUNNY", "SUNUP", "SUSHI", "SWALE", "SWAMI", "SWAMP", "SWANK", "SWANS", "SWARD", "SWARM", "SWASH", "SWATH", "SWAZI", "SWEAR", "SWEAT", "SWELL", "SWIFT", "SWILL", "SWINE", "SWING", "SWIPE", "SWIRL", "SWISH", "SWISS", "SWOON", "SWOOP", "SWORD", "SWORE", "SWORN", "SWUNG", "TACIT", "TAFFY", "TAILS", "TAINT", "TAKEN", "TALLY", "TALON", "TAMED", "TAMIL", "TANGO", "TANGY", "TARDY", "TAROT", "TARRY", "TASKS", "TATTY", "TAUNT", "TAWNY", "TAXES", "TAXON", "TEACH", "TEAMS", "TEARS", "TEARY", "TEASE", "TECHY", "TEDDY", "TEENS", "TEENY", "TEETH", "TELLS", "TELLY", "TENOR", "TENSE", "TENTH", "TENTS", "TEXAS", "THANK", "THEIR", "THEME", "THESE", "THETA", "THIGH", "THINE", "THING", "THINK", "THIRD", "THONG", "THORN", "THOSE", "THUMB", "TIARA", "TIBIA", "TIDAL", "TIGHT", "TILDE", "TILED", "TILES", "TILTH", "TIMED", "TIMES", "TIMID", "TINES", "TINNY", "TIPSY", "TIRED", "TITLE", "TOMMY", "TONAL", "TONED", "TONGS", "TONIC", "TONNE", "TOOLS", "TOONS", "TOOTH", "TOPIC", "TOPSY", "TOQUE", "TORCH", "TORSO", "TORTE", "TORUS", "TOTAL", "TOTEM", "TOUCH", "TOXIC", "TOXIN", "TRACT", "TRAIL", "TRAIN", "TRAIT", "TRAMS", "TRAWL", "TREAD", "TREAT", "TRIAD", "TRIAL", "TRIED", "TRIKE", "TRILL", "TRITE", "TROLL", "TROOP", "TROUT", "TRUCE", "TRUCK", "TRULY", "TRUST", "TRUTH", "TUBBY", "TULIP", "TUMMY", "TUNED", "TUNIC", "TUTEE", "TUTOR", "TWANG", "TWEAK", "TWINS", "TWIRL", "TWIST", "TYING", "UDDER", "ULCER", "ULNAR", "ULTRA", "UMBRA", "UNCAP", "UNCLE", "UNCUT", "UNDER", "UNDUE", "UNFED", "UNFIT", "UNHIP", "UNIFY", "UNION", "UNITE", "UNITS", "UNITY", "UNLIT", "UNMET", "UNSAY", "UNTIE", "UNTIL", "UNZIP", "USAGE", "USHER", "USING", "USUAL", "UTTER", "UVULA", "VAGUE", "VALET", "VALID", "VALOR", "VALUE", "VAPOR", "VAULT", "VAUNT", "VEDIC", "VEINS", "VEINY", "VENAL", "VENOM", "VICAR", "VIEWS", "VIGIL", "VIGOR", "VILLA", "VINES", "VINYL", "VIRAL", "VIRUS", "VISIT", "VISOR", "VITAL", "VIVID", "VIXEN", "VOGUE", "VOTED", "VOUCH", "VROOM", "WAGON", "WAIST", "WAITS", "WAIVE", "WALKS", "WALLS", "WALTZ", "WANTS", "WARDS", "WARES", "WARNS", "WASTE", "WATCH", "WAVED", "WAVES", "WAXEN", "WEARS", "WEARY", "WEAVE", "WEBBY", "WELLS", "WETLY", "WHALE", "WHEAT", "WHEEL", "WHICH", "WHILE", "WHINE", "WHITE", "WHORL", "WHOSE", "WIDTH", "WIELD", "WILLS", "WIMPY", "WINCE", "WINCH", "WINDS", "WINES", "WINGS", "WITCH", "WITTY", "WIVES", "WOMAN", "WORDS", "WORKS", "WORLD", "WORMS", "WORMY", "WORSE", "WORST", "WORTH", "WOULD", "WOUND", "XENON", "YARDS", "YAWNS", "YEARN", "YEARS", "YOUNG", "YOUTH", "YUCCA", "YUMMY", "ZILCH", "ZINGY", "ZONAL", "ZONES" };

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    public GameObject SphereObj;
    public Transform WordParent;
    public GameObject OriginParent;
    public GameObject OriginObj;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    // _font[row][ltr][bit]
    private static readonly bool[][][] _font = ".#.,##.,.#,##.,##,##,.##,#.#,#,.#,#.#,#.,#...#,#..#,.#.,##.,.#.,##.,.##,###,#.#,#...#,#...#,#.#,#.#,###;#.#,#.#,#.,#.#,#.,#.,#..,#.#,#,.#,#.#,#.,##.##,##.#,#.#,#.#,#.#,#.#,#..,.#.,#.#,#...#,#...#,#.#,#.#,..#;###,##.,#.,#.#,##,##,#.#,###,#,.#,##.,#.,#.#.#,#.##,#.#,##.,#.#,##.,.#.,.#.,#.#,.#.#.,#.#.#,.#.,.#.,.#.;#.#,#.#,#.,#.#,#.,#.,#.#,#.#,#,.#,#.#,#.,#...#,#.##,#.#,#..,###,#.#,..#,.#.,#.#,.#.#.,##.##,#.#,.#.,#..;#.#,##.,.#,##.,##,#.,.##,#.#,#,#.,#.#,##,#...#,#..#,.#.,#..,.##,#.#,##.,.#.,.#.,..#..,#...#,#.#,.#.,###"
    .Split(';').Select(row => row.Split(',').Select(str => str.Select(ch => ch == '#').ToArray()).ToArray()).ToArray();

    private string _chosenWord;
    private List<bool> _wordInFont;
    private int _width;
    private const float _size = 0.0075f;

    private List<Vector3> _spherePoses = new List<Vector3>();

    void Awake()
    {
        moduleId = moduleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };

    }

    void Start()
    {
        _chosenWord = _words[Rnd.Range(0, _words.Length)];
        _wordInFont = new List<bool>();
        for (int row = 0; row < 5; row++)
        {
            for (int lIx = 0; lIx < _chosenWord.Length; lIx++)
            {
                _wordInFont.AddRange(_font[row][_chosenWord[lIx] - 'A']);
                if (lIx != 4)
                    _wordInFont.Add(false);
            }
        }

        const float textDist = 20;
        const float cameraDist = 100;

        var width = _wordInFont.Count / 5;
        for (int i = 0; i < _wordInFont.Count; i++)
        {
            if (!_wordInFont[i])
                continue;

            // Position of the sphere relative to center of module
            float x = i % width - (width * 0.5f);
            float z = -i / width + 2.5f;
            var pos = new Vector3(x, textDist, z);

            // Assume camera is at y = 40
            // Vector of the sphere relative to the camera
            var rel = pos - new Vector3(0, cameraDist, 0);

            // Move the spheres towards/away from the camera
            var multFactor = Rnd.Range(0.75f, 1.1f);
            var movedRel = rel * multFactor;

            // Get the position of the sphere relative to the module
            var finalPos = movedRel + new Vector3(0, cameraDist, 0);

            var obj = Instantiate(SphereObj);
            obj.transform.SetParent(WordParent);
            obj.transform.localPosition = finalPos * _size;
            obj.transform.localScale = new Vector3(_size, _size, _size) * multFactor;
        }
        Debug.Log(_chosenWord);

        WordParent.transform.localEulerAngles = new Vector3(Rnd.Range(-30f, 30f), Rnd.Range(0, 360f), 0f);
    }
}
