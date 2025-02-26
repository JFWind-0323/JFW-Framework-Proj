using Config;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class Player : MonoBehaviour
    {
        public PlayerData playerData;
        private Image image;
        private MeshRenderer meshRenderer;
        void Awake()
        {
            //image = GetComponent<Image>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        void Start()
        {
            meshRenderer.material= playerData.playerMaterial;
        }

 
    }
}
