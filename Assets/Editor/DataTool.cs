using DataPersistence;
using UnityEditor;

namespace Editor
{
    public static class DataTool
    {
        [MenuItem("Tools/Save Data")]
        public static void SaveData()
        {
            DataMgr.Instance.SaveData(1);
        }

        [MenuItem("Tools/Load Data")]
        public static void LoadData()
        {
            DataMgr.Instance.LoadData(1);
        }
    }
}