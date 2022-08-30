using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObstacles : MonoBehaviour
{
    public ObstacleTag obstacleTag = ObstacleTag.grass;

    private float speedMulAdd = 1.1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (IsSameTool()) { SkipPlayer(); }
        }
    }

    bool IsSameTool()
    {
        return Player.current.currentTool.obstacleTag == obstacleTag;
    }

    private void SkipPlayer()
    {
        Player.current.moveSpeedMultipier += speedMulAdd;
        gameObject.SetActive(false);
    }

}
