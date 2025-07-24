using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class boozleTalkScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable Selectable;
    public GameObject SpritesObj;
    public SpriteRenderer[] SpriteSlots;
    public Sprite[] Sprites;

    string[] wordlists = {
        "EIGHT,ELEVEN,FIVE,FOUR,NINE,ONE,SEVEN,SIX,TEN,THIRTEEN,THREE,TWELVE,TWENTY,TWO,ZERO",
        "AZURE,BLACK,BLUE,BROWN,CYAN,GRAY,GREEN,LIME,MAGENTA,PINK,PURPLE,RED,VIOLET,WHITE,YELLOW",
        "CIRCLE,CUBE,DIAMOND,ELLIPSE,HEART,HEXAGON,KITE,OCTAGON,PENTAGON,PRISM,PYRAMID,RHOMBUS,SPHERE,SQUARE,STAR",
        "BRAZIL,CANADA,CHINA,EGYPT,FRANCE,GERMANY,INDIA,ITALY,JAPAN,MEXICO,POLAND,RUSSIA,SPAIN,THAILAND,VIETNAM",
        "ARSENIC,BORON,CARBON,COBALT,HELIUM,IRON,LEAD,NEON,NICKEL,NITROGEN,OXYGEN,SILICON,SODIUM,SULFUR,ZINC",
        "BEAVER,BIRD,CAT,DOG,FERRET,FOX,GIRAFFE,HORSE,KOALA,LION,LLAMA,MONKEY,NARWHAL,PANDA,RACCOON",
        "BACON,BREAD,BURRITO,CAKE,CHEESE,COOKIE,CORN,KEBAB,MANGO,PASTA,PIE,PIZZA,RICE,SAUSAGE,TACO",
        "ANGER,AWE,DISGUST,FEAR,GRIEF,GUILTY,JOY,LOATHING,LOVE,PENSIVE,RAGE,REMORSE,SADNESS,SURPRISE,TERROR",
        "ARCHERY,BASEBALL,BOWLING,CURLING,CYCLING,DARTS,FOOTBALL,FRISBEE,GOLF,HOCKEY,LACROSSE,RUGBY,SNOOKER,SOCCER,TENNIS",
        "BANJO,CELLO,CLARINET,COWBELL,CYMBAL,DRUM,FLUTE,GUITAR,OBOE,OCARINA,PIANO,RECORDER,TRUMPET,UKULELE,VIOLIN"
    };
    string[] categories = { "a number", "a color", "a shape", "a country", "an element", "an animal", "a food", "an emotion", "a sport", "an instrument" };
    int digit;
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        Selectable.OnInteract += delegate () { Press(); return false; };
    }

    // Use this for initialization
    void Start()
    {
        digit = Rnd.Range(0, 10);
        string word = wordlists[digit].Split(',').PickRandom();
        Debug.LogFormat("[BoozleTalk #{0}] Your word is {1}, which is {2}. It's corresponding digit is {3}.", moduleId, word, categories[digit], digit);

        string wPadding = "*";
        for (int h = 0; h < (8 - word.Length) / 2; h++)
        {
            wPadding = " " + wPadding + " ";
        }
        if (word.Length % 2 == 0)
        {
            SpritesObj.transform.localPosition = new Vector3(-0.0095f, 0f, 0f);
        }
        else
        {
            wPadding += " ";
        }
        wPadding = wPadding.Replace("*", word);

        for (int q = 0; q < 8; q++) {
            SpriteSlots[q].sprite = wPadding[q] == ' ' ? null : Sprites[alphabet.IndexOf(wPadding[q])];
        }
    }

    void Press() {
        if (moduleSolved) { return; }
        if (Bomb.GetFormattedTime().Contains(digit.ToString()))
        {
            Module.HandlePass();
            moduleSolved = true;
            Debug.LogFormat("[BoozleTalk #{0}] Pressed at {1}, which is correct. Module solved.", moduleId, Bomb.GetFormattedTime());
        }
        else
        {
            Module.HandleStrike();
            Debug.LogFormat("[BoozleTalk #{0}] Pressed at {1}, which is incorrect, strike!", moduleId, Bomb.GetFormattedTime());
        }
    }
}
