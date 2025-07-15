using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class quoteCrazyTalkEndQuoteScript : MonoBehaviour {

    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable MainScreen;
    public KMSelectable[] SmallScreens;
    public GameObject[] SmallObjs;
    public TextMesh[] Words;
    public TextMesh[] Digits;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    string[] tableWords = {
        "",     "KNEE", "NUMB", "TOFU", "ZERO", "ENVY", "AXIS", "EACH", "VETO", "",
        "ELSE", "TECH", "IDOL", "ECHO", "JOEY", "TREK", "EXAM", "KIWI", "EXPO", "MOJO",
        "TUTU", "DELI", "CRIB", "NOVA", "ODOR", "SYNC", "FERN", "ONCE", "AMMO", "EURO",
        "VOID", "REDO", "ITEM", "AUTO", "EVIL", "ISLE", "OBOE", "INFO", "PESO", "GURU",
        "ZANY", "GNAW", "HAJJ", "DUTY", "AHOY", "TUFT", "EDGE", "ORCA", "ERGO", "YETI",
        "AURA", "INCH", "DATA", "SHWA", "ONLY", "ULNA", "IDEA", "UREA", "ONYX", "ANAL",
        "POEM", "ATOM", "HOBO", "HIYA", "STYE", "ALLY", "JUDO", "BUZZ", "JAVA", "WIKI",
        "ANON", "GRUB", "YOLK", "CRUX", "ORGY", "LIAR", "BOZO", "LUAU", "WREN", "IOTA",
        "WHOA", "UGLY", "RELY", "HOAX", "CLEF", "EPEE", "UNDO", "SEXY", "LYNX", "DEMO",
        "",     "IDLY", "FOCI", "EPIC", "DODO", "CYAN", "ICKY", "OBEY", "MENU", ""
    };
    string[] tableDigits = {
        "9814072356", //top, from left to right
        "2170348569", //right, from top to bottom
        "1029765384", //bottom, from left to right
        "3816547290" //left, from top to bottom
    };

    void Awake()
    {
        moduleId = moduleIdCounter++;
        
        MainScreen.OnInteract += delegate () { MainPress(); return false; };

        for (int s = 0; s < 4; s++) {
            int sx = s; //this is so incredibly dumb
            SmallScreens[s].OnInteract += delegate { SmallPress(sx); return false; };
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int s = 0; s < 4; s++)
        {
            SmallObjs[s].SetActive(false);
        }

    retry:
        int[] positions = Enumerable.Range(0, 100).Except(new[] { 0, 9, 90, 99 }).ToArray().Shuffle().Take(4).ToArray();
        if (!positionsValid(positions))
        {
            goto retry;
        }
    }

    bool positionsValid(int[] a) //this is deceptively tricky
    {
        return true;
    }

    void MainPress() {
        Debug.Log("om");
    }

    void SmallPress(int s) {
        Debug.Log("s " + s);
    }
}
