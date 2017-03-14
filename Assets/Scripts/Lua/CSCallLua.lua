CSCallLua = Class("CSCallLua", {})

CSCallLua.num = 10
CSCallLua.obj = {}
CSCallLua.classA = { a = 1 }
CSCallLua.structA = { a = 1 }
CSCallLua.listA = { 1 }
CSCallLua.dictA = { a = 1 }

function CSCallLua.TestNumber(num)
    -- Log.Debug(tostring(num))
end

function CSCallLua.TestObject(obj)
    -- Log.Debug(tostring(obj))
end

function CSCallLua.TestStruct(struct)
    -- Log.Debug(tostring(struct.c))
end
