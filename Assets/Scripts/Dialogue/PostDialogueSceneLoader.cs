using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    public class PostDialogueSceneLoader : MonoBehaviour
    {
        public string Scene;
    
        // Start is called before the first frame update
        public void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        }
    }
}
