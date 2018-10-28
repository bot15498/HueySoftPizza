using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class End : MonoBehaviour
{
  private LevelTransition transitionManager;

  void Start()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }

    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.dataPath + "/ss.ss");
    Data saveFile = new Data();
    //write saveFile to file
    bf.Serialize(file, saveFile);
    file.Close();
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
