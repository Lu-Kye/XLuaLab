using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResManager
{
    public static readonly ResManager Instance = new ResManager();
    ResManager() {}     

    /**
     * If is editor, load lua file by fileStream.
     * If is mobile, load lua file by assetBundle.
     * @path : start from Assets(exclude), like "Scripts/*.cs"
     */
    public byte[] LoadLua(string path)
    {

#if UNITY_EDITOR
        var fullpath = Application.dataPath + "/" + path;
        byte[] bytes;
        using (FileStream fsRead = new FileStream(fullpath, FileMode.Open))
        {
            int fsLen = (int)fsRead.Length;
            bytes = new byte[fsLen];
            fsRead.Read(bytes, 0, bytes.Length);
        } 
        return bytes;
#endif
        return this.Load<TextAsset>(path).bytes;
    }


    /**
     * If is editor, load asset by assetDatabase.
     * If is mobile, load asset by assetBundle.
     * @path : start from Assets(exclude), like "Scripts/*.cs"
     */
    public T Load<T>(string path)
        where T : Object
    {
#if UNITY_EDITOR
        var fullpath = "Assets/" + path;
        return AssetDatabase.LoadAssetAtPath<T>(fullpath);
#endif
        return null;
    }
}