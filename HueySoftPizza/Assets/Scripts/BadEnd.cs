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
    Titles.Add("Dummy None Placeholder");
    Descriptions.Add("Dummy None Placeholder");
    Titles.Add("Your Money Ran Out!");
    Titles.Add("Your Family Ran Out of Food");
    Titles.Add("You Failed to Pay Housing Costs");
    Titles.Add("You Didn't Pay Taxes for the Day");
    Titles.Add("Data Breach");
    Titles.Add("Your Kid Dies of Polio");
    Titles.Add("You Made Too Many Mistakes");
    Titles.Add("You Conspired With The Enemy.");
    Descriptions.Add("Due to poor financial decisions, you're broke. In fact you're in debt. How did you manage that? Your family can't depend on a breadloser, you loser.");
    Descriptions.Add("While living the lavish life of spending money on not food somewhere along the way you somehow convinced yourself that food was no longer a necessity, the sudden realization causes you to look at yourself and you realize you are nothing but skin and bones, a walking skeleton, the fantasy of wellness has been shattered as well as the dream of living a normal life. You're a skeleton now you can't go to work like this, it's embarrassing!  ");
    Descriptions.Add("Due to your unwillingness to pay your own rent, your cold and unfeeling robot of a landlord physically throws you out of your home leaving you only with the clothes on your back and the money in your wallet. With your lack of housing, you turn to living on the streets, a new life of vagrancy is in store, with new horrible experiences on the horizon, explore and discover what the world of homelessness has in store for you. Get out there killer!");
    Descriptions.Add("Either because of your lack of money or lack of brain cells to comprehend the importance of taxes, no matter the case, the wolves are at your door and by wolves I mean tax collectors. Since you can't pay your taxes with money, they decided that you'll have to pay with something else, specifically your right arm! Get used to living the rest of your life being monoplegic, oh and you die of blood loss. What? You didn't expect them to send you to a hospital or something right? Those medical bills are expensive you know.");
    Descriptions.Add("Data Breach");
    Descriptions.Add("Because you refused to buy medicine for your son, your son died. Your neighbor soon found out and told the police about your negligence. You were convicted for the destruction of your child and sentenced to life in prison.");
    Descriptions.Add("A leakage of classified information has empowered international radical terrorist groups to seize power in your nation resulting in them taking control of all the world's freedom. You're dead, your family is dead, your friends are dead, all the pets are dead and the world hates you.");
    Descriptions.Add("Your supervisor took notice of your inability to perform simple tasks and with the approval from the higher ups at the company, has saw fit to relieve you of your position PERMANENTLY. You'll never see your co-worker, desk, or even your family again! No one knows where you were taken, so you were never killed, rather your official status is that you were erased.");

    //LoadEndGameText();
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
