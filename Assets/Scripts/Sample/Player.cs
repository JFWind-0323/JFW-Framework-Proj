using Config;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sample
{
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("playerData")] public PlayerConfig playerConfig;
        private Image image;
        private MeshRenderer meshRenderer;
        void Awake()
        {
            //image = GetComponent<Image>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        void Start()
        {
            meshRenderer.material= playerConfig.playerMaterial;
        }

 
    }
}
