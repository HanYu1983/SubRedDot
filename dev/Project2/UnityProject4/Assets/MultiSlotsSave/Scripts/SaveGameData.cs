using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[System.Serializable]
public class SaveGameData {

    public SaveGameData(string key, SaveHistory data, Texture2D screenshot)
    {
        this.key = key;
        this.data = JsonUtility.ToJson(data);
        screenTextureArray = screenshot.GetRawTextureData();
        time=DateTime.Now.ToString("yyyy-MM-dd");
    }

    public string key;
    public string time;
    public string data;
    public byte[] screenTextureArray; 

    public SaveHistory ReadGameHistory()
    {
        return JsonUtility.FromJson<SaveHistory>(data);
    }
    public Texture2D GetScreenshot()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(screenTextureArray);
        return tex;
    }
    public SaveGameData Clone()
    {
        return MemberwiseClone() as SaveGameData;
    }
}
