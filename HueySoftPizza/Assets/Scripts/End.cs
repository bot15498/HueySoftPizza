using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
  private LevelTransition transitionManager;

  void Start()
  {
    if(transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
  }

  // Update is called once per frame
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
