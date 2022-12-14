using System;
using System.Collections.Generic;
using CodeBase.Logic.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class LoseWindowUIController : MonoBehaviour
    {
        public event Action RestartButtonClicked;
        public event Action AdButtonClicked;
        
        [SerializeField] private LoadingWindow _loadingWindow;
        [SerializeField] private Image LoseWindowContainer;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            _adButton.onClick.AddListener(() => AdButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => RestartButtonClicked?.Invoke());
        }


        public void Show()
        {
            LoseWindowContainer.gameObject.SetActive(true);
            _loadingWindow.Show();
        }

        public void Hide()
        {
            LoseWindowContainer.gameObject.SetActive(false);
            _loadingWindow.Hide();
        }
    }
}