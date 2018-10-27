using UnityEngine; // MonoBehavior

public class LevelTransition : MonoBehaviour
{
  // Fade Parameters
  const float fadeTime = 1.0f;
  const float minTransparency = 1.0f; // 1.0 = completely visible
  const float maxTransparency = 0.0f; // 0.0 = completely invisible

  // A black mask over the screen. Alpha is interpolated to fade the screen in and out.
  GameObject screenFadeMask;
  

  /// <summary>
  /// Initializes variables
  /// </summary>
  void Start ()
  {
    // Fetch a pointer to the fade mask
    GameObject screenFadeMask = GameObject.Find("screenFadeMask");
  }


  /// <summary>
  /// Fades the screen out over fadeTime
  /// </summary>
  public void FadeScreenOut()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = GetComponent<SpriteRenderer>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<SpriteRenderer>().color = tempColorHolder;

    // Fade out the screen
    StartCoroutine(FadeOut(screenFadeMask));
  }


  /// <summary>
  /// Enumerator used in FadeScreenOut()
  /// </summary>
  /// <param name="fadedObject"></param>
  /// <returns></returns>
  IEnumerator FadeOut (GameObject fadedObject)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (f / fadeTime);

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<SpriteRenderer>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<SpriteRenderer>().color = tempColorHolder;

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
    Color tempColorHolder = GetComponent<SpriteRenderer>().color;
    tempColorHolder.a = 1f;
    screenFadeMask.GetComponent<SpriteRenderer>().color = tempColorHolder;

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
      float interpolationValue = (1 - (f / fadeTime));

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<SpriteRenderer>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<SpriteRenderer>().color = tempColorHolder;

      // Advance to next frame
      yield return null;
    }
  }
}
