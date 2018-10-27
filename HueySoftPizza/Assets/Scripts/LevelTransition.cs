using UnityEngine; // MonoBehavior
using System.Collections; // IEnumerator
using UnityEngine.UI; // Image

public class LevelTransition : MonoBehaviour
{
  // Fade Parameters
  const float fadeTime = 1.0f;
  const float minTransparency = 1.0f; // 1.0 = completely visible
  const float maxTransparency = 0.0f; // 0.0 = completely invisible

  // A black mask over the screen. Alpha is interpolated to fade the screen in and out.
  public GameObject screenFadeMask;

  /// <summary>
  /// Fades in screen on startup
  /// </summary>
  void Awake()
  {
    FadeScreenIn();
  }


  /// <summary>
  /// Fades the screen out over fadeTime
  /// </summary>
  public void FadeScreenOut()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    // Fade out the screen
    StartCoroutine(FadeOut(screenFadeMask));
  }


  /// <summary>
  /// Enumerator used in FadeScreenOut()
  /// </summary>
  /// <param name="fadedObject"></param>
  /// <returns></returns>
  IEnumerator FadeOut(GameObject fadedObject)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (1 - (f / fadeTime));

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<Image>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<Image>().color = tempColorHolder;

      // Advance to next frame
      yield return null;
    }
  }


  /// <summary>
  /// Fades the screen in over fadeTime
  /// </summary>
  public void FadeScreenIn()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 1f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    // Fade out the screen
    StartCoroutine(FadeIn(screenFadeMask));
  }


  /// <summary>
  /// Enumerator used in FadeScreenIn()
  /// </summary>
  /// <param name="fadedObject"></param>
  /// <returns></returns>
  IEnumerator FadeIn(GameObject fadedObject)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (f / fadeTime);

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<Image>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<Image>().color = tempColorHolder;

      // Advance to next frame
      yield return null;
    }
  }
}
