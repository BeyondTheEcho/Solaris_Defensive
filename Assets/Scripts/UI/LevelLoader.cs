using UnityEngine;

namespace UI
{
    public class LevelLoader : MonoBehaviour
    {
        public int Level;
        public void LoadLevel() => SceneManager.LoadLevel(Level);
    }
}
