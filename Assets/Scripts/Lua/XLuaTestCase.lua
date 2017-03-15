XLuaTestCase = Class("XLuaTestCase", {})

XLuaTestCase.name = nil

XLuaTestCase.csharpTestCase = nil
XLuaTestCase.enableLog = false
XLuaTestCase.onlyTestCase = nil 
XLuaTestCase.repeatTimes = 1
XLuaTestCase.updatingTest = false

XLuaTestCase.testCases = nil
XLuaTestCase.currentTestCase = nil

XLuaTestCase.startTestTime = nil
XLuaTestCase.startMemory = nil
XLuaTestCase.endMemory = nil

function XLuaTestCase:ctor(csharpTestCase)
    self.name = csharpTestCase:GetType().Name
    self.csharpTestCase = csharpTestCase
    self.testCases = csharpTestCase.TestCases
end

function XLuaTestCase:Log(methodName, format, ...)
    if self.enableLog == false then
        return
    end
    Log.Debug(self.name.."::"..methodName..": "..string.format(format, ...))
end

function XLuaTestCase:LogElapsed(methodName, startTime)
    self:Log(methodName, "Elapsed milliseconds %f", (CS.UnityEngine.Time.realtimeSinceStartup - startTime) * 1000 / self.repeatTimes)
end

function XLuaTestCase:BeginTest()
    self.currentTestCase = nil
    if self.enableLog == false then
        return
    end
    self.startTestTime = CS.UnityEngine.Time.realtimeSinceStartup
end

function XLuaTestCase:EndTest()
    if self.enableLog == false then
        return
    end
    self:LogElapsed(self.currentTestCase, self.startTestTime)
end

function XLuaTestCase:Test()
    for i = 0, self.testCases.Length - 1 do
        self:BeginTest()
        self.startMemory = Memory.total()
        for j = 1, self.repeatTimes do
            if self.onlyTestCase ~= nil and self.onlyTestCase ~= '' then
                if self.onlyTestCase == self.testCases[i] then
                    self:DoTest(i)                    
                end
            else
                self:DoTest(i)
            end
        end
        self.endMemory = Memory.total()
        if self.currentTestCase ~= nil then
            Log.Debug(self.name.."::"..self.currentTestCase..": Offset memory %fB", (self.endMemory - self.startMemory) * 1024)
        end

        self:EndTest()
    end
    self:Log("Test", Memory.snapshot())
end

function XLuaTestCase:DoTest(i)
    self.currentTestCase = self.testCases[i]
    self[self.testCases[i]](self)
end

function XLuaTestCase:Update() 
    if self.updatingTest == false then
        return
    end
    self:Test()
end
