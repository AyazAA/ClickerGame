using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Web;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Logic
{
    public class GameOver
    {
        private const string RecordTimeKey = "RecordTime";
        private readonly Timer _timer;
        private readonly EnemySpawner _enemySpawner;
        private readonly LeaderboardNetwork _leaderboard;
        private readonly WinWindowUIController _winWindow;
        private readonly LoseWindowUIController _loseWindow;

        public GameOver(Timer timer, EnemySpawner enemySpawner,
            LeaderboardNetwork leaderboard, WinWindowUIController winWindowUIController,
            LoseWindowUIController loseWindow)
        {
            _timer = timer;
            _enemySpawner = enemySpawner;
            _leaderboard = leaderboard;
            _winWindow = winWindowUIController;
            _loseWindow = loseWindow;
            Subscribe();
        }
        
        ~GameOver()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _timer.TimeEnded += OnTimeEnded;
            _enemySpawner.AllMonstersDied += OnAllMonsterDied;
        }

        private void UnSubscribe()
        {
            _timer.TimeEnded -= OnTimeEnded;
            _enemySpawner.AllMonstersDied -= OnAllMonsterDied;
        }

        private void OnTimeEnded()
        {
            _timer.Stop();
            _enemySpawner.ClickDetectorOff();
            _loseWindow.Show();
        }

        private void OnAllMonsterDied()
        {
            _timer.Stop();
            SetDataToWinWindow();
        }

        private void SetDataToWinWindow()
        {
            int time = (int)(_timer.SpentTime);

            int record = PlayerPrefs.GetInt(RecordTimeKey);

            if (record == 0 || time < record)
            {
                PlayerPrefs.SetInt(RecordTimeKey, time);
                record = time;
            }

            List<PlayerStat> newLeaderboard = null;
            PlayerStat playerStat = null;
            if (_leaderboard.GetPlayers != null)
            {
                playerStat = new PlayerStat { name = "You", score = record };
                _leaderboard.GetPlayers.PlayerStats.Add(playerStat);
                newLeaderboard = _leaderboard.GetPlayers.PlayerStats.OrderBy(stat => stat.score).ToList();
            }

            _winWindow.SetData(time, record, newLeaderboard);
            _winWindow.Show();

            if (_leaderboard.GetPlayers != null)
                _leaderboard.GetPlayers.PlayerStats.Remove(playerStat);
        }
    }
}