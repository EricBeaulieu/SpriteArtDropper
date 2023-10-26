using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GlobalTools
{
    public static string texturePath = $"{Application.persistentDataPath}/Assets/CurrentImage.png";
    public static Texture2D SavePNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            //Color[] pix = tex.GetPixels(); // get pixel colors
            //for (int i = 0; i < pix.Length; i++)
            //    pix[i].a = pix[i].grayscale; // set the alpha of each pixel to the grayscale value
            //tex.SetPixels(pix); // set changed pixel alphas
            tex.filterMode = FilterMode.Point;
            tex.Apply(); // upload texture to GPU
        }

        return tex;
        //byte[] _bytes = tex.EncodeToPNG();
        //System.IO.File.WriteAllBytes(texturePath, _bytes);
        //return $"{_bytes.Length / 1024} Kb was saved as: {texturePath}";
    }

    //static string _fullPath = "CurrentImage";
    static int currentPictureIndex = 0;

    //public static string returnPNG(string filePath)
    //{
    //    Texture2D tex = null;
    //    byte[] fileData;

    //    if (File.Exists(filePath))
    //    {
    //        fileData = File.ReadAllBytes(filePath);
    //        tex = new Texture2D(2, 2);
    //        tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
    //    }
    //    string path = Application.dataPath + $"/Assets/{_fullPath}{currentPictureIndex}";
    //    currentPictureIndex++;
    //    System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());

    //    return path;
    //}

    //static Texture2D load_s01_texture;
    //static void LoadTextureToFile(string filename)
    //{
    //    load_s01_texture = System.IO.File.ReadAllBytes(Application.dataPath + "/Save/" + filename);
    //}
}
