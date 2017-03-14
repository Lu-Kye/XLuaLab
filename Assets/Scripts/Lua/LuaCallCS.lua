LuaCallCS = Class("LuaCallCS", XLuaTestCase) 

-- 0B GC
-- Elapsed milliseconds about 0.015
function LuaCallCS:TestNumber()
    self:BeginTest()
    self.csharpTestCase:DoTestNumber(10);
    self:EndTest()
end

-- If _obj is lua object will get gc
-- If _obj is csharp object will not get gc
-- Elapsed milliseconds about 0.007
function LuaCallCS:TestObject()
    self:BeginTest()
    self.csharpTestCase:DoTestObject(self.csharpTestCase);
    self:EndTest()
end