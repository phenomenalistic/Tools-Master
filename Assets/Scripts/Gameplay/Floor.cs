using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject floor;
    public GameObject finish;

    public static Floor current;

    public int floorLenght;

    public const int floorStartShift = -4;

    void Awake()
    {
        current = this;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        floor.transform.localScale = new Vector3(floorLenght, 1, 1);
        float newZposition = Mathf.FloorToInt(floorLenght / 2f);
        newZposition = floorLenght % 2f == 0 ? newZposition - 0.5f : newZposition;
        floor.transform.position = new Vector3(0, 0, newZposition + floorStartShift);

        finish.transform.position = new Vector3(0, 0, floorLenght + floorStartShift);
    }
#endif
}
