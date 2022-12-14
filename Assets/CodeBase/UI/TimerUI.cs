using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private Image _slider;

        public void SetValue(float current, float max)
        {
            _slider.fillAmount = current / max;
            _time.text = ((int)current).ToString();
        }
    }
}