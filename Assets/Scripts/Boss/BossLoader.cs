using UnityEngine;
using UnityEngine.Events;

namespace Boss
{
    public class BossLoader : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private UnityEvent callback;
    
        public void LoadBoss()
        {
            SceneManager.LoadBossScene(bossPrefab, () =>
            {
                Debug.Log("Boss callback");
                callback.Invoke();
            });
        }
    }
}
