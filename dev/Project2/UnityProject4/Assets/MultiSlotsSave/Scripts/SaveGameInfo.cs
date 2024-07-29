using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameInfo {

    public SaveGameInfo(string infoKey, string dataKey)
    {
        this.infoKey = infoKey;
        this.dataKey = dataKey;

        UpdateTime();
    }

    public string infoKey;
    public string dataKey;
    public string time;
    public byte[] texByteArray;
    public int texWidth;
    public int texHeight;

    public Texture2D GetScreenshot()
    {
        Texture2D tex = new Texture2D(texWidth, texHeight, TextureFormat.RGB24, false);
        tex.LoadRawTextureData(texByteArray);
        tex.Apply();
        return tex;
    }
    public void UpdateScreenShot(Texture2D texture)
    {
        texByteArray = texture.GetRawTextureData();
        texWidth = texture.width;
        texHeight = texture.height;
    }
    public void UpdateTime()
    {
        time = DateTime.Now.ToString("G");
    }
    public SaveGameInfo Clone()
    {
        return MemberwiseClone() as SaveGameInfo;
    }
}
