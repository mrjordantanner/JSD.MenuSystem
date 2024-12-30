using UnityEngine;
using UnityEngine.UI;

namespace JSD.MenuSystem
{
    public class MenuCloseButton : MonoBehaviour
    {
        private Button _closeButton;

        private void Awake()
        {
            _closeButton = GetComponent<Button>();
        }

        // On menu creation, bind this button to the menu's close Action
        public void Initialize(System.Action closeAction)
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveAllListeners();
                _closeButton.onClick.AddListener(() => closeAction.Invoke());
            }
            else
            {
                Debug.LogError("Close button is not assigned!");
            }
        }
    }
}