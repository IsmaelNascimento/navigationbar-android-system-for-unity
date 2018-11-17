using UnityEngine;

namespace IsmaelNascimentoAssets
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private GameObject panelNavigationBar;

        private void Start()
        {
            ScreenManager.OnScreenDisabled += ScreenDisabled;
#if UNITY_ANDROID && !UNITY_EDITOR
            panelNavigationBar.SetActive(false);
#endif
            Debug.Log("Total screens:: " + ScreenManager.Instance.GetAllScreens().Count);
            foreach (var screen in ScreenManager.Instance.GetAllScreens())
                Debug.Log("Name Screen:: " + screen.GetName());
        }

        private void ScreenDisabled(Screen screen)
        {
            Debug.Log("Name Screen Disabled:: " + screen.GetName());
            Invoke("PrintNameScreenActived", 0.5f);
        }

        private void PrintNameScreenActived()
        {
            Debug.Log("Name Screen Actived:: " + ScreenManager.Instance.GetScreenActived().GetName());
        }
    }
}