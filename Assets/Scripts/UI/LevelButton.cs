using UnityEngine;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        public int Level;
        public void Click() => SceneManager.LoadLevel(Level);
    }
}
