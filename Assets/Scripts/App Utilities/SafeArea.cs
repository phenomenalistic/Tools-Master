using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Awake()
    {
        SetSafeArea();
        Debug.Log("SafeArea");
    }

    void SetSafeArea()
    {
        var safeArea = Screen.safeArea;

        //var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        //anchorMin.x /= Screen.width;
        //anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;


        GetComponent<RectTransform>().anchorMax = anchorMax;
    }
}
