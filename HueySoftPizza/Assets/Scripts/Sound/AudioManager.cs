using UnityEngine; // MonoBehavior
using UnityEngine.SceneManagement; //SceneManager

public class AudioManager : MonoBehaviour
{
  public AudioSource audioSource;
  public AudioClip buttonHoverSFX;
  public AudioClip sellSFX;
  public AudioClip passSFX;
  public AudioClip levelMusic;
  public AudioClip mainMenuMusic;
  public AudioClip clickSFX;

  // Use this for initialization
  void Start()
  {
    string currScene = SceneManager.GetActiveScene().name;
    if (currScene == "MainMenu")
      PlayMenuMusic();
    else
      PlayLevelMusic();
  }

  // Update is called once per frame
  void Update()
  {

  }


  void Play(AudioClip clip, float db, bool loop = false)
  {
    audioSource.clip = clip;
    audioSource.volume = dBToGain(db);
    audioSource.loop = loop;

    audioSource.Play();
  }

  static public float dBToGain(float dB)
  {
    float gain = 10f;
    float ratio = dB / 20f;
    gain = Mathf.Pow(gain, ratio);

    return gain;
  }

  public void PlayButtonHoverSFX()
  {
    Play(buttonHoverSFX, -10f);
  }

  public void PlaySellSFX()
  {
    Play(sellSFX, 0f);
  }

  public void PlayPassSFX()
  {
    Play(passSFX, -2f);
  }

  public void PlayLevelMusic()
  {
    Play(levelMusic, -16f, true);
  }

  public void PlayMenuMusic()
  {
    Play(mainMenuMusic, -18f, true);
  }

  public void PlayClickSFX()
  {
    Play(clickSFX, -3f);
  }
}
