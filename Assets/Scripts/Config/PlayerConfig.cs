using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SO/Data/Player Data", order = 0)]
    public class PlayerConfig : ScriptableObject
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

