CSCallLua = Class("CSCallLua", {})

function CSCallLua.TestNumber(num)
    -- Log.Debug(tostring(num))
end

function CSCallLua.TestObject(obj)
    -- Log.Debug(tostring(obj))
end

-- function CSCallLua:ctor()
-- end
-- function CSCallLua:TestAll()
--     Log.Debug("TestAll")
--     self:TestCoroutine()
-- end
-- function CSCallLua:TestCoroutine()
--     -- local util = require 'xlua.util'
--     local co = coroutine.create(function () 
--         print("coroutine start")

--         -- Log.Debug("TestCoroutine "..CS.Time.realtimeSinceStartup)
--         -- coroutine.yield(CS.UnityEngine.WaitForSeconds(2))
--         -- Log.Debug("TestCoroutine "..CS.Time.realtimeSinceStartup)
--     end)
--     coroutine.resume(co)
-- end
-- CSCallLua.Instance = CSCallLua.New()
