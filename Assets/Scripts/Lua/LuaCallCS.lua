-- Elapsed time at first time is more than next times.
-- Elapsed time is calculated by repeating 100000 times. 

LuaCallCS = Class("LuaCallCS", XLuaTestCase) 

-- Elapsed milliseconds about 0.0019
-- 0B GC
function LuaCallCS:TestNumber()
    self.csharpTestCase:TestNumber(10);
end

-- Elapsed milliseconds about 0.0061
-- 0B GC
-- LuaCallCS.obj1 = CS.UnityEngine.GameObject() -- No issue.
LuaCallCS.obj1 = {} -- Issue: lua table to csharp object will cause a lot of memory ??
function LuaCallCS:TestObject()
    self.csharpTestCase:TestObject(self.obj1)
end

-- Elapsed milliseconds about 0.0019
-- 0B GC
function LuaCallCS:TestOutRef()
    local a, b
    a, b = self.csharpTestCase:TestOutRef(a)
    -- Log.Debug("a "..tostring(a).."b "..tostring(b))
end

-- Elapsed milliseconds about 0.0019
-- 0B GC
LuaCallCS.obj2 = {}
function LuaCallCS:TestOverload()
    self.csharpTestCase:OverloadA(1)
    -- self.csharpTestCase:OverloadB(self.obj2)
end