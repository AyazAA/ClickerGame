using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
    public class LoadingWindow : MonoBehaviour
    {
        public CanvasGroup Window;

        public void Show() => 
            StartCoroutine(DoFadeIn());

        public void Hide() => 
            Window.alpha = 0;
    
        private IEnumerator DoFadeIn()
        {
            while (Window.alpha < 1)
            {
                Window.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}