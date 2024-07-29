using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RendererExt {

    public static Texture2D CreateScreenShot(this Camera cam)
    {
        
        var width = Screen.width;
        var height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        screenShot.Apply();
        cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        return screenShot;       

    }

    public static Texture2D ToTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
    public static Sprite ToSprite(this Texture2D tex, float heightFactor = 1)
    {
        float readHeight = tex.height * heightFactor;
        float Ypos = tex.width - readHeight == 0 ? 0 :

            (tex.height - readHeight) / 2;
        Rect rect = new Rect(0, Ypos, tex.width, readHeight);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(tex, rect, pivot);
    }
    public static Sprite ToSprite(this RenderTexture rTex, float heightFactor = 1)
    {
        return rTex.ToTexture2D().ToSprite(heightFactor);
    }
    
}
