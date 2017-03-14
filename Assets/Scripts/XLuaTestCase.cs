using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaExamples
{
    [LuaCallCSharp]
    public class XLuaTestCase<T> : MonoBehaviour
        where T : XLuaTestCase<T>
    {
        public static T Instance { get; private set; }

        protected virtual string LuaFile { get { return this.GetType().Name; } }
        protected XLua.LuaTable _luaTable;

        public bool EnableLog = false;
        protected virtual void Log(MethodInfo methodInfo, string format, params object[] args)
        {
            if (this.EnableLog == false)
                return;

            Debug.Log(string.Format("{0}::{1}: {2}", this.GetType().Name, methodInfo.Name, string.Format(format, args))); 
        }

        protected MethodInfo _curMethodInfo;

        string[] _testCases;
		protected virtual string[] TestCases {
            get
            {
                if (_testCases != null)
                    return _testCases;

                var testCases = new List<string>();
                var methodInfos = this.GetType().GetMethods();
                for (int i = 0; i < methodInfos.Length; i++)
                {
                    var methodInfo = _curMethodInfo = methodInfos[i];
                    var methodName = methodInfo.Name;
                    if (methodName.StartsWith("Test") && methodName.Length > "Test".Length)
                        testCases.Add(methodName);
                }
                return _testCases = testCases.ToArray();
            }
        }
        public string OnlyTestCase;

        public string CurrentTestCase { get; private set; }

        MethodInfo[] _initMethods;
        MethodInfo[] _testMethods;

        protected virtual void Awake()
        {
            Instance = this as T;

            var testCases = this.TestCases;
            _initMethods = new MethodInfo[testCases.Length]; 
            _testMethods = new MethodInfo[testCases.Length]; 
			for (int i = 0; i < testCases.Length; i++)
            {
                var type = this.GetType();
                _initMethods[i] = type.GetMethod("Init" + testCases[i]);
                _testMethods[i] = type.GetMethod(testCases[i]);
            }
        }

		protected virtual void Start()
		{
            Setup();
            for (int i = 0; i < _initMethods.Length; i++)
            {
                if (_initMethods[i] == null)
                    continue;
                _initMethods[i].Invoke(this, null);
            }
		}

        [ContextMenu("DoSetup")]
        protected void DoSetup()
        {
            this.Start();
        }

        protected virtual void Setup()
        {
            if (string.IsNullOrEmpty(LuaFile))
                return;
			XLuaManager.LuaEnv.DoString(string.Format("include('{0}')", LuaFile));
			_luaTable = XLuaManager.LuaEnv.Global.Get<XLua.LuaTable>(LuaFile);
        }

        [ContextMenu("DoTest")]
        protected void DoTest()
        {
            this.Test();
        }

		protected virtual void Test()
		{
            for (int i = 0; i < _testMethods.Length; i++)
            {
                if (_testMethods[i] == null)
                    continue;

                if (string.IsNullOrEmpty(this.OnlyTestCase) == false 
                    && _testMethods[i].Name != this.OnlyTestCase)
                    continue;
                
                this.CurrentTestCase = _testMethods[i].Name;

                var startTime = Time.realtimeSinceStartup;
                _testMethods[i].Invoke(this, null);
                if (this.EnableLog)
                    this.Log(_testMethods[i], "Elapsed milliseconds {0}", (Time.realtimeSinceStartup - startTime) * 1000);
            }
		}

        public bool UpdatingTest = false;
        protected virtual void Update()
        {
            if (UpdatingTest)
                this.Test();
        }
    }
}