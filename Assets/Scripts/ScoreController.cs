using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ScoreController
{
    static int score = 0;

    static public int Score
    {
        get { return score; }
        set { score = value; }
    }
}
