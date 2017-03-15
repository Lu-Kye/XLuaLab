Log = {}

function Log.Debug(format, ...)
    print(string.format(format, ...).."\n"..debug.traceback())
end

function Log.Warn(format, ...)
    print("<color=yellow>"..string.format(format, ...).."</color>\n"..debug.traceback())
end

function Log.Error(format, ...)
    print("<color=red>"..string.format(format, ...).."</color>\n"..debug.traceback())

    -- XLua override error, and will throw an exception so log error by printing red log.
    -- error(string.format(format, ...))
end