using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public Image bg1;
  public Image bg2;

  // Use this for initialization
  void Start()
  {
    if (File.Exists(Application.dataPath + "/ss.ss"))
    {
      bg2.gameObject.SetActive(true);
      bg1.gameObject.SetActive(false);
    }
    else
    {
      bg2.gameObject.SetActive(false);
      bg1.gameObject.SetActive(true);
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
