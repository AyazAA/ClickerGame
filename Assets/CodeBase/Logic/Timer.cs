using System;
using System.Collections;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Timer : MonoBehaviour
    {
        public event Action TimeEnded;
        private TimerUI _timerUI;
        private Coroutine _timerCoroutine;
        private float _startTime = 20;
        private float _remainingTime = 20;
        private float _rewardTime = 7;
        private float _spentTime = 0;

        public float SpentTime => _spentTime;

        public void Construct(TimerUI timerUI, float startTime, float rewardTime)
        {
            _timerUI = timerUI;
            _startTime = startTime;
            _remainingTime = startTime;
            _rewardTime = rewardTime;
        }

        public void WindUp() => 
            _timerCoroutine = StartCoroutine(StartTimer(_startTime));

        public void Stop() => 
            StopCoroutine(_timerCoroutine);


        public void AddTime()
        {
            _remainingTime = _rewardTime;
            _timerCoroutine = StartCoroutine(StartTimer(_rewardTime));
        }
        
        private IEnumerator StartTimer(float time)
        {
            while (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                _spentTime += Time.deltaTime;
                _timerUI.SetValue(_remainingTime,time);
                yield return null;
            }
            TimeEnded?.Invoke();
        }

        public void Reset()
        {
            _remainingTime = _startTime;
            _spentTime = 0;
        }
    }
}