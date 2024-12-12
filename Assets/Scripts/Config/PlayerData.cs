using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public string playerName;
        public Material playerMaterial;
        public Color color;
        public int speed;
        public int health;
        public int attack;
        public int defense;

    }
}

