using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class insanagrams : MonoBehaviour {

    public KMBombInfo info;
    public KMBombModule module;
    public KMAudio newAudio;

    private static int _moduleIdCounter = 1;
    private int _moduleId;
    private bool _isSolved, _lightsOn;

    public KMSelectable[] buttons;
    public KMSelectable submit, clear;
    public GameObject[] buttonObjects;

    public TextMesh ana, ans;

    private String answer;

    private Dictionary<string, string> modules = new Dictionary<string, string>();
    private Dictionary<char, int> keys = new Dictionary<char, int>();
    private String[] moduleNames;

    // Use this for initialization
    void Start () {
        _moduleId = _moduleIdCounter++;
        module.OnActivate += Activate;
    }

    void Activate() {
        _lightsOn = true;
        Init();
    }

    private void Awake()
    {
        for (int i = 0; i < 47; i++)
        {
            var j = i;
            buttons[i].OnInteract += delegate {
                handleKeyPress(j);
                return false;
            };
        }
        submit.OnInteract += delegate {
            handleSubmit();
            return false;
        };
        clear.OnInteract += delegate {
            handleClear();
            return false;
        };
    }

    void Init() {
        SetupDict();
        int moduleCode = Random.Range(0, moduleNames.Length);
        answer = moduleNames[moduleCode];
        string anagram = modules[answer];
        ana.text = anagram.ToUpper();
        ans.text = "";
        foreach (GameObject but in buttonObjects) {
            but.SetActive(false);
        }

        foreach (char letter in answer.ToUpper().ToCharArray()) {
            buttonObjects[keys[letter]].SetActive(true);
        }
        Debug.LogFormat("[Insanagrams #{0}] Anagram: '{1}', Answer: '{2}'", _moduleId, anagram, answer);
    }

    void handleKeyPress(int button) {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, buttons[button].transform);
        if (!_lightsOn || _isSolved) return;
        switch (button) {
            case 0://A
                ans.text += "A";
                break;
            case 1://B
                ans.text += "B";
                break;
            case 2://C
                ans.text += "C";
                break;
            case 3://D
                ans.text += "D";
                break;
            case 4://E
                ans.text += "E";
                break;
            case 5://F
                ans.text += "F";
                break;
            case 6://G
                ans.text += "G";
                break;
            case 7://H
                ans.text += "H";
                break;
            case 8://I
                ans.text += "I";
                break;
            case 9://J
                ans.text += "J";
                break;
            case 10://K
                ans.text += "K";
                break;
            case 11://L
                ans.text += "L";
                break;
            case 12://M
                ans.text += "M";
                break;
            case 13://N
                ans.text += "N";
                break;
            case 14://O
                ans.text += "O";
                break;
            case 15://P
                ans.text += "P";
                break;
            case 16://Q
                ans.text += "Q";
                break;
            case 17://R
                ans.text += "R";
                break;
            case 18://S
                ans.text += "S";
                break;
            case 19://T
                ans.text += "T";
                break;
            case 20://U
                ans.text += "U";
                break;
            case 21://V
                ans.text += "V";
                break;
            case 22://W
                ans.text += "W";
                break;
            case 23://X
                ans.text += "X";
                break;
            case 24://Y
                ans.text += "Y";
				break;
            case 25://Z
                ans.text += "Z";
				break;
            case 26://1
                ans.text += "1";
				break;
            case 27://2
                ans.text += "2";
				break;
            case 28://3
                ans.text += "3";
				break;
            case 29://4
                ans.text += "4";
				break;
            case 30://5
                ans.text += "5";
				break;
            case 31://6
                ans.text += "6";
				break;
            case 32://7
                ans.text += "7";
				break;
            case 33://8
                ans.text += "8";
				break;
            case 34://9
                ans.text += "9";
				break;
            case 35://0
                ans.text += "0";
				break;
            case 36://-
                ans.text += "-";
				break;
            case 37://[
                ans.text += "[";
				break;
            case 38://]
                ans.text += "]";
				break;
            case 39://'
                ans.text += "'";
				break;
            case 40://.
                ans.text += ".";
				break;
            case 41://!
                ans.text += "!";
				break;
            case 42://?
                ans.text += "?";
				break;
            case 43://&
                ans.text += "&";
				break;
            case 44://^
                ans.text += "^";
				break;
            case 45://SPACE
                ans.text += " ";
				break;
            case 46://,
                ans.text += ",";
				break;

        }
    }

    void handleSubmit() {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        if (!_lightsOn || _isSolved) return;

        if (ans.text.ToUpper().Equals(answer.ToUpper())){
            module.HandlePass();
            _isSolved = true;
            newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, submit.transform);
            Debug.LogFormat("[Insanagrams #{0}] Solved!", _moduleId);
        } else {
            module.HandleStrike();
            Debug.LogFormat("[Insanagrams #{0}] Strike! Inputted: '{1}'. If you feel like this is an error, contact TasThing#5896 on Discord with a copy of this log file.", _moduleId, ans.text);
            ans.text = "";
        }
    }

    void handleClear() {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        if (!_lightsOn || _isSolved) return;
        Debug.LogFormat("[Insanagrams #{0}] Clear pressed. Text cleared: '{1}'.", _moduleId, ans.text);
        ans.text = "";
    }

    void SetupDict() {

        keys.Add('A',0);
        keys.Add('B',1);
        keys.Add('C',2);
        keys.Add('D',3);
        keys.Add('E',4);
        keys.Add('F',5);
        keys.Add('G',6);
        keys.Add('H',7);
        keys.Add('I',8);
        keys.Add('J',9);
        keys.Add('K',10);
        keys.Add('L',11);
        keys.Add('M',12);
        keys.Add('N',13);
        keys.Add('O',14);
        keys.Add('P',15);
        keys.Add('Q',16);
        keys.Add('R',17);
        keys.Add('S',18);
        keys.Add('T',19);
        keys.Add('U',20);
        keys.Add('V',21);
        keys.Add('W',22);
        keys.Add('X',23);
        keys.Add('Y',24);
        keys.Add('Z',25);
        keys.Add('1',26);
        keys.Add('2',27);
        keys.Add('3',28);
        keys.Add('4',29);
        keys.Add('5',30);
        keys.Add('6',31);
        keys.Add('7',32);
        keys.Add('8',33);
        keys.Add('9',34);
        keys.Add('0',35);
        keys.Add('-',36);
        keys.Add('[',37);
        keys.Add(']',38);
        keys.Add('\'',39);
        keys.Add('.',40);
        keys.Add('!',41);
        keys.Add('?',42);
        keys.Add('&',43);
        keys.Add('^',44);
        keys.Add(' ',45);
        keys.Add(',',46);


        moduleNames = new String[] { "101 Dalmatians", "3D Maze", "3D Tunnels", "Accumulation", "Adjacent Letters", "Adventure Game", "Air Traffic Controller", "Alchemy", "Algebra", "Alphabetical Order", "Alphabet Numbers", "Alphabet", "Anagrams", "Answering Questions", "Astrology", "Backgrounds", "Bartending", "Bases", "Battleship", "Benedict Cumberbatch", "Big Circle", "Binary LEDs", "Binary Puzzle", "Binary Tree", "Bitmaps", "Bitwise Operations", "Black Hole", "Blackjack", "Blind Alley", "Blind Maze", "Blockbusters", "Blue Cipher", "Boggle", "Boolean Maze", "Boolean Venn Diagram", "Braille", "British Slang", "Broken Buttons", "Broken Guitar Chords", "Burglar Alarm", "Button Masher", "Button Sequence", "Caesar Cipher", "Calendar", "Capacitor Discharge", "Catchphrase", "Challenge & Contact", "Character Shift", "Chinese Counting", "Cheap Checkout", "Chess", "Chord Qualities", "Christmas Presents", "Coffeebucks", "Color Addition", "Color Decoding", "Colored Squares", "Colored Switches", "Color Flash", "Colorful Insanity", "Colorful Madness", "Color Generator", "Color Match", "Color Math", "Color Morse", "Combination Lock", "Command Prompt", "Complex Keypad", "Complicated Buttons", "Complicated Wires", "Connection Check", "Connection Device", "Cookie Jars", "Cooking", "Coordinates", "Countdown", "Crackbox", "Crazy Talk", "Creation", "Cruel Countdown", "Cruel Piano Keys", "Cryptography", "Curriculum", "Cursed Double-Oh", "Decolored Squares", "Determinants", "DetoNATO", "Digital Cipher", "Digital Root", "Discolored Squares", "Divided Squares", "Dominoes", "Double-Oh", "Double Color", "Dr. Doctor", "Dragon Energy", "Edgework", "Elder Futhark", "Emoji Math", "Encrypted Morse", "Encryption Bingo", "English Test", "Equations", "Error Codes", "European Travel", "Extended Password", "Factoring", "Factory Maze", "Fast Math", "Faulty Backgrounds", "Festive Piano Keys", "Filibuster", "FizzBuzz", "Flags", "Flashing Lights", "Flavor Text EX", "Flavor Text", "Flip The Coin", "Follow the Leader", "Font Select", "Foreign Exchange Rates", "Forget Everything", "Forget Me Not", "Forget This", "Free Parking", "Friendship", "Functions", "Gadgetron Vendor", "Game of Life Cruel", "Game of Life Simple", "Geometry Dash", "Genetic Sequence", "Graffiti Numbers", "Greek Calculus", "Green Cipher", "Gridlock", "Grid Matching", "Guitar Chords", "Harmony Sequence", "Hexamaze", "Hex To Decimal", "Hieroglyphics", "Hogwarts", "Homophones", "Horrible Memory", "Hot Potato", "HTTP Response", "Human Resources", "Hunting", "Ice Cream", "Identity Parade", "IKEA", "Indigo Cipher", "Insanagrams", "Instructions", "Keypad", "Knob", "Know Your Way", "Krazy Talk", "Kudosudoku", "Lasers", "Laundry", "LED Encryption", "LED Grid", "LED Math", "Left And Right", "LEGO", "Letter Keys", "Light Cycle", "Lights Out", "Lightspeed", "Lion's Share", "Listening", "Logical Buttons", "Logic Gates", "Logic", "Mad Memory", "Mafia", "Mahjong", "Maintenance", "Manometers", "Marble Tumble", "Maritime Flags", "Mashematics", "Mastermind Cruel", "Mastermind Simple", "Math", "Maze Scrambler", "Maze", "Melody Sequencer", "Memory", "Micro-Modules", "Microcontroller", "Mineseeker", "Minesweeper", "Modern Cipher", "Module Homework", "Module Maze", "Modules Against Humanity", "Modulo", "Monsplode, Fight!", "Monsplode Trading Cards", "Morse-A-Maze", "Morse Code", "Morse Identification", "Morsematics", "Morse War", "Mortal Kombat", "Motion Sense", "Mouse In The Maze", "Murder", "Mystic Square", "Needy Mrs Bob", "Neutralization", "Nonogram", "Number Nimbleness", "Number Pad", "Numbers", "Only Connect", "Orientation Cube", "Painting", "Party Time", "Passport Control", "Password", "Pattern Cube", "Pay Respects", "Periodic Table", "Perplexing Wires", "Perspective Pegs", "Piano Keys", "Pie", "Pigpen Rotations", "Playfair Cipher", "Plumbing", "Poetry", "Point of Order", "Poker", "Polyhedral Maze", "Press X", "Probing", "QR Code", "Question Mark", "Quintuples", "Radiator", "Random Number Generator", "Rapid Buttons", "Refill that Beer!", "Regular Crazy Talk", "Resistors", "Retirement", "Reverse Morse", "Rhythms", "Rock-Paper-Scissors-Lizard-Spock", "Rotary Phone", "Round Keypad", "Rubik's Clock", "Rubik's Cube", "S.E.T.", "Safety Safe", "Schlag den Bomb", "Scripting", "Sea Shells", "Semaphore", "Shape Memory", "Shapes And Bombs", "Shape Shift", "Shikaku", "Signals", "Silly Slots", "Simon's Sequence", "Simon's Stages", "Simon's Star", "Simon Samples", "Simon Says", "Simon Scrambles", "Simon Screams", "Simon Sends", "Simon Shrieks", "Simon Sings", "Simon Sounds", "Simon Speaks", "Simon Spins", "Simon Squawks", "Simon States", "Sink", "Skewed Slots", "Skinny Wires", "Skyrim", "Snooker", "Sonic & Knuckles", "Sonic the Hedgehog", "Souvenir", "Spinning Buttons", "Splitting The Loot", "Square Button", "Street Fighter", "Subways", "Sueet Wall", "Superlogic", "Switches", "Symbol Cycle", "Symbolic Coordinates", "Symbolic Password", "SYNC-125 [3]", "Synchronization", "Synonyms", "T-Words", "Tangrams", "Tap Code", "Tasha Squeals", "Tax Returns", "Ten-Button Color Code", "Tennis", "Ternary Converter", "Tetris", "Text Field", "The Bulb", "The Button", "The Clock", "The Code", "The Crystal Maze", "The Cube", "The Digit", "The Festive Jukebox", "The Fidget Spinner", "The Gamepad", "The Hangover", "The Hexabutton", "The iPhone", "The Jack-O'-Lantern", "The Jewel Vault", "The Jukebox", "The Labyrinth", "The London Underground", "The Moon", "The Number Cipher", "The Number", "The Plunger Button", "The Plunger", "The Radio", "The Screw", "The Sphere", "The Stock Market", "The Stopwatch", "The Sun", "The Swan", "The Switch", "The Time Keeper", "The Triangle", "The Wire", "Third Base", "Tic-Tac-Toe", "Timezone", "Tower of Hanoi", "Turn The Keys", "Turn The Key", "Turtle Robot", "Two Bits", "Ultrastores", "Uncolored Squares", "Unfair Cipher", "Unrelated Anagrams", "USA Maze", "Valves", "Varicolored Squares", "Venting Gas", "Violet Cipher", "Visual Impairment", "Waste Management", "Web Design", "Westeros", "Who's on First", "Who's That Monsplode", "Wingdings", "Wire Placement", "Wire Sequence", "Wire Spaghetti", "Wires", "Word Scramble", "Word Search", "X-Ray", "X01", "Yahtzee", "Zoni", "Zoo", "Subscribe to Pewdiepie", "Grocery Store", "Draw", "Burger Alarm", "Purgatory", "Mega Man 2", "Lombax Cubes", "The Stare", "Graphic Memory", "Quiz Buzz", "Wavetapping", "The Hypercube", "Speak English", "Stack'em", "Seven Wires", "Colored Keys", "The Troll", "Planets", "The Necronomicon", "Four-Card Monte", "Aa", "The Witness", "The Giant's Drink", "Digit String", "Alpha", "Snap!", "Hidden Colors", "Colour Code", "Vexillology", "Brush Strokes", "Odd One Out", "The Triangle Button", "Mazematics", "Equations X", "Maze^3", "Yellow Cipher", "Orange Cipher", "Red Cipher", "Faulty RGB Maze", "Forget Me Later", "Garfield Kart", "Footnotes", "Bamboozling Button", "Fruits", "The Modkit", "Tetravex", "Flower Patch", "Matchematics", "Caesar Cycle", "Playfair Cycle", "Cryptic Cycle", "Ultimate Cycle", "Bamboozling Button Grid", "Old Fogey", "The Ultracube" };

        modules.Add("101 Dalmatians", "101 Natal Maids");
        modules.Add("3D Maze", "Am Zed 3");
        modules.Add("3D Tunnels", "Lend 3 Nuts");
        modules.Add("Accumulation", "I Am Uncut Cola");
        modules.Add("Adjacent Letters", "Calendar Jet Test");
        modules.Add("Adventure Game", "Meet Guava Nerd");
        modules.Add("Air Traffic Controller", "Antarctic Floor Rifler");
        modules.Add("Alchemy", "Hey Clam");
        modules.Add("Algebra", "Lab Gear");
        modules.Add("Alphabetical Order", "A Birthplace Reload");
        modules.Add("Alphabet Numbers", "A Bat Burns Me Help");
        modules.Add("Alphabet", "Bath Leap");
        modules.Add("Anagrams", "A Mr Sagan");
        modules.Add("Answering Questions", "Earn Wings Noses Quit");
        modules.Add("Astrology", "Royals Tog");
        modules.Add("Backgrounds", "Dragon Bucks");
        modules.Add("Bartending", "Intend Brag");
        modules.Add("Bases", "Sea BS");
        modules.Add("Battleship", "Hip Tablets");
        modules.Add("Benedict Cumberbatch", "Butch Em Bent Iced Crab");
        modules.Add("Big Circle", "Big Cleric");
        modules.Add("Binary LEDs", "Badly Risen");
        modules.Add("Binary Puzzle", "Early Buzz Pin");
        modules.Add("Binary Tree", "Bye Terrain");
        modules.Add("Bitmaps", "Ms Pi Bat");
        modules.Add("Bitwise Operations", "Is It Web Arose Point");
        modules.Add("Black Hole", "Be Lo Chalk");
        modules.Add("Blackjack", "Cab JK Lack");
        modules.Add("Blind Alley", "A Lend By Ill");
        modules.Add("Blind Maze", "Me Land Biz");
        modules.Add("Blockbusters", "Rubble Stocks");
        modules.Add("Blue Cipher", "Burp Lichee");
        modules.Add("Boggle", "Log Beg");
        modules.Add("Boolean Maze", "Enable A Zoom");
        modules.Add("Boolean Venn Diagram", "Drag Aim Ban Novel One");
        modules.Add("Braille", "Ill Bear");
        modules.Add("British Slang", "Snails Bright");
        modules.Add("Broken Buttons", "Stubborn Token");
        modules.Add("Broken Guitar Chords", "A Corgi Dub Horns Trek");
        modules.Add("Burglar Alarm", "Rural Lamb Rag");
        modules.Add("Button Masher", "Share Tomb Nut");
        modules.Add("Button Sequence", "On Ten Cube Quest");
        modules.Add("Caesar Cipher", "Crisp Ear Ache");
        modules.Add("Calendar", "Red Canal");
        modules.Add("Capacitor Discharge", "It Graced His Orca Cap");
        modules.Add("Catchphrase", "Cash Chapter");
        modules.Add("Challenge & Contact", "Agent Cloth & Cancel");
        modules.Add("Character Shift", "Scratch Hat Fire");
        modules.Add("Chinese Counting", "Eugenic Notch Sin");
        modules.Add("Cheap Checkout", "Oh Cupcake Tech");
        modules.Add("Chess", "SS Ech");
        modules.Add("Chord Qualities", "Quad Ostrich Lie");
        modules.Add("Christmas Presents", "Pens Rest Charm Sits");
        modules.Add("Coffeebucks", "Beck Cue Offs");
        modules.Add("Color Addition", "Cation Lid Door");
        modules.Add("Color Decoding", "Diced Corn Logo");
        modules.Add("Colored Squares", "Dress Or Coequal");
        modules.Add("Colored Switches", "Witches Close Rod");
        modules.Add("Color Flash", "Call For Hos");
        modules.Add("Colorful Insanity", "Run So Fictionally");
        modules.Add("Colorful Madness", "Mad Focus Enrolls");
        modules.Add("Color Generator", "Lone Rotor Grace");
        modules.Add("Color Match", "Halt Mr Coco");
        modules.Add("Color Math", "Roam Cloth");
        modules.Add("Color Morse", "Mr Sore Cool");
        modules.Add("Combination Lock", "Black Coin Motion");
        modules.Add("Command Prompt", "Mr Mod Pant Comp");
        modules.Add("Complex Keypad", "My Explode Pack");
        modules.Add("Complicated Buttons", "Placed Bottom Tunics");
        modules.Add("Complicated Wires", "Rides Camel Tip Cow");
        modules.Add("Connection Check", "Chick Con One Cent");
        modules.Add("Connection Device", "Once Vented Iconic");
        modules.Add("Cookie Jars", "Is Orca Joke");
        modules.Add("Cooking", "Nick Goo");
        modules.Add("Coordinates", "Second Ratio");
        modules.Add("Countdown", "On Duct Now");
        modules.Add("Crackbox", "Cork Cab X");
        modules.Add("Crazy Talk", "Lazy Track");
        modules.Add("Creation", "Rate Icon");
        modules.Add("Cruel Countdown", "Run Coconut Weld");
        modules.Add("Cruel Piano Keys", "Recoup Sky Alien");
        modules.Add("Cryptography", "Try Choppy Rag");
        modules.Add("Curriculum", "I Curl Mu Cur");
        modules.Add("Cursed Double-Oh", "Should Cube-Redo");
        modules.Add("Decolored Squares", "A Corroded Sequels");
        modules.Add("Determinants", "Tanned Mister");
        modules.Add("DetoNATO", "To Donate");
        modules.Add("Digital Cipher", "Pi Acid Lighter");
        modules.Add("Digital Root", "Dog Tail Riot");
        modules.Add("Discolored Squares", "Careless Squid Door");
        modules.Add("Divided Squares", "Revise Dad Squid");
        modules.Add("Dominoes", "Dime Soon");
        modules.Add("Double-Oh", "Blood-hue");
        modules.Add("Double Color", "Cool Boulder");
        modules.Add("Dr. Doctor", "Cord. Trod");
        modules.Add("Dragon Energy", "Grand Grey One");
        modules.Add("Edgework", "Word Geek");
        modules.Add("Elder Futhark", "Father Lurked");
        modules.Add("Emoji Math", "He Omit Jam");
        modules.Add("Encrypted Morse", "Ms Creed Entropy");
        modules.Add("Encryption Bingo", "Benign Cry Option");
        modules.Add("English Test", "Tense Lights");
        modules.Add("Equations", "In Quote As");
        modules.Add("Error Codes", "Score Order");
        modules.Add("European Travel", "A Nepal Overture");
        modules.Add("Extended Password", "Next Spades Worded");
        modules.Add("Factoring", "Acorn Gift");
        modules.Add("Factory Maze", "To Crazy Fame");
        modules.Add("Fast Math", "That Ms Fa");
        modules.Add("Faulty Backgrounds", "Arguably Duck Fonts");
        modules.Add("Festive Piano Keys", "Pokeys Vine Fiesta");
        modules.Add("Filibuster", "Built Fires");
        modules.Add("FizzBuzz", "Bizz Fuzz");
        modules.Add("Flags", "SF Lag");
        modules.Add("Flashing Lights", "Gall Fish Things");
        modules.Add("Flavor Text EX", "To Flax Vertex");
        modules.Add("Flavor Text", "Flat Vortex");
        modules.Add("Flip The Coin", "Fiction Help");
        modules.Add("Follow the Leader", "Defeat Whole Roll");
        modules.Add("Font Select", "Elf Net Cost");
        modules.Add("Foreign Exchange Rates", "A Teachers Genre Foxing");
        modules.Add("Forget Everything", "Very Tethering Fog");
        modules.Add("Forget Me Not", "Ten Foot Germ");
        modules.Add("Forget This", "To Fighters");
        modules.Add("Free Parking", "Fire Rank Peg");
        modules.Add("Friendship", "Shred If Pin");
        modules.Add("Functions", "Unfit Cons");
        modules.Add("Gadgetron Vendor", "Not Graded Govern");
        modules.Add("Game of Life Cruel", "Cameo Figure Fell");
        modules.Add("Game of Life Simple", "Female Implies Fog");
        modules.Add("Geometry Dash", "Gated Rye Ohms");
        modules.Add("Genetic Sequence", "Get Science Queen");
        modules.Add("Graffiti Numbers", "Rift Beam Surfing");
        modules.Add("Greek Calculus", "Glue Us Crackle");
        modules.Add("Green Cipher", "He Rep Cringe");
        modules.Add("Gridlock", "Gold Rick");
        modules.Add("Grid Matching", "Mind Chart Gig");
        modules.Add("Guitar Chords", "Guard Ostrich");
        modules.Add("Harmony Sequence", "Shy Queen Romance");
        modules.Add("Hexamaze", "Haze Exam");
        modules.Add("Hex To Decimal", "Aced Hotel Mix");
        modules.Add("Hieroglyphics", "Holy Pig Riches");
        modules.Add("Hogwarts", "Raw Goths");
        modules.Add("Homophones", "No Home Hops");
        modules.Add("Horrible Memory", "Me Blimey Horror");
        modules.Add("Hot Potato", "Tattoo Hop");
        modules.Add("HTTP Response", "Hen Strep Stop");
        modules.Add("Human Resources", "Ace Nurses Humor");
        modules.Add("Hunting", "Hint Gun");
        modules.Add("Ice Cream", "Mice Race");
        modules.Add("Identity Parade", "Painted Dietary");
        modules.Add("IKEA", "I Eak");
        modules.Add("Indigo Cipher", "Ionic Herd Pig");
        modules.Add("Insanagrams", "Assign An Arm");
        modules.Add("Instructions", "Us Or Instinct");
        modules.Add("Keypad", "Pa Dyke");
        modules.Add("Knob", "Bonk");
        modules.Add("Know Your Way", "Yuk Yo Own Raw");
        modules.Add("Krazy Talk", "Talky Ark Z");
        modules.Add("Kudosudoku", "Duo Kou Dusk");
        modules.Add("Lasers", "Re Lass");
        modules.Add("Laundry", "Run Lady");
        modules.Add("LED Encryption", "Lyric Deponent");
        modules.Add("LED Grid", "Girdled");
        modules.Add("LED Math", "Mat Held");
        modules.Add("Left And Right", "Darling Theft");
        modules.Add("LEGO", "El Go");
        modules.Add("Letter Keys", "Elk Tyre Set");
        modules.Add("Light Cycle", "Eight LLC YC");
        modules.Add("Lights Out", "Tight Soul");
        modules.Add("Lightspeed", "Hedge Split");
        modules.Add("Lion's Share", "Hon's Serial");
        modules.Add("Listening", "In Tin Legs");
        modules.Add("Logical Buttons", "Bacon Guilt Lost");
        modules.Add("Logic Gates", "Locates Gig");
        modules.Add("Logic", "I Clog");
        modules.Add("Mad Memory", "Dear Mommy");
        modules.Add("Mafia", "I Am Fa");
        modules.Add("Mahjong", "Mag John");
        modules.Add("Maintenance", "Mice Antenna");
        modules.Add("Manometers", "Rename Most");
        modules.Add("Marble Tumble", "Met Lemur Blab");
        modules.Add("Maritime Flags", "Legit Mrs Mafia");
        modules.Add("Mashematics", "Sea Mismatch");
        modules.Add("Mastermind Cruel", "Unclear Midterms");
        modules.Add("Mastermind Simple", "Smeared Mint Limps");
        modules.Add("Math", "Hat M");
        modules.Add("Maze Scrambler", "Blame Mrs Craze");
        modules.Add("Maze", "M Eaz");
        modules.Add("Melody Sequencer", "Elm Encodes Query");
        modules.Add("Memory", "Me Or My");
        modules.Add("Micro-Modules", "Room-Dice Slum");
        modules.Add("Microcontroller", "Correct Ill Moron");
        modules.Add("Mineseeker", "Me Rise Keen");
        modules.Add("Minesweeper", "Newer Pi Seem");
        modules.Add("Modern Cipher", "Perched Mr Ion");
        modules.Add("Module Homework", "Lowered Hook Mum");
        modules.Add("Module Maze", "Mum Ole Daze");
        modules.Add("Modules Against Humanity", "Yum Dalmatians Toes In Ugh");
        modules.Add("Modulo", "Om Loud");
        modules.Add("Monsplode, Fight!", "Golfed Months, Pi!");
        modules.Add("Monsplode Trading Cards", "Landscaped Dorm Sorting");
        modules.Add("Morse-A-Maze", "Mrs A-Zee-Roam");
        modules.Add("Morse Code", "Do Some Rec");
        modules.Add("Morse Identification", "A Moist Iron Deficient");
        modules.Add("Morsematics", "Scariest Mom");
        modules.Add("Morse War", "Ear Worms");
        modules.Add("Mortal Kombat", "Mr Balk Tomato");
        modules.Add("Motion Sense", "No Semitones");
        modules.Add("Mouse In The Maze", "Seem To Humanize");
        modules.Add("Murder", "Mr Rude");
        modules.Add("Mystic Square", "Yes Squirm Cat");
        modules.Add("Needy Mrs Bob", "Ye Bomb Nerds");
        modules.Add("Neutralization", "Outran Alien Zit");
        modules.Add("Nonogram", "Moron Nag");
        modules.Add("Number Nimbleness", "Nine Emblems Burns");
        modules.Add("Number Pad", "Damper Bun");
        modules.Add("Numbers", "Burns Me");
        modules.Add("Only Connect", "Cent Con Only");
        modules.Add("Orientation Cube", "Iceboat Neutrino");
        modules.Add("Painting", "Giant Pin");
        modules.Add("Party Time", "I Am Pretty");
        modules.Add("Passport Control", "Clan Troop Sports");
        modules.Add("Password", "Saw Drops");
        modules.Add("Pattern Cube", "Pecan Butter");
        modules.Add("Pay Respects", "Pease Crypts");
        modules.Add("Periodic Table", "Tidier Placebo");
        modules.Add("Perplexing Wires", "Griper Pixel News");
        modules.Add("Perspective Pegs", "I Respect Vegs Pep");
        modules.Add("Piano Keys", "Noisy Peak");
        modules.Add("Pie", "I PE");
        modules.Add("Pigpen Rotations", "Orientating Pops");
        modules.Add("Playfair Cipher", "Reply Hip Africa");
        modules.Add("Plumbing", "Blimp Gun");
        modules.Add("Poetry", "Pry Toe");
        modules.Add("Point of Order", "Portioned For");
        modules.Add("Poker", "Rope K");
        modules.Add("Polyhedral Maze", "Amazed Hype Roll");
        modules.Add("Press X", "SS Prex");
        modules.Add("Probing", "Pig Born");
        modules.Add("QR Code", "CEO Dr Q");
        modules.Add("Question Mark", "A Squirm Token");
        modules.Add("Quintuples", "Lupin Quest");
        modules.Add("Radiator", "Radio Art");
        modules.Add("Random Number Generator", "A Bartender Moon Germ Run");
        modules.Add("Rapid Buttons", "Bad Printouts");
        modules.Add("Refill that Beer!", "Fret Hill Berate!");
        modules.Add("Regular Crazy Talk", "A Larger Lazy Truck");
        modules.Add("Resistors", "Sis Resort");
        modules.Add("Retirement", "Entire Term");
        modules.Add("Reverse Morse", "Me Server Rose");
        modules.Add("Rhythms", "Mrs Hyth");
        modules.Add("Rock-Paper-Scissors-Lizard-Spock", "Socks-Carload-Crops-Zipper-Risks");
        modules.Add("Rotary Phone", "Rare Typhoon");
        modules.Add("Round Keypad", "Yup Dank Redo");
        modules.Add("Rubik's Clock", "Irk Cub's Lock");
        modules.Add("Rubik's Cube", "Rubie's Buck");
        modules.Add("S.E.T.", "E.S.T.");
        modules.Add("Safety Safe", "Eases Taffy");
        modules.Add("Schlag den Bomb", "Belch Bangs Mod");
        modules.Add("Scripting", "Pic String");
        modules.Add("Sea Shells", "Eels Slash");
        modules.Add("Semaphore", "Hear Poems");
        modules.Add("Shape Memory", "My Semaphore");
        modules.Add("Shapes And Bombs", "Mesh Bobs Pandas");
        modules.Add("Shape Shift", "The Spa Fish");
        modules.Add("Shikaku", "Us Khaki");
        modules.Add("Signals", "Is Slang");
        modules.Add("Silly Slots", "Still Lossy");
        modules.Add("Simon's Sequence", "Ms Queen's Conies");
        modules.Add("Simon's Stages", "Tango's Misses");
        modules.Add("Simon's Star", "Smart's Ions");
        modules.Add("Simon Samples", "Misnames Slop");
        modules.Add("Simon Says", "Noisy Mass");
        modules.Add("Simon Scrambles", "Blames Crimsons");
        modules.Add("Simon Screams", "Miss Romances");
        modules.Add("Simon Sends", "Noses Minds");
        modules.Add("Simon Shrieks", "Skirmish Ones");
        modules.Add("Simon Sings", "Missing Son");
        modules.Add("Simon Sounds", "Mid Suns Soon");
        modules.Add("Simon Speaks", "Eskimo Snaps");
        modules.Add("Simon Spins", "Miss No Snip");
        modules.Add("Simon Squawks", "Ms Quasi Knows");
        modules.Add("Simon States", "Season Mitts");
        modules.Add("Sink", "Inks");
        modules.Add("Skewed Slots", "Slowest Desk");
        modules.Add("Skinny Wires", "Sky Is Winner");
        modules.Add("Skyrim", "My Risk");
        modules.Add("Snooker", "Rooks En");
        modules.Add("Sonic & Knuckles", "Conk & Luckiness");
        modules.Add("Sonic the Hedgehog", "Eighth Echoed Song");
        modules.Add("Souvenir", "One Virus");
        modules.Add("Spinning Buttons", "Tin Snot Snub Ping");
        modules.Add("Splitting The Loot", "Telling Tooth Spit");
        modules.Add("Square Button", "Rants Bouquet");
        modules.Add("Street Fighter", "Register Theft");
        modules.Add("Subways", "Buy Saws");
        modules.Add("Sueet Wall", "Wallet Use");
        modules.Add("Superlogic", "Police Rugs");
        modules.Add("Switches", "Chew Sits");
        modules.Add("Symbol Cycle", "My Cos By Cell");
        modules.Add("Symbolic Coordinates", "Cabs Consider Limo Toy");
        modules.Add("Symbolic Password", "Bypass Limo Crowds");
        modules.Add("SYNC-125 [3]", "NYCs-123 [5]");
        modules.Add("Synchronization", "Crazy Thin Onions");
        modules.Add("Synonyms", "My SS Nony");
        modules.Add("T-Words", "Sword-T");
        modules.Add("Tangrams", "Grant Sam");
        modules.Add("Tap Code", "Cape Dot");
        modules.Add("Tasha Squeals", "He Salsa Squat");
        modules.Add("Tax Returns", "Ranters Tux");
        modules.Add("Ten-Button Color Code", "Outdo Nettle-Corncob");
        modules.Add("Tennis", "Nest In");
        modules.Add("Ternary Converter", "Contrary Reverent");
        modules.Add("Tetris", "Sitter");
        modules.Add("Text Field", "Fed Ex Tilt");
        modules.Add("The Bulb", "Belt Hub");
        modules.Add("The Button", "Hot Tub Ten");
        modules.Add("The Clock", "Etch Lock");
        modules.Add("The Code", "Doc Thee");
        modules.Add("The Crystal Maze", "Zesty Cream Halt");
        modules.Add("The Cube", "He Be Cut");
        modules.Add("The Digit", "It Get Hid");
        modules.Add("The Festive Jukebox", "Bee Jive Text Of Husk");
        modules.Add("The Fidget Spinner", "Deepening Thrifts");
        modules.Add("The Gamepad", "Aged Pet Ham");
        modules.Add("The Hangover", "Her Hot Vegan");
        modules.Add("The Hexabutton", "Beneath Hot Tux");
        modules.Add("The iPhone", "Oh Hip Teen");
        modules.Add("The Jack-O'-Lantern", "Can't Not-Heal-Jerk");
        modules.Add("The Jewel Vault", "We Jut Have Tell");
        modules.Add("The Jukebox", "But Hex Joke");
        modules.Add("The Labyrinth", "Try Health Bin");
        modules.Add("The London Underground", "Goldenrod Thunder Noun");
        modules.Add("The Moon", "One Moth");
        modules.Add("The Number Cipher", "Breech Hermit Pun");
        modules.Add("The Number", "Mr Hen Tube");
        modules.Add("The Plunger Button", "Tenth Upturn Globe");
        modules.Add("The Plunger", "Nether Gulp");
        modules.Add("The Radio", "Head Riot");
        modules.Add("The Screw", "Chew Rest");
        modules.Add("The Sphere", "Eh Her Step");
        modules.Add("The Stock Market", "Hack Treks Totem");
        modules.Add("The Stopwatch", "Two Step Hatch");
        modules.Add("The Sun", "Uh Sent");
        modules.Add("The Swan", "New Hats");
        modules.Add("The Switch", "Which Test");
        modules.Add("The Time Keeper", "Met Tepee Hiker");
        modules.Add("The Triangle", "Hanger Title");
        modules.Add("The Wire", "Were Hit");
        modules.Add("Third Base", "Bead Shirt");
        modules.Add("Tic-Tac-Toe", "Cat-Cot-Tie");
        modules.Add("Timezone", "Monetize");
        modules.Add("Tower of Hanoi", "Antihero Woof");
        modules.Add("Turn The Keys", "Try Hut Knees");
        modules.Add("Turn The Key", "Then Turkey");
        modules.Add("Turtle Robot", "Torture Bolt");
        modules.Add("Two Bits", "Bit Stow");
        modules.Add("Ultrastores", "Laser Tutors");
        modules.Add("Uncolored Squares", "Our Sons Lacquered");
        modules.Add("Unfair Cipher", "Hi Rip Furnace");
        modules.Add("Unrelated Anagrams", "A Urgent Salamander");
        modules.Add("USA Maze", "Amaze Us");
        modules.Add("Valves", "Slave V");
        modules.Add("Varicolored Squares", "Discover Quasar Lore");
        modules.Add("Venting Gas", "Vegan Sting");
        modules.Add("Violet Cipher", "Hitler Cove Pi");
        modules.Add("Visual Impairment", "A Minus Lit Vampire");
        modules.Add("Waste Management", "New Nag Teammates");
        modules.Add("Web Design", "Bed Sewing");
        modules.Add("Westeros", "Two Seers");
        modules.Add("Who's on First", "Frown's Hoist");
        modules.Add("Who's That Monsplode", "Topmost How's Handle");
        modules.Add("Wingdings", "Ding Swing");
        modules.Add("Wire Placement", "Replace Me Twin");
        modules.Add("Wire Sequence", "We Quire Scene");
        modules.Add("Wire Spaghetti", "Pirates Weight");
        modules.Add("Wires", "Wiser");
        modules.Add("Word Scramble", "Marble Crowds");
        modules.Add("Word Search", "Crew Hoards");
        modules.Add("X-Ray", "Y-Arx");
        modules.Add("X01", "10X");
        modules.Add("Yahtzee", "Hey Zeta");
        modules.Add("Zoni", "I No Z");
        modules.Add("Zoo", "Ooz");
        modules.Add("Subscribe to Pewdiepie", "I Purities Deep Cobwebs"); //save
        modules.Add("Grocery Store", "Retry Scrooge"); //save
        modules.Add("Draw", "Ward");
        modules.Add("Burger Alarm", "Larger Umbra");
        modules.Add("Purgatory", "Yogurt Rap");
        modules.Add("Mega Man 2", "2 Name Mag");
        modules.Add("Lombax Cubes", "Able Box Scum");
        modules.Add("The Stare", "Theaters");
        modules.Add("Graphic Memory", "Impeach Mr Gyro");
        modules.Add("Quiz Buzz", "IQ Buuzzz");
        modules.Add("Wavetapping", "VIP Agent Paw");
        modules.Add("The Hypercube", "Yep Butch Here");
        modules.Add("Speak English", "Shaking Peels");
        modules.Add("Stack'em", "'Tacks Me");
        modules.Add("Seven Wires", "Reeves Wins");
        modules.Add("Colored Keys", "Yodeler Sock");
        modules.Add("The Troll", "Hell Tort");
        modules.Add("Planets", "Nest Lap");
        modules.Add("The Necronomicon", "Homer Connection");
        modules.Add("Four-Card Monte", "Unread-Comfort");
        modules.Add("Aa", "aA");
        modules.Add("The Witness", "These Twins");
        modules.Add("The Giant's Drink", "Tinker's Night Ad");
        modules.Add("Digit String", "Grid Sitting");
        modules.Add("Alpha", "Ha Pal");
        modules.Add("Snap!", "Naps!");
        modules.Add("Hidden Colors", "Scolded Rhino");
        modules.Add("Colour Code", "Louder Coco");
        modules.Add("Vexillology", "Lily Glove Ox");
        modules.Add("Brush Strokes", "Be Rush Storks");
        modules.Add("Odd One Out", "No Dude Too");
        modules.Add("The Triangle Button", "North Baguette Lint");
        modules.Add("Mazematics", "Sam Zit Acme");
        modules.Add("Equations X", "Quiet Axons");
        modules.Add("Maze^3", "Za Me^3");
        modules.Add("Yellow Cipher", "Hip Celery Owl");
        modules.Add("Orange Cipher", "Charger On Pie");
        modules.Add("Red Cipher", "Priced Her");
        modules.Add("Faulty RGB Maze", "Zebra At Fly Mug");
        modules.Add("Forget Me Later", "Telegram Forte");
        modules.Add("Garfield Kart", "Rag Dirt Flake");
        modules.Add("Footnotes", "To Soft One");
        modules.Add("Bamboozling Button", "Blab Butt Gizmo Noon");
        modules.Add("Fruits", "Surf It");
        modules.Add("The Modkit", "Kited Moth");
        modules.Add("Tetravex", "Ax Tree Tv");
        modules.Add("Flower Patch", "Chapter Wolf");
        modules.Add("Matchematics", "Schematic Mat");
        modules.Add("Organization", "A Train Oozing");
        modules.Add("Caesar Cycle", "Scarcely Ace");
        modules.Add("Playfair Cycle", "Clay Elf Piracy");
        modules.Add("Cryptic Cycle", "Cyclic Cry Pet");
        modules.Add("Ultimate Cycle", "Mutate Icy Cell");
        modules.Add("Colour Talk", "All Cork Out");
        modules.Add("Bamboozling Button Grid", "A Bubbling Ding Zoom Trot");
        modules.Add("Old Fogey", "Goofy Led");
        modules.Add("The Ultracube", "Blue Hut Crate");
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "Use '!{0} Cheap Checkout' to submit Cheap Checkout as your answer.";
#pragma warning restore 414

    protected KMSelectable[] ProcessTwitchCommand(string input) {

        KMSelectable[] ansButtons;
        if (input.Length != 0) {
            ansButtons = new KMSelectable[input.Trim().Length + 1];
            for (int i = 0; i < input.Trim().Length; i++) {
                if (!keys.ContainsKey(input.ToUpper()[i])) {
                    return null;
                }
                ansButtons[i] = buttons[keys[input.Trim().ToUpper()[i]]];
            }

            ansButtons[input.Trim().Length] = submit;
            return ansButtons;
        }

        return null;
    }

}
