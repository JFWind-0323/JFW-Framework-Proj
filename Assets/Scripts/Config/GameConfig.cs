using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "SO/Data/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public float timeScale = 1f;
        public bool autoSave = true;
        public int autoSaveTimeInterval = 10;
        public List<PlayerConfig> playerData;
        public List<LevelConfig> levels = new List<LevelConfig>();

    }
}
