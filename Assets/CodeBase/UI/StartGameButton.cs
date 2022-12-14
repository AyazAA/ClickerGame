using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class StartGameButton : MonoBehaviour
    {
        public event Action StartButtonClicked;
        
        [SerializeField] private Button _startButton;

        private void Start() => 
            _startButton.onClick.AddListener(OnClicked);

        private void OnClicked()
        {
            StartButtonClicked?.Invoke();
            _startButton.gameObject.SetActive(false);
        }
    }
}