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
    float[] bz = { 0.95f, 1.122f, 1.07f, 1f };

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
        Screen.AddInteractionPunch(0.5f);
        if (!traversable)
        {
            Audio.PlaySoundAtTransform("KMT_bwomp", Screen.transform);
            traversable = true;
            Word.text = InvertIfNeeded();
            StartCoroutine(ActivateArrows());
            Debug.LogFormat("<KayMazey Talk #{0}> screen", moduleId);
        }
    }

    void ArrowPress(int a)
    {
        Arrows[a].AddInteractionPunch(0.5f);
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
                Audio.PlaySoundAtTransform("KMT_solve", Screen.transform);
                StartCoroutine(SolveAnim());
                return;
            }
            Audio.PlaySoundAtTransform("KMT_step", Screen.transform);
            invert = Rnd.Range(0, 10) < 4;
            Word.text = InvertIfNeeded();
            Debug.LogFormat("<KayMazey Talk #{0}> {1} ({2}), {3}", moduleId, dirNames[a], dirNames[r], InvertIfNeeded());
            StartCoroutine(ArrowAnim(1.03f, r));
        }
        else
        {
            Module.HandleStrike();
            Debug.LogFormat("[KayMazey Talk #{0}] Moving {1} from {2} leads nowhere, strike!", moduleId, dirNames[a], InvertIfNeeded());
            traversable = false;
            Word.text = mazeWords[goalPosition];
            StartCoroutine(DeactivateArrows());
        }
    }

    string InvertIfNeeded()
    {
        return mazeWords[currentPosition].Replace("K", invert ? "" : "K");
    }

    private IEnumerator ActivateArrows()
    {
        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].SetActive(true);
        }
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            for (int o = 0; o < 4; o++)
            {
                //use the bezier to make smooth
                float scale = Lerp(Lerp(Lerp(bz[0], bz[1], elapsed), Lerp(bz[1], bz[2], elapsed), elapsed), Lerp(Lerp(bz[1], bz[2], elapsed), Lerp(bz[2], bz[3], elapsed), elapsed), elapsed);
                ArrowObjs[o].transform.localScale = new Vector3(scale, 1f, scale);
            }
            yield return null;
            elapsed += Time.deltaTime;
        }
        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private IEnumerator ArrowAnim(float z, int w)
    {
        float elapsed = 0f;
        float duration = 0.2f;
        while (elapsed < duration)
        {
            float scale = Lerp(z, 1f, elapsed * 5);
            ArrowObjs[w].transform.localScale = new Vector3(scale, 1f, scale);
            yield return null;
            elapsed += Time.deltaTime;
        }
        ArrowObjs[w].transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator DeactivateArrows()
    {
        float elapsed = 0f;
        float duration = 0.1f;
        while (elapsed < duration)
        {
            float scale = Lerp(1f, 0f, elapsed * 10);
            for (int o = 0; o < 4; o++)
            {
                ArrowObjs[o].transform.localScale = new Vector3(o % 2 == 0 ? scale : 1f, 1f, o % 2 == 0 ? 1f : scale);
            }
            yield return null;
            elapsed += Time.deltaTime;
        }
        for (int o = 0; o < 4; o++)
        {
            ArrowObjs[o].SetActive(false);
            ArrowObjs[o].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private IEnumerator SolveAnim()
    {
        float elapsed = 0f;
        float duration = 0.4f;
        float om = 0.04f;
        while (elapsed < duration)
        {
            for (int o = 0; o < 4; o++)
            {
                ArrowObjs[o].SetActive(Rnd.Range(0, 2) == 0);
            }
            yield return new WaitForSeconds(om);
            yield return null;
            elapsed += Time.deltaTime + om;
            for (int o = 0; o < 4; o++)
            {
                ArrowObjs[o].SetActive(false);
            }
        }
    }
    
    float Lerp(float a, float b, float t)
    { //this assumes t is in the range 0-1
        return a * (1f - t) + b * t;
    }
}
