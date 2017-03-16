using XLua;

namespace XLuaExamples
{
    [LuaCallCSharp]
    public class XLuaTestCaseForLua<T> : XLuaTestCase<T>
        where T : XLuaTestCaseForLua<T>
    {
        protected LuaFunction _instFunc;
        protected LuaFunction _testFunc;
        protected LuaFunction _updateFunc;

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

        protected virtual string LuaOnlyTestCase
        {
            get
            {
                return _luaTable.Get<string>("onlyTestCase");
            }
            set
            {
                _luaTable.Set<string, string>("onlyTestCase", value);
            }
        }

        protected virtual uint LuaRepeatTimes
        {
            get 
            {
                return _luaTable.Get<uint>("repeatTimes");
            }
            set
            {
                _luaTable.Set<string, uint>("repeatTimes", value);
            }
        }

        protected virtual bool LuaUpdatingTest
        {
            get 
            {
                return _luaTable.Get<bool>("updatingTest");
            }
            set
            {
                _luaTable.Set<string, bool>("updatingTest", value);
            }
        }

        public bool ShowMemory = false;
        protected virtual bool LuaShowMemory
        {
            get 
            {
                return _luaTable.Get<bool>("showMemory");
            }
            set
            {
                _luaTable.Set<string, bool>("showMemory", value);
            }
        }

        protected override void Setup()
        {
            base.Setup();
            _instFunc = _luaTable.Get<LuaFunction>("New");
            _luaTable = _instFunc.Call(this, typeof(LuaTable))[0] as LuaTable;

            _testFunc = _luaTable.Get<LuaFunction>("Test");
            _updateFunc = _luaTable.Get<LuaFunction>("Update");
        } 

        protected override void Test()
        {
            _testFunc.Call(_luaTable, null);
        }

        protected override void Update()
        {
            if (_luaTable == null)
                return;

            if (this.LuaEnableLog != this.EnableLog)
                this.LuaEnableLog = this.EnableLog;

            if (this.LuaRepeatTimes != this.RepeatTimes)
                this.LuaRepeatTimes = this.RepeatTimes;

            if (this.LuaOnlyTestCase != this.OnlyTestCase)
                this.LuaOnlyTestCase = this.OnlyTestCase;

            if (this.LuaUpdatingTest != this.UpdatingTest)
                this.LuaUpdatingTest = this.UpdatingTest;
            
            if (this.LuaShowMemory != this.ShowMemory)
                this.LuaShowMemory = this.ShowMemory;
        }

        protected virtual void LateUpdate()
        {
            if (this.UpdatingTest == false)
                return;

            if (_updateFunc == null)
                return;

            _updateFunc.Call(_luaTable);
        }

        protected override void Unsetup()
        {
            _instFunc = null;
            _testFunc = null;
            _updateFunc = null;
            base.Unsetup();
        }
    }
}