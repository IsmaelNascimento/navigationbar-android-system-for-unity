using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IsmaelNascimentoAssets
{
    public class ScreenManager : MonoBehaviour
    {
        #region VARIABLES

        public static ScreenManager Instance;

        private List<Screen> screens = new List<Screen>();
        private Screen screenCurrentActived;
        private List<string> fluxScreen = new List<string>();
        private int countTapButtonBack;
        private int indexFluxScreenCurrent;
        private int countTapExitApp = 2;
        private float timeResetTapExitApp = 0.5f;
        /// <summary>
        /// The on screen disabled.
        /// </summary>
        public static Action<Screen> OnScreenDisabled;

        #endregion

        #region METHODS_MONOBEHAVIOUR

        private void Awake()
        {
            Instance = this;
            screens = Resources.FindObjectsOfTypeAll(typeof(Screen)).OfType<Screen>().ToList();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnButtonBackNativeClick();
        }

        #endregion

        #region METHODS_AUX

        /// <summary>
        /// Adds the flux screen.
        /// </summary>
        /// <param name="screen">Screen.</param>
        public void AddFluxScreen(Screen screen)
        {
            if (!fluxScreen.Contains(screen.GetId()))
                fluxScreen.Add(screen.GetId());

            screenCurrentActived = screen;
        }

        public void OnButtonBackNativeClick()
        {
            Debug.Log("OnButtonBackNativeClick");

            if (fluxScreen.Count == 1)
            {
                countTapButtonBack++;
                StartCoroutine(ExitAppOnButtonBackNativeClick_Coroutine());
                return;
            }

            if (OnScreenDisabled != null)
                OnScreenDisabled(screenCurrentActived);

            screenCurrentActived.gameObject.SetActive(false);
            fluxScreen.Remove(screenCurrentActived.GetId());
            indexFluxScreenCurrent = fluxScreen.Count - 1;
            screenCurrentActived = screens.Find(screen => screen.GetId() == fluxScreen[indexFluxScreenCurrent]);
            screenCurrentActived.gameObject.SetActive(true);
        }

        #endregion

        #region COROUTINES

        private IEnumerator ExitAppOnButtonBackNativeClick_Coroutine()
        {
            if (countTapButtonBack == countTapExitApp && (screenCurrentActived.GetId() == fluxScreen[indexFluxScreenCurrent]))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            yield return new WaitForSeconds(timeResetTapExitApp);
            countTapButtonBack = 0;
        }

        #endregion

        #region METHODS_GET

        /// <summary>
        /// Gets all screens.
        /// </summary>
        /// <returns>The all screens.</returns>
        public List<Screen> GetAllScreens()
        {
            return screens;
        }

        /// <summary>
        /// Gets the screen actived.
        /// </summary>
        /// <returns>The screen actived.</returns>
        public Screen GetScreenActived()
        {
            return screenCurrentActived;
        }

        #endregion
    }
}