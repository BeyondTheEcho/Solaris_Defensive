using UnityEngine;

namespace Dialogue
{
    public class DialogueTest : MonoBehaviour
    {
        public DialogueNode Node;
    
        void Start()
        {
            SceneManager.LoadDialogueScene(Node, () => UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueTest"));
        }
    }
}
