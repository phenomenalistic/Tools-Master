using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockProperties", menuName = "ScriptableObjects/BlockProperties")]
public class ObstacleProperties : ScriptableObject
{
    public ObstacleTag tag = ObstacleTag.grass;
}

