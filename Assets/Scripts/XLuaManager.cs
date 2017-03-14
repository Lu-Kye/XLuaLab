using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharpAttribute]
public class XLuaManager : MonoBehaviour
{
    public static readonly LuaEnv LuaEnv = new LuaEnv();
    public static XLuaManager Instance { get; private set; }

    float _lastGCTime = 0;
    // Lua gc interval (seconds)
    public float GCInterval = 1.0f;

    // Path start from Assets, like "Scripts/Lua"
    public string LuaRootPath; 

    public string[] PreloadLuas;

#if UNITY_EDITOR
    public List<string> LoadedLuas;
#endif

    Action _start;
    Action _update;
    Action _onDestroy;

    void Awake()
    {
        Instance = this;

        this.LoadedLuas.Clear();
        for (int i = 0; i < this.PreloadLuas.Length; i++) 
        {
            this.LoadFile(this.PreloadLuas[i]);
        }
    }

    /**
     * Load lua file and doString.
     * @filepath: Start from LuaRootPath without file extension.
     */
    public void LoadFile(string filepath)
    {
        // TODO: Cache loaded file when in release mode, and reload every time in debug mode

#if UNITY_EDITOR
        if (this.LoadedLuas.Contains(filepath) == false)
        {
            this.LoadedLuas.Add(filepath);
        }
        else
        {
            this.LoadedLuas.Remove(filepath);
            Debug.Log(string.Format("Reload lua file({0})", filepath));
            this.LoadedLuas.Add(filepath);
        }
#endif

        var fullpath = string.Format("{0}/{1}.lua", this.LuaRootPath, filepath);
        var bytes = ResManager.Instance.LoadLua(fullpath);
        LuaEnv.DoString(bytes);
    }

    void Start() 
    {
        LuaEnv.Global.Get("Start", out _start);
        LuaEnv.Global.Get("Update", out _update);
        LuaEnv.Global.Get("OnDestroy", out _onDestroy);

        _start();
    }

    void Update()
    {
        _update();

        if ((Time.realtimeSinceStartup - _lastGCTime) > this.GCInterval)
        {
            _lastGCTime = Time.realtimeSinceStartup;
            LuaEnv.Tick();    
        }
    }

    void OnDestroy()
    {
        _onDestroy();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)        
            LuaEnv.StopGc();
        else
            LuaEnv.RestartGc();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("LuaMemory: {0}KB", LuaEnv.Memroy));
        GUILayout.EndHorizontal();
    }

}