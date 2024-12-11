using UnityEngine;

namespace Framework.DataPersistence
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "SO/Data/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public int levelNumber;
        public int targetScore;
    }
}