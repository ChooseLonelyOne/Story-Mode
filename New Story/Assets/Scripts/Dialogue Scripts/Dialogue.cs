using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;

    public Mans[] mans;
}

[System.Serializable]
public struct Mans
{
    public string name;
    [TextArea(3, 10)]
    public string[] words;
}


