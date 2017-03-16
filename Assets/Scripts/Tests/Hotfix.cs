using UnityEngine;

namespace XLuaExamples
{
    [XLua.Hotfix]
    public class Hotfix : XLuaTestCaseForLua<Hotfix>
    {
        [ContextMenu("TestDoHotfix")]
        public void DoHotfix()
        {
            Debug.Log("CSharp DoHotfix");
        }
    }
}