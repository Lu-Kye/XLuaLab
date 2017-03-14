-- @filepath: Start from LuaRootPath without file extension(.lua)
function include(filepath)
    CS.XLuaManager.Instance:LoadFile(filepath) 
end

include("Class")
include("Log")
include("XLuaTestCase")

function Start()
    Log.Debug("Start")
end

function Update()
    -- print("Update")
end

function OnDestroy()
    Log.Debug("OnDestroy")
end