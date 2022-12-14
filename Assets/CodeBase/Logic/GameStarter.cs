using CodeBase.Logic.EnemySpawners;
using CodeBase.UI;

namespace CodeBase.Logic
{
    public class GameStarter
    {
        private readonly Timer _timer;
        private readonly EnemySpawner _enemySpawner;
        private readonly WinWindowUIController _winWindow;
        private readonly LoseWindowUIController _loseWindow;
        private readonly StartGameButton _startGameButton;

        public GameStarter(Timer timer, EnemySpawner enemySpawner,WinWindowUIController winWindowUIController,
            LoseWindowUIController loseWindow, StartGameButton startGameButton)
        {
            _timer = timer;
            _enemySpawner = enemySpawner;
            _winWindow = winWindowUIController;
            _loseWindow = loseWindow;
            _startGameButton = startGameButton;
            Subscribe();
        }
        
        ~GameStarter()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _loseWindow.RestartButtonClicked += RestartGame;
            _winWindow.RestartButtonClicked += RestartGame;
            _startGameButton.StartButtonClicked += StartGame;
        }

        private void UnSubscribe()
        {
            _loseWindow.RestartButtonClicked -= RestartGame;
            _winWindow.RestartButtonClicked -= RestartGame;
            _startGameButton.StartButtonClicked -= StartGame;
        }

        private void RestartGame()
        {
            _enemySpawner.Reset();
            _timer.Reset();
            _winWindow.Hide();
            _loseWindow.Hide();
            _enemySpawner.ClickDetectorOn();
            StartGame();
        }

        private void StartGame()
        {
            _timer.WindUp();
            _enemySpawner.Spawn();
        }
    }
}