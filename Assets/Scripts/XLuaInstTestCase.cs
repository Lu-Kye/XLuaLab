using XLua;

namespace XLuaExamples
{
    [LuaCallCSharp]
    public class XLuaInstTestCase<T> : XLuaTestCase<T>
        where T : XLuaInstTestCase<T>
    {
        protected LuaFunction _instFunc;
        protected virtual bool LuaEnableLog
        {
            get 
            {
                return _luaTable.Get<bool>("enableLog");
            }
            set
            {
                _luaTable.Set<string, bool>("enableLog", value);
            }
        }

        protected override void Setup()
        {
            base.Setup();
            _instFunc = _luaTable.Get<LuaFunction>("New");
            _luaTable = _instFunc.Call(this, typeof(LuaTable))[0] as LuaTable;
        } 

        protected override void Update()
        {
            if (this.LuaEnableLog != this.EnableLog)
                this.LuaEnableLog = this.EnableLog;

            base.Update();
        }
    }
}