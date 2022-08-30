using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObstaclesWay : MonoBehaviour
{
    private void Awake()
    {
        if (transform.position.x == 0)
        {
            GameObject way = Instantiate(transform.gameObject, new Vector3(-3, 0, 0), Quaternion.identity, transform);
        }
        
    }
}
