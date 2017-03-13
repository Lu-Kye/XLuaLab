Log = {}

function Log.Debug(msg)
    print(msg.."\n"..debug.traceback())
end

function Log.Warn(msg)
    print("<color=yellow>"..msg.."</color>\n"..debug.traceback())
end

function Log.Error(msg)
    print("<color=red>"..msg.."</color>\n"..debug.traceback())

    -- XLua override error, and will throw an exception so log error by printing red log.
    -- error(msg)
end