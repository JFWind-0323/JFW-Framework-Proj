using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "SO/Data/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public string levelName;
        public int levelNumber;
        public int targetScore;
    }
}
