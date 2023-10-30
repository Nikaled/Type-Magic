using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level List", menuName = "ScriptableObjects/Level List", order = 51)]
public class LevelListSO : ScriptableObject
{
    public List<string> LevelsNameList;
}
