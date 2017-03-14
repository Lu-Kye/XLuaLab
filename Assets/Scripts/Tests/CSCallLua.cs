using XLua;
using System.Collections.Generic;

namespace XLuaExamples
{
	[LuaCallCSharp]
	// Elapsed time at first time is more than next times.
	public class CSCallLua : XLuaTestCase<CSCallLua> 
	{
		// Elapsed milliseconds about 0.045
		// 0B GC
		public void TestGetInt()
		{
			var num = _luaTable.Get<int>("num");
		}

		// Elapsed milliseconds about 0.045
		// 32B GC
		LuaTable _testObj;
		public void TestGetObj()
		{
			_testObj = _luaTable.Get<LuaTable>("obj");
		}

		public class ClassA
		{
			public int a;
		}
		ClassA _testClassA;
		// Elapsed milliseconds about 0.08
		// 112B GC
		public void TestGetClass()
		{
			_testClassA = _luaTable.Get<ClassA>("classA");
			// if (this.EnableLog)
			// 	this.Log(_curMethodInfo, "a:{0}", _testClassA.a);
		}

		public struct Struct
		{
			public int a;
		}
		// Elapsed milliseconds about 0.07
		// 112B GC
		public void TestGetStruct()
		{
			var structA = _luaTable.Get<Struct>("structA");
			// if (this.EnableLog)
			// 	this.Log(_curMethodInfo, "a:{0}", structA.a);
		}

		List<int> _list;
		// Elapsed milliseconds about 0.03
		// 212B GC
		public void TestGetList()
		{
			_list = _luaTable.Get<List<int>>("listA");
		}

		Dictionary<string, int> _dict;
		// Elapsed milliseconds about 0.03
		// 700B GC
		public void TestGetDict()
		{
			_dict = _luaTable.Get<Dictionary<string, int>>("dictA");
		}

		LuaFunction _testNumber;
		public void InitTestNumber()
		{
			_testNumber = _luaTable.Get<LuaFunction>("TestNumber");
		}
		// Elapsed milliseconds about 0.03
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
			_luaTable.Get("TestNumber", out _testNumberOpt);
		}
		// Elapsed milliseconds about 0.015
		// 0B GC
		public void TestNumberOpt()
		{
			_testNumberOpt(10);
		}


		[LuaCallCSharp(GenFlag.GCOptimize)]
		public struct StructA
		{
			public int a;
		}
		[LuaCallCSharp(GenFlag.GCOptimize)]
		public struct StructB
		{
			public StructA b;
			public double c;
			public float d;
		}
		[CSharpCallLua]
		public delegate void StructBParamMethod(StructB B);
		StructBParamMethod _testStructOpt;
		StructB _testStructB = new StructB{
			b = new StructA{ a = 1 },
			c = 1.1,
			d = 1.1f,
		};
		public void InitTestStructOpt()
		{
			_luaTable.Get("TestStruct", out _testStructOpt);
		}
		public void TestStructOpt()
		{
			_testStructOpt(_testStructB);
		}


		LuaFunction _testObject;
		object _obj = "testObject";
		public void InitTestObject()
		{
			_testObject = _luaTable.Get<LuaFunction>("TestObject");
		}
		// Elapsed milliseconds about 0.015
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
			_luaTable.Get("TestObject", out _testObjectOpt);
		}
		// Elapsed milliseconds about 0.007
		// 0B GC
		public void TestObjectOpt()
		{
			_testObjectOpt(_obj);
		}
	}
}
