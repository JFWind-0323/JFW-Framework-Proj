using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "GameData", menuName = "SO/Data/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public float timeScale = 1f;
        public bool autoSave = true;
        public int autoSaveTimeInterval = 10;
        public List<PlayerData> playerData;
        public List<LevelData> levels = new List<LevelData>();

    }
}
