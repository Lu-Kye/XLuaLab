using XLua;
using UnityEngine;

namespace XLuaExamples
{
    [LuaCallCSharp]
    public class LuaCallCS : XLuaTestCaseForLua<LuaCallCS>
    { 
        protected override string LuaFile { get { return "LuaCallCS"; } }
        
        public void TestNumber(int num)
        {
        }

        public void TestObject(object obj)
        {
        }

        public void TestOutRef(ref int a, out int b)
        {
            a = 1;
            b = 2;
        }

        public void TestOverload() {}
        public void OverloadA(int a)
        {
            // Debug.Log(string.Format("int a = {0}", a));
        }
        public void OverloadB(LuaTable a)
        {
            // Debug.Log(string.Format("LuaTable a = {0}", a.ToString()));
        }
    }
}