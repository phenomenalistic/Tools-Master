using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBladesSpin : MonoBehaviour
{
    public GameObject blades;
    public float speed = 10;
    void Update()
    {
        blades.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
