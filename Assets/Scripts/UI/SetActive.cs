using UnityEngine;

namespace UI
{
    public class SetActive : MonoBehaviour
    {
        public GameObject[] Active;
        public GameObject[] Inactive;
        public GameObject[] Toggle;

        public void Execute()
        {
            foreach (var active in Active) active.SetActive(true);
            foreach (var inactive in Inactive) inactive.SetActive(false);
            foreach (var toggle in Toggle) toggle.SetActive(!toggle.activeSelf);
        }
    }
}
