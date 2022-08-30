using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCameraImage : MonoBehaviour
{
    public RenderTexture rt;

    private void Start()
    {
        StartCoroutine(CaptureBeforeStart());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureImage();
        }
    }
    public void SaveTexture()
    {
        byte[] bytes = toTexture2D(rt).EncodeToPNG();
        System.IO.File.WriteAllBytes("C:/Users/sermo/Desktop/" + GetFileName() + ".png", bytes); 
    }
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    string GetFileName()
    {
        string filename;
        if (!PlayerPrefs.HasKey("CaptureImageNumber")) { PlayerPrefs.SetInt("CaptureImageNumber", 0); }

        filename = PlayerPrefs.GetInt("CaptureImageNumber").ToString();
        PlayerPrefs.SetInt("CaptureImageNumber", PlayerPrefs.GetInt("CaptureImageNumber")+1);
        PlayerPrefs.Save();
        return filename;
    }

    void CaptureImage()
    {
        SaveTexture();
        Debug.Log("Image captured");
    }

    IEnumerator CaptureBeforeStart()
    {
        yield return null;
        yield return null;
        yield return null;
        CaptureImage();
    }
}
