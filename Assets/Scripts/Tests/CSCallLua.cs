using XLua;

namespace XLuaExamples
{
	public class CSCallLua : XLuaTestCase 
	{
		LuaTable _csCallLuaTable;
		protected override void Setup()
		{
			base.Setup();
			XLuaManager.LuaEnv.DoString("include('CSCallLua')");
			_csCallLuaTable = XLuaManager.LuaEnv.Global.Get<LuaTable>("CSCallLua");
		}

		LuaFunction _testNumber;
		public void InitTestNumber()
		{
			_testNumber = _csCallLuaTable.Get<LuaFunction>("TestNumber");
		}
		// 60B GC
		public void TestNumber()
		{
			_testNumber.Call(10);
		}

		[CSharpCallLua]
		public delegate void IntParamMethod(int p);
		IntParamMethod _testNumberOpt;	
		public void InitTestNumberOpt()
		{
			_csCallLuaTable.Get("TestNumber", out _testNumberOpt);
		}
		// 0B GC
		public void TestNumberOpt()
		{
			_testNumberOpt(10);
		}

		LuaFunction _testObject;
		object _obj = "testObject";
		public void InitTestObject()
		{
			_testObject = _csCallLuaTable.Get<LuaFunction>("TestObject");
		}
		// 40B GC
		public void TestObject()
		{
			_testObject.Call(_obj);
		}

		[CSharpCallLua]
		public delegate void ObjParamMethod(object p);
		ObjParamMethod _testObjectOpt;	
		public void InitTestObjectOpt()
		{
			_csCallLuaTable.Get("TestObject", out _testObjectOpt);
		}
		// 0B GC
		public void TestObjectOpt()
		{
			_testObjectOpt(_obj);
		}
	}
}
