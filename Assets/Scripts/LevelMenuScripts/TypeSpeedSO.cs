using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type Speed settings", menuName = "ScriptableObjects/Type Speed settings", order = 51)]

public class TypeSpeedSO : ScriptableObject
{
    public int TypeSpeed;
    public bool IsChallengeMode;
}
