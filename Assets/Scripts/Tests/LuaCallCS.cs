using XLua;

namespace XLuaExamples
{
    [LuaCallCSharp]
    public class LuaCallCS : XLuaInstTestCase<LuaCallCS>
    { 
        protected override string LuaFile { get { return "LuaCallCS"; } }
        
        [CSharpCallLua]
        public delegate void NoParamMethod(LuaTable self);

        NoParamMethod _testNumber;
        public void InitTestNumber()
        {
            _luaTable.Get("TestNumber", out _testNumber);
        }
        public void TestNumber()
        {
            _testNumber(_luaTable);
        }        
        public void DoTestNumber(int num)
        {
        }

        NoParamMethod _testObject;
        public void InitTestObject()
        {
            _luaTable.Get("TestObject", out _testObject);
        }
        public void TestObject()
        {
            _testObject(_luaTable);
        }
        public void DoTestObject(object obj)
        {
        }
    }
}