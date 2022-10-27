using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static int Sign(float v)
    {
        if (v == 0.0f) return 0;
        else return v > 0.0f ? 1 : -1;
    }
}
