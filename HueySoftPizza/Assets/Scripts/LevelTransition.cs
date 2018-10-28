using UnityEngine; // MonoBehavior
using UnityEngine.SceneManagement; // SceneManager
using System.Collections; // IEnumerator
using UnityEngine.UI; // Image

public class LevelTransition : MonoBehaviour
{
  // AudioSource used for fade outs
  public AudioSource audioSource;

  // Fade Parameters
  const float fadeTime = 1.0f;
  const float fadeTime2 = 5.0f;
  const float minTransparency = 1.0f; // 1.0 = completely visible
  const float maxTransparency = 0.0f; // 0.0 = completely invisible
  const string level1 = "Day1";

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

  public IEnumerator TransitionScene(string newSceneName)
  {
    screenFadeMask.GetComponent<Image>().raycastTarget = true;
    isTransitioning = true;
    FadeScreenOut();
    while (isTransitioning)
    {
      yield return true;
    }
    SceneManager.LoadScene(newSceneName);
  }

  public IEnumerator ScaryFade(string newSceneName)
  {
    screenFadeMask.GetComponent<Image>().raycastTarget = true;
    isTransitioning = true;

    // Reset the alpha of screenFadeMask
    Color tempColorHolder = screenFadeMask.GetComponent<Image>().color;
    tempColorHolder.a = 0f;
    screenFadeMask.GetComponent<Image>().color = tempColorHolder;

    StartCoroutine(FadeOutScary(screenFadeMask));

    while (isTransitioning)
    {
      yield return true;
    }
    SceneManager.LoadScene(newSceneName);
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

    // Fade out music
    FadeOutVol();
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

    // Fade out music
    FadeOutVol();
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
      SceneManager.LoadScene(level1);
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

  IEnumerator FadeOutScary(GameObject fadedObject, PostFadeAction action = PostFadeAction.Nothing)
  {
    // Decrement alpha over time so that it reaches maxTransparency after fadeTime
    for (float f = 0; f <= fadeTime2; f += Time.deltaTime)
    {
      isTransitioning = true;
      // Find the percent to interpolate between max transparency and min transparency
      float interpolationValue = (1 - (f / fadeTime2));

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
      SceneManager.LoadScene(level1);
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

  /// <summary>
  /// Fade out the volume of the AudioSource of the AudioManager attached to this TransitionManager
  /// </summary>
  public void FadeOutVol()
  {
    // Find the volume before it fades out
    float currentVol = audioSource.volume;

    // Fade it out
    StartCoroutine(FadeOutV(currentVol));
  }

  IEnumerator FadeOutV(float startVol)
  {
    for (float f = 0; f <= fadeTime; f += Time.deltaTime)
    {
      // Find the percent to interpolate between max volume and min volume
      float interpolationValue = (1 - (f / fadeTime));

      // Set the new volume
      audioSource.volume = Mathf.Lerp(0f, startVol, interpolationValue);

      // Advance to next frame
      yield return null;
    }
  }
}
