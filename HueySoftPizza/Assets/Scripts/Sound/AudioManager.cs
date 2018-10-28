using UnityEngine; // MonoBehavior
using UnityEngine.SceneManagement; //SceneManager

public class AudioManager : MonoBehaviour
{
  public AudioSource audioSource;
  public AudioSource musicSource;
  public AudioClip buttonHoverSFX;
  public AudioClip sellSFX;
  public AudioClip passSFX;
  public AudioClip levelMusic;
  public AudioClip mainMenuMusic;
  public AudioClip clickSFX;
  public AudioClip scarySFX1;
  public AudioClip scarySFX2;

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

  public void PlayScarySFX1()
  {
    Play(scarySFX1, -6f);
  }

  public void PlayScarySFX2()
  {
    Play(scarySFX2, 0f);
    StopMusic();
  }

  public void StopMusic()
  {
    musicSource.volume = 0f;
  }
}
