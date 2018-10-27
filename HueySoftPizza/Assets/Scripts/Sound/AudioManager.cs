using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public AudioSource audioSource;
  public AudioClip buttonHoverSFX;
  public AudioClip sellSFX;
  public AudioClip passSFX;
  public AudioClip levelMusic;
  public AudioClip mainMenuMusic;

  // Use this for initialization
  void Start()
  {
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
    Play(buttonHoverSFX, -6f);
  }

  public void PlaySellSFX()
  {
    Play(sellSFX, -6f);
  }

  public void PlayPassSFX()
  {
    Play(passSFX, -6f);
  }

  public void PlayLevelMusic()
  {
    Play(levelMusic, -21f, true);
  }

  public void PlayMenuMusic()
  {
    Play(mainMenuMusic, -21f, true);
  }
}
