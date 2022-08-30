using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ToolProperies", menuName = "ScriptableObjects/ToolProperies")]
public class ToolProperies : ScriptableObject
{
    public ObstacleTag obstacleTag = ObstacleTag.grass;
    public Sprite icon;

    public List<Vector3> positions = new List<Vector3> { };

    public string moveAnimation;

    public GameObject prefab;
    public float leftWeight, rightWeight, chestWeight;

    public float raycastShift = 1;

    public float moveSpeed = 2f;

    private Dictionary<ObstacleTag, string> tagDictionary = new Dictionary<ObstacleTag, string> { 
        { ObstacleTag.grass, "Grass" },
        { ObstacleTag.balloons, "Balloons" },
        { ObstacleTag.wall, "Wall" },
        { ObstacleTag.water, "Water" },
        { ObstacleTag.spiral, "Spiral" },
        { ObstacleTag.freeHands, "FreeHands" },
        { ObstacleTag.untagged, "Untagged" },

    };

    public bool IsSameTag(string tag)
    {
        return tag == tagDictionary[obstacleTag];
    }
}

public enum ObstacleTag
{
    grass, balloons, wall, water, spiral, freeHands, untagged
}

public enum ToolType
{
    shovel, pickaxe, axe
};
