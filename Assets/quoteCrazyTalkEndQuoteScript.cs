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
        int[] positions = { -1, -1, -1, -1 };
        for (int p = 0; p < 4; p++)
        {
            int v;
            do
            {
                v = Rnd.Range(1, 99); //avoids 0 (top-left) and 99 (bottom-right)
            } while (v == 9 || v == 90 || positions.Contains(v));
            positions[p] = v;
        }
        if (!positionsValid(positions)) {
            goto retry;
        }
    }

    bool positionsValid(int[] a) //this is deceptively tricky
    {
        /*
        int[] xs = { -1, -1, -1, -1 };
        int[] ys = { -1, -1, -1, -1 };
        for (int e = 0; e < 4; e++)
        {
            xs[e] = a[e] % 10;
            ys[e] = a[e] / 10;
        }
        int[] xb = { 10, -10 };
        int[] yb = { 10, -10 };
        for (int e = 0; e < 4; e++)
        {
            if (xs[e] < xb[0]) { xb[0] = xs[e]; }
            if (xs[e] > xb[1]) { xb[1] = xs[e]; }
            if (ys[e] < yb[0]) { yb[0] = ys[e]; }
            if (ys[e] > yb[1]) { yb[1] = ys[e]; }
        }
        int[] edge = { -1, -1, -1, -1 };
        for (int d = 0; d < 10; d++)
        {
            if (!edge.Contains(-1)) { break; }
            for (int e = 0; e < 4; e++) {
                if (ys[e] == d) {  }
            }
        }
        */
        return true;
    }

    void MainPress() {
        Debug.Log("ommmmmmmmm");
    }

    void SmallPress(int s) {
        Debug.Log("s " + s);
    }
}
