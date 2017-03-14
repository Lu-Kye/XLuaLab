# Test environment
Macbook Pro

# Use csharp API in lua
* New csharp class
```lua
local gameObject = CS.UnityEngine.GameObject()
```
* Call csharp static method and get static field
```lua
CS.UnityEngine.GameObject.Find("GameObject")
local deltaTime = CS.UnityEngine.Time.deltaTime
```
* Call csharp object method ang get field
```lua
gameObject:SetActive(false)
local transform = gameObject.transform
```

# CSharp call lua
## Test get luaTable fields
* Int
```lua
CSCallLua.num = 10
```
```csharp
// Elapsed milliseconds about 0.005
// 0B GC
public void TestGetInt()
{
    var num = _luaTable.Get<int>("num");
}
```
* LuaTable
```lua
CSCallLua.obj = {}
```
```csharp
// Elapsed milliseconds about 0.007
// 0B GC
public void TestGetObj()
{
    LuaTable obj = _luaTable.Get<LuaTable>("obj");
}
```
* LuaTable to CSharp Class
```lua
CSCallLua.classA = { a = 1 }
```
```csharp
public class ClassA
{
    public int a;
}
ClassA _testClassA;
// Elapsed milliseconds about 0.01
// 112B GC
public void TestGetClass()
{
    _testClassA = _luaTable.Get<ClassA>("classA");
    // if (this.EnableLog)
    // 	this.Log(_curMethodInfo, "a:{0}", _testClassA.a);
}
```
* LuaTable to CSharp Struct
```lua
CSCallLua.structA = { a = 1 }
```
```csharp
public struct Struct
{
    public int a;
}
// Elapsed milliseconds about 0.009 
// 112B GC
public void TestGetStruct()
{
    var structA = _luaTable.Get<Struct>("structA");
    // if (this.EnableLog)
    // 	this.Log(_curMethodInfo, "a:{0}", structA.a);
}
```
* LuaTable to CSharp Interface (Recommendation)
```lua
CSCallLua.interfaceA = {
    a = 1,
    A = function() 
    end,
}
```
```csharp
[CSharpCallLua]
public interface IInterfaceA
{
    int a { get; set; }
    void A();
}
IInterfaceA _interfaceA;
// Elapsed milliseconds about 0.007
// 32B GC
public void TestGetInterface()
{
    _interfaceA = _luaTable.Get<IInterfaceA>("interfaceA");
}
```
* LuaTable to CSharp List
```lua
CSCallLua.listA = { 1 }
```
```csharp
List<int> _list;
// Elapsed milliseconds about 0.012
// 212B GC
public void TestGetList()
{
    _list = _luaTable.Get<List<int>>("listA");
}
```
* LuaTable to CSharp Dictionary
```lua
CSCallLua.dictA = { a = 1 }
```
```csharp
Dictionary<string, int> _dict;
// Elapsed milliseconds about 0.014
// 700B GC
public void TestGetDict()
{
    _dict = _luaTable.Get<Dictionary<string, int>>("dictA");
}
```

## Test call lua method with number parameter
```lua
function CSCallLua.TestNumber(num)
end
```
* Without gc optimization
```csharp
LuaFunction _testNumber;
public void InitTestNumber()
{
    _testNumber = _csCallLuaTable.Get<LuaFunction>("TestNumber");
}
// Elapsed milliseconds about 0.003
// 60B GC
public void TestNumber()
{
    _testNumber.Call(10);
}
```
* With gc optimization
```csharp
// Will generate code
[CSharpCallLua]
public delegate int IntParamMethod(int p);
IntParamMethod _testNumberOpt;	
public void InitTestNumberOpt()
{
    _csCallLuaTable.Get("TestNumber", out _testNumberOpt);
}
// Elapsed milliseconds about 0.002
// 0B GC
public void TestNumberOpt()
{
    _testNumberOpt(10);
}
```

## Test call lua method with csharp struct parameter
```lua
function CSCallLua.TestStruct(struct)
    -- Log.Debug(tostring(struct.c))
end
```
```csharp
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
// Elapsed milliseconds about 0.003
// 0B GC
public void TestStructOpt()
{
    _testStructOpt(_testStructB);
}
```

## Test call lua method with object parameter
```lua
function CSCallLua.TestObject(obj)
end
```
* Without gc optimization
```csharp
LuaFunction _testObject;
object _obj = "testObject";
public void InitTestObject()
{
    _testObject = _csCallLuaTable.Get<LuaFunction>("TestObject");
}
// Elapsed milliseconds about 0.0037
// 40B GC
public void TestObject()
{
    _testObject.Call(_obj);
}
```
* With gc optimization
```csharp
[CSharpCallLua]
public delegate void ObjParamMethod(object p);
ObjParamMethod _testObjectOpt;	
public void InitTestObjectOpt()
{
    _csCallLuaTable.Get("TestObject", out _testObjectOpt);
}
// Elapsed milliseconds about 0.0030
// 0B GC
public void TestObjectOpt()
{
    _testObjectOpt(_obj);
}
```

# Some other features
* Custom lua loader
* Configure *CSharpCallLua* by list, like below:
```csharp
[CSharpCallLua]
public static List<Type> CSharpCallLua = new List<Type>()
{
    typeof(Action),
    typeof(Action<bool>),
    typeof(UnityAction),
};
```
* Lua call csharp function with *out*, *ref*
* Lua call csharp overload functions