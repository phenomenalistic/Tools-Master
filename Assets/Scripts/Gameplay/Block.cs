using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //public BlockProperties properties;

    [HideInInspector]
    public float endurance = 100.0f;
    private float startEndurance = 100.0f;

    [HideInInspector]
    public ToolType toolType = ToolType.shovel;

    [HideInInspector]
    public ObstacleTag obstacleType = ObstacleTag.grass;

    public GameObject model;
    public MeshRenderer meshRenderer;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        //type = properties.type;
        //toolType = properties.toolType;

        //meshRenderer.material = properties.material;

        //startEndurance = endurance = properties.endurance;
        
    }

    public bool Break(ToolProperies tool)
    {
        endurance -= 1;
        return SetSize();
    }

    bool SetSize()
    {
        model.transform.localScale = Vector3.one * Mathf.InverseLerp(0, startEndurance, endurance);
        if (endurance <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Init();
    }
#endif
}


