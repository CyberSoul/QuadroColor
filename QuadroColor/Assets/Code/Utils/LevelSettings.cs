using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "NewSettings", menuName = "Quadro/Level", order = 1)]
public class LevelSettings : ScriptableObject
{
    public int deskSize;
    public int colorPairs;
}
