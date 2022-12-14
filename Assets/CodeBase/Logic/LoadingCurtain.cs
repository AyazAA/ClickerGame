using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      WaitForSeconds waitForSeconds = new WaitForSeconds(0.03f);
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return waitForSeconds;
      }
      
      gameObject.SetActive(false);
    }
  }
}