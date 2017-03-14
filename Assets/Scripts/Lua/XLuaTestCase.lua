XLuaTestCase = Class("XLuaTestCase", {})

XLuaTestCase.enableLog = false
XLuaTestCase.name = nil
XLuaTestCase.csharpTestCase = nil
XLuaTestCase.startTestTime = nil

function XLuaTestCase:ctor(csharpTestCase)
    self.name = csharpTestCase:GetType().Name
    self.csharpTestCase = csharpTestCase
end

function XLuaTestCase:Log(methodName, format, ...)
    if self.enableLog == false then
        return
    end
    Log.Debug(self.name.."::"..methodName..": "..string.format(format, ...))
end

function XLuaTestCase:LogElapsed(methodName, startTime)
    self:Log(methodName, "Elapsed milliseconds %f", (CS.UnityEngine.Time.realtimeSinceStartup - startTime) * 1000)
end

function XLuaTestCase:BeginTest()
    if self.enableLog == false then
        return
    end
    self.startTestTime = CS.UnityEngine.Time.realtimeSinceStartup
end

function XLuaTestCase:EndTest()
    if self.enableLog == false then
        return
    end
    self:LogElapsed(self.csharpTestCase.CurrentTestCase, self.startTestTime)
end
