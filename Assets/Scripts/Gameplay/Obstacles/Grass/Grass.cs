using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject grassBlade, grass;

    public int countX = 10, countY = 10;
    public float randomPosition = 0.05f, randomRotation = 30;
    public List<GrassMaterials> grassMaterials;
    [HideInInspector] [SerializeField] public GrassMaterials wm = new GrassMaterials();
    public BoxCollider trigger;

    [HideInInspector]public bool lustBlock = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mower")
        {
            UpdateGrassPlades(other.transform.gameObject);
        }
    }

    private void UpdateGrassPlades(GameObject pointer)
    {
        foreach (Transform gb in grass.transform)
        {
            if (pointer.transform.position.z >= gb.position.z || Vector3.Distance(pointer.transform.position, gb.position) <= 0.45f)
            {
                gb.gameObject.SetActive(false);
                gb.transform.parent = transform;
            }
        }
        if (grass.transform.childCount == 0)
        {
            trigger.enabled = false;
            if (lustBlock) { StopMowerPS(pointer.GetComponent<MowerPS>()); }
        }
    }

    private void StopMowerPS(MowerPS mps)
    {
        mps.Stop();
    }
#if UNITY_EDITOR
    /*
    private void OnValidate()
    {
        //CreateGrass(countX, countY);
        //RandomizePosition();
        //RandomizeRotation();
        //SetGrassColor();
    }
    */

    public void SetGrassColor(GrassMaterials.GrassColors grassColor)
    {
        Material newMaterial = grassMaterials[0].material;

        foreach (GrassMaterials gm in grassMaterials)
        {
            if (gm.name == grassColor) { newMaterial = gm.material; break; }
        }

        foreach (Transform gb in grass.transform)
        {
            gb.GetComponent<MeshRenderer>().material = newMaterial;
        }
    }
    
    private void CreateGrass(int x = 10, int y = 10)
    {
        RemoveOldGrass();
        grassBlade.SetActive(true);
        //grassBlades.Clear();
        float xShift = 1 / (float)y, yShift = 1 / (float)x;
        for (int i = 0; i <= x; i++)
        {
            for (int o = 0; o <= y; o++)
            {
                GameObject gb = Instantiate(grassBlade, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), grass.transform);
                gb.transform.localPosition = new Vector3((o * xShift) - 0.5f, -0.5f, (i * yShift) - 0.5f);
                //grassBlades.Add(gb);
            }
        }
        grassBlade.SetActive(false);
    }

    private void RemoveOldGrass()
    {
        if (grass.transform.childCount > 0)
        {
            foreach (Transform child in grass.transform)
            {
                StartCoroutine(Destroy(child.gameObject));
            }
        }
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }


    private void RandomizePosition()
    {
        foreach (Transform gb in grass.transform)
        {
            gb.transform.localPosition += new Vector3(Random.Range(-randomPosition, randomPosition), 0, Random.Range(-randomPosition, randomPosition));
            gb.transform.localPosition = new Vector3(Mathf.Clamp(gb.transform.localPosition.x, -0.5f, 0.5f), gb.transform.localPosition.y, Mathf.Clamp(gb.transform.localPosition.z, -0.5f, 0.5f));
        }
    }

    private void RandomizeRotation()
    {
        foreach (Transform gb in grass.transform)
        {
            gb.transform.rotation = Quaternion.Euler(0, 45 + Random.Range(-randomRotation, randomRotation), 0);
        }
    }
#endif
}

[System.Serializable]
public struct GrassMaterials
{
    public enum GrassColors
    {
        green, blue, whiteAndPurple, yellow
    }

    public Material material;
    public Color oneColor, twoColor;
    public GrassColors name;
}
