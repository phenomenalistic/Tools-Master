using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObstacles : MonoBehaviour
{
    public int length = 3;
    public bool finishObstacless = false;
    public GameObject grassBlock, blocks, trigger, psGO, way, finishObstaclessGO;
    public GrassMaterials.GrassColors grassColor;
    public ParticleSystem ps;
#if UNITY_EDITOR

    private void OnValidate()
    {
        if (gameObject.scene.name != null && gameObject.scene.name != grassBlock.name) { PutGrassBlocks();  }
        ChangeLength(length);
        PutWay();
        IsFinishObstacles();
    }

    public void IsFinishObstacles()
    {
        finishObstaclessGO.GetComponent<FinishObstacles>().obstacleTag = ObstacleTag.grass;
        if (finishObstacless)
        {
            transform.localScale = new Vector3(3, 3, 3);
            finishObstaclessGO.SetActive(true);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            finishObstaclessGO.SetActive(false);
        }
    }
    public void ChangeLength(int length)
    {
        trigger.transform.localScale = new Vector3(1, 3, length);
        float newZposition = Mathf.FloorToInt((float)length / 2f);
        newZposition = length % 2f == 0 ? newZposition - 0.5f : newZposition;
        trigger.transform.localPosition = new Vector3(0, 1.1f, newZposition);
    }

    private void PutWay()
    {
        way.transform.localScale = new Vector3(length, 1, 1);
        way.transform.localPosition = new Vector3(0, -1, (float)length / 2f - 0.5f);
    }
    public void PutGrassBlocks()
    {
        RemoveOldBlocks();
        CreateBlocks();
        SetGrassBlocksMaterial();
    }
    private void CreateBlocks()
    {
        Grass g = new Grass { };
        for (int i = 0; i < length; i++)
        {
            GameObject go = Instantiate(grassBlock, new Vector3(transform.position.x, transform.position.y, transform.position.z + i), Quaternion.Euler(0, 0, 0), blocks.transform);
            g = go.GetComponent<Grass>();
            g.SetGrassColor(grassColor);
            g.lustBlock = false;
        }
        g.lustBlock = true;
    }

    private void RemoveOldBlocks()
    {
        if (blocks.transform.childCount > 0)
        {
            foreach (Transform child in blocks.transform)
            {
                StartCoroutine(Destroy(child.gameObject));
            }
        }
    }

    private void SetGrassBlocksMaterial()
    {
        List<GrassMaterials> grassMaterials = grassBlock.GetComponent<Grass>().grassMaterials;
        GrassMaterials wm = new GrassMaterials();
        foreach (GrassMaterials g in grassMaterials)
        {
            if (g.name == grassColor) { wm = g; break; }
        }
        foreach (Transform gb in blocks.transform)
        {
            gb.GetComponent<Grass>().wm = wm;
        }
        SetPSColors(wm);
    }

    private void SetPSColors(GrassMaterials wm)
    {
        ParticleSystem.MainModule main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(wm.oneColor, wm.twoColor);
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }
#endif
}
