using UnityEngine; // MonoBehavior
using UnityEngine.SceneManagement; // SceneManager
using System.Collections; // IEnumerator
using UnityEngine.UI; // Image

public class LevelTransition : MonoBehaviour
{
  // Fade Parameters
  const float fadeTime = 1.0f;
  const float minTransparency = 1.0f; // 1.0 = completely visible
  const float maxTransparency = 0.0f; // 0.0 = completely invisible

  // Optional action to take after fading out
  public enum PostFadeAction { Nothing, StartGame, QuitGame };

  // A black mask over the screen. Alpha is interpolated to fade the screen in and out.
  public GameObject screenFadeMask;

  //A flag to show that we are transitioning
  public bool isTransitioning;

  /// <summary>
  /// Fades in screen on startup
  /// </summary>
  void Awake()
  {
    FadeScreenIn();
  }


  /// <summary>
  /// Fades the screen out over fadeTime.
  /// </summary>
  public void FadeScreenOut()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    // Fade out the screen.
    StartCoroutine(FadeOut(screenFadeMask));
  }


  /// <summary>
  /// Fades the screen out over fadeTime, then starts the game.
  /// </summary>
  public void StartGame()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    // Fade out the screen, then start the game.
    StartCoroutine(FadeOut(screenFadeMask, PostFadeAction.StartGame));
  }


  /// <summary>
  /// Fades the screen out over fadeTime, then quits the game.
  /// </summary>
  public void QuitGame()
  {
    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    // Fade out the screen, then start the game.
    StartCoroutine(FadeOut(screenFadeMask, PostFadeAction.QuitGame));
  }


  /// <summary>
  /// Fades an object from minTransparency to maxTransparency
  /// Starts the game after fading out if action == StartGame.
  /// Quits the game after fading out if action == QuitGame.
  /// </summary>
  /// <param name="fadedObject">The object to fade out</param>
  /// <param name="action">The action to take after fading out.</param>
  /// <returns></returns>
  IEnumerator FadeOut(GameObject fadedObject, PostFadeAction action = PostFadeAction.Nothing)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      isTransitioning = true;
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (1 - (f / fadeTime));

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<Image>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<Image>().color = tempColorHolder;

      // Advance to next frame
      yield return null;
    }
    isTransitioning = false;

    // Start the game if screen is finished fading out and startGame == true
    if (action == PostFadeAction.StartGame)
    {
      SceneManager.LoadScene("Game");
      yield break;
    }

    else if (action == PostFadeAction.QuitGame)
    {
      Application.Quit();
      yield break;
    }

    else
    {
      yield break;
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
  /// Fades an object in from maxTransparency to minTransparency
  /// </summary>
  /// <param name="fadedObject">The object to fade in</param>
  /// <returns></returns>
  IEnumerator FadeIn(GameObject fadedObject)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      isTransitioning = true;
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (f / fadeTime);

      // Use a temporary Color object to set the new Color
      Color tempColorHolder = fadedObject.GetComponent<Image>().color;
      tempColorHolder.a = Mathf.Lerp(minTransparency, maxTransparency, interpolationValue);
      fadedObject.GetComponent<Image>().color = tempColorHolder;

      // Advance to next frame
      yield return null;
    }
    isTransitioning = false;
  }
}
