using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TextFragments",menuName ="ScriptableObjects/TextFragments",order = 51)]

public class TextFragments : ScriptableObject
{
    [TextArea(15,20)]
    public string  FragmentOfText1;
}
