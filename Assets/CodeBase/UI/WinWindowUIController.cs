using System;
using System.Collections.Generic;
using CodeBase.Logic.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class WinWindowUIController : MonoBehaviour
    {
        public event Action RestartButtonClicked;

        [SerializeField] private LoadingWindow _loadingWindow;
        [SerializeField] private Image WinWindowContainer;
        [SerializeField] private TextMeshProUGUI _result;
        [SerializeField] private TextMeshProUGUI _record;
        [SerializeField] private TextMeshProUGUI _leaderboard;
        [SerializeField] private Button _restartButton;

        private void Start() => 
            _restartButton.onClick.AddListener(()=>RestartButtonClicked?.Invoke());

        public void SetData(int result, int record, List<PlayerStat> leaders)
        {
            _result.text = $"New time: {result}";
            _record.text = $"Record time: {record}";

            _leaderboard.text = "Leaderboard:";
            
            if (leaders != null)
                foreach (PlayerStat player in leaders)
                    _leaderboard.text += $"\n{player.name} : {player.score}";
            else
                _leaderboard.text += $"\nYou : {record}";
        }

        public void Show()
        {
            WinWindowContainer.gameObject.SetActive(true);
            _loadingWindow.Show();
        }

        public void Hide()
        {
            WinWindowContainer.gameObject.SetActive(false);
            _loadingWindow.Hide();
        }
    }
}