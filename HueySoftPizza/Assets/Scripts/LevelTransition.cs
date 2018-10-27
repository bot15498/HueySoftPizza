using UnityEngine; // MonoBehavior

public class LevelTransition : MonoBehaviour
{
  // Fade Parameters
  const float fadeTime = 1.0f;
  const float minTransparency = 1.0f; // 1.0 = completely visible
  const float maxTransparency = 0.0f; // 0.0 = completely invisible

  // A black mask over the screen. Alpha is interpolated to fade the screen in and out.
  GameObject screenFadeMask;
  
  void Start ()
  {
    // Fetch a pointer to the fade mask
    GameObject screenFadeMask = GameObject.Find("screenFadeMask");
  }

  public void FadeScreenOut()
  {
    StartCoroutine(FadeOut(screenFadeMask));
  }

  IEnumerator FadeOut (GameObject fadedObject)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = maxTransparency; f <= fadeTime; f += Time.deltaTime)
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
}
