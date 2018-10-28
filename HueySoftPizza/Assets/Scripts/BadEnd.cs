using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BadEnd : MonoBehaviour
{
  public Text TitleField;
  public Text DescField;
  public List<string> Titles;
  public List<string> Descriptions;

  private LevelTransition transitionManager;
  private PlayerInfo playerInfo;

  void Start()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
    Titles = new List<string>();
    Descriptions = new List<string>();
    LoadEndGameText();
  }

  void Update()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
    if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
      UpdateText(playerInfo.currState);
    }
  }

  public void BackToMainMenu()
  {
    StartCoroutine(transitionManager.TransitionScene("MainMenu"));
  }

  private void UpdateText(EndStates state)
  {
    TitleField.text = Titles[(int)state];
    DescField.text = Descriptions[(int)state];
  }

  private void LoadEndGameText()
  {
    string line;
    Titles.Add("Dummy None Placeholder");
    //Titles
    StreamReader reader = new StreamReader("Assets/Stories/BadEndTitles.txt", Encoding.GetEncoding(1252));
    while ((line = reader.ReadLine()) != null)
    {
      Titles.Add(line);
    }
    reader.Close();
    //Descriptions
    Descriptions.Add("Dummy None Placeholder");
    reader = new StreamReader("Assets/Stories/BadEndDesc.txt", Encoding.GetEncoding(1252));
    while ((line = reader.ReadLine()) != null)
    {
      Descriptions.Add(line);
    }
    reader.Close();
  }
}
