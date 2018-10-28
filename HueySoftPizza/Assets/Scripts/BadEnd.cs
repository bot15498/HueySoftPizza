using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEnd : MonoBehaviour
{
  public Text TitleField;
  public Text DescField;

  private LevelTransition transitionManager;

  void Start()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
  }

  void Update()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
  }

  public void BackToMainMenu()
  {
    StartCoroutine(transitionManager.TransitionScene("MainMenu"));
  }
}
