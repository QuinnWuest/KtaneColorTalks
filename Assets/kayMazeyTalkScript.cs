using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class kayMazeyTalkScript : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable Screen;
    public KMSelectable[] Arrows;
    public TextMesh Word;
    public GameObject[] ArrowObjs;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    string[] mazeWords = {
        "Knit",   "Knows",   "Knock",   "",       "Knew",   "Knoll",
        "Kneed",  "Knuff",   "Knork",   "Knout",  "Knits",  "",
        "Knife",  "Knights", "Knap",    "Knee",   "Knocks", "",
        "Knacks", "Knab",    "Knocked", "Knight", "Knitch", "",
        "Knots",  "Knish",   "Knob",    "Knox",   "Knur",   "",
        "Knook",  "Know",    "",        "Knack",  "Knurl",  "Knot"
    };
    int[] mazeDirs = {
        2, 14, 12, -1,  6,  8,
        2, 13,  1,  4,  5, -1,
        6, 11, 14, 11, 13, -1,
        7, 12,  7, 12,  1, -1,
        1,  5,  1,  7, 12, -1,
        2,  9, -1,  1,  3,  8
    };
    bool traversable = false;
    bool invert = false;
    int currentPosition = -1;
    int goalPosition = -1;
    int[] vectors = { -6, 1, 6, -1 };
    string[] dirNames = { "up", "right", "down", "left" };

    void Awake()
    {
        moduleId = moduleIdCounter++;

        Screen.OnInteract += delegate () { ScreenPress(); return false; };

        for (int a = 0; a < 4; a++)
        {
            int ax = a; //this is so incredibly dumb
            Arrows[a].OnInteract += delegate { ArrowPress(ax); return false; };
        }
    }

    // Use this for initialization
    void Start()
    {
        do
        {
            currentPosition = Rnd.Range(0, 36);
            goalPosition = Rnd.Range(0, 36);
        } while (mazeWords[currentPosition] == "" || mazeWords[goalPosition] == "" || currentPosition == goalPosition);

        Word.text = mazeWords[goalPosition];

        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].SetActive(false);
        }

        invert = Rnd.Range(0, 10) < 4;

        Debug.LogFormat("[KayMazey Talk #{0}] Your goal is {1}.", moduleId, mazeWords[goalPosition]);
        Debug.LogFormat("[KayMazey Talk #{0}] You start at {1}.", moduleId, mazeWords[currentPosition]);
    }

    void ScreenPress()
    {
        if (!traversable)
        {
            Debug.LogFormat("<KayMazey Talk #{0}> screen", moduleId);
            traversable = true;
            Word.text = InvertIfNeeded();
            StartCoroutine(ActivateArrows());
        }
    }

    void ArrowPress(int a)
    {
        int r = invert ? a ^ 2 : a;
        int p = (int)Math.Pow(2, r);
        if ((mazeDirs[currentPosition] & p) == p)
        {
            currentPosition += vectors[r];
            if (currentPosition == goalPosition)
            {
                Word.text = "";
                Module.HandlePass();
                Debug.LogFormat("[KayMazey Talk #{0}] You made it to the goal. Module solved.", moduleId);
                return;
            }
            invert = Rnd.Range(0, 10) < 4;
            Word.text = InvertIfNeeded();
            Debug.LogFormat("<KayMazey Talk #{0}> {1} ({2}), {3}", moduleId, dirNames[a], dirNames[r], InvertIfNeeded());
        }
        else
        {
            Module.HandleStrike();
            Debug.LogFormat("[KayMazey Talk #{0}] Moving {1} from {2} leads nowhere, strike!", moduleId, dirNames[a], InvertIfNeeded());
            StartCoroutine(DeactivateArrows());
            traversable = false;
            Word.text = mazeWords[goalPosition];
        }
    }

    string InvertIfNeeded () {
        return mazeWords[currentPosition].Replace("K", invert ? "" : "K");
    }

    private IEnumerator ActivateArrows()
    {
        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].SetActive(true);
        }
        yield return null;
    }

    private IEnumerator DeactivateArrows()
    {
        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].SetActive(false);
        }
        yield return null;
    }
}
