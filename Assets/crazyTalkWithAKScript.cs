using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class crazyTalkWithAKScript : MonoBehaviour
{

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

    private static readonly string[] _words = new string[] { "TIMWI" };
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
        Debug.Log(_wordInFont.Count);
        var width = _wordInFont.Count / 5;
        Debug.Log(width);
        for (int i = 0; i < _wordInFont.Count; i++)
        {
            if (!_wordInFont[i])
                continue;
            float x = i % width - (width * 0.5f);
            float z = i / width - 2.5f;
            float y = 20;
            var rand = Rnd.Range(0.5f, 1.5f);
            Vector3 pos = new Vector3(x, y, z) * _size * rand;
            var obj = Instantiate(SphereObj);
            obj.transform.SetParent(WordParent);
            obj.transform.localPosition = pos;
            obj.transform.localScale = new Vector3(_size, _size, _size);
        }

        var angle = Rnd.Range(0, 360f);

        OriginParent.transform.localEulerAngles = new Vector3(0, angle, 0);
        OriginObj.transform.SetParent(transform);
        WordParent.transform.localEulerAngles = new Vector3(90f, angle, 0f);
        Debug.Log(OriginObj.transform.localPosition);
    }
}
