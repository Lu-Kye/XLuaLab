using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace XLuaExamples
{
    public class XLuaTestCase : MonoBehaviour
    {
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
                    var methodInfo = methodInfos[i];
                    var methodName = methodInfo.Name;
                    if (methodName.StartsWith("Test") && methodName.Length > "Test".Length)
                        testCases.Add(methodName);
                }
                return _testCases = testCases.ToArray();
            }
        }
        public string OnlyTestCase;

        MethodInfo[] _initMethods;
        MethodInfo[] _testMethods;

        protected virtual void Awake()
        {
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
                _initMethods[i].Invoke(this, null);
            }
		}

        [ContextMenu("DoSetup")]
        protected void DoSetup()
        {
            this.Setup();
        }

        protected virtual void Setup()
        {
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
                if (string.IsNullOrEmpty(this.OnlyTestCase) == false 
                    && _testMethods[i].Name != this.OnlyTestCase)
                    continue;

                _testMethods[i].Invoke(this, null);
            }
		}

        public bool UpdatingTest = false;
        protected virtual void Update()
        {
            if (UpdatingTest)
            {
                this.Test();
            }
        }
    }
}