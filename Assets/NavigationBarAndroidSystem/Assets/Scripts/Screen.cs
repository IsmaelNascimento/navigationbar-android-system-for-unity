using System;
using UnityEngine;

namespace IsmaelNascimentoAssets
{
    public class Screen : MonoBehaviour
    {
        #region VARIABLES

        private string id;
        [Tooltip("Optional")] [SerializeField] private string nameScreen;

        #endregion

        #region METHODS_MONOBEHAVIOUR

        private void Awake()
        {
            id = Guid.NewGuid().ToString();
            nameScreen = string.IsNullOrEmpty(nameScreen) ? gameObject.name : nameScreen;
        }

        private void OnEnable()
        {
            ScreenManager.Instance.AddFluxScreen(this);
        }

        #endregion

        #region METHODS_GET

        public string GetId()
        {
            return id;
        }

        public string GetName()
        {
            return nameScreen;
        }

        #endregion
    }
}