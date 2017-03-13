-- @filepath: Start from LuaRootPath without file extension(.lua)
function include(filepath)
    CS.XLuaManager.Instance:LoadFile(filepath) 
end

include("Class")
include("Log")

function Start()
    print("Start")
end

function Update()
    -- print("Update")
end

function OnDestroy()
    print("OnDestroy")
end