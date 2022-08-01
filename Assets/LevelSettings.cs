using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_Settings", menuName = "ScriptableObjects/Level/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    
    public int minEnemies;
    
    public int maxEnemies;
}
