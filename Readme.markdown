# Test environment
Macbook Pro

# CSharp call lua
## Test call lua method with number parameter(struct is similar to number test)
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
// 0B GC
public void TestNumberOpt()
{
    _testNumberOpt(10);
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
// 0B GC
public void TestObjectOpt()
{
    _testObjectOpt(_obj);
}
```