﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Day2 : MonoBehaviour
{
  public List<Image> TutorialPages;
  public Canvas TutorialCanvas;
  public Canvas CostCanvas;
  public Canvas MainGameCanvas;
  public LevelTransition transitionManager;
  public int taxCostPerDay = 2;
  public int foodCostPerDay = 2;
  public int houseCostPerDay = 1;
  public Text foodCostField;
  public Text houseCostField;
  public Text TaxCostField;
  public Text TotalField;
  public GameButton gameButton;

  [SerializeField]
  private int currPage;
  [SerializeField]
  private bool inTutorial;
  [SerializeField]
  private bool onCostPage;
  private PlayerInfo playerInfo;
  private GameOverChecker gameOverCheck;

  public string todaysNewspaper; // path to txt file
  private string newsTitleText;
  private string leftColumnTitle;
  private string leftColumnBodyText;
  private bool leftColumnLong = true;
  public Sprite leftColumnImage;
  private string rightColumnTitle;
  private string rightColumnBodyText;
  private bool rightColumnLong = false;
  public Sprite rightColumnImage;

  public string todaysEmail; // path to txt file
  public Sprite senderImage;
  private string senderName;
  private string subject;
  private string emailBody;

  public GameObject newspaperCanvas;
  public GameObject newsTitle;
  public GameObject leftTitle;
  public GameObject leftLongBody;
  public GameObject leftShortBody;
  public GameObject leftImageObj; // only shown if left column length is set to short
  public GameObject rightTitle;
  public GameObject rightLongBody;
  public GameObject rightShortBody;
  public GameObject rightImageObj; // only shown if right column length is set to short

  public GameObject emailCanvas;
  public GameObject emailImageObj;
  public GameObject emailSenderText;
  public GameObject emailSubjectText;
  public GameObject emailBodyText;

  // Daily Startup Routine: Newspaper, then game
  public enum GameState
  {
    Newspaper,
    Email,
    Game
  }
  public GameState currentGameState = GameState.Game;

  // Use this for initialization
  void Start()
  {
    onCostPage = false;
    //load tutorial things
    inTutorial = true;
    foreach (Image i in TutorialPages)
    {
      i.gameObject.SetActive(false);
    }
    if(TutorialPages.Count == 0)
    {
      inTutorial = false;
    }
    //TutorialPages[0].gameObject.SetActive(true);
    //currPage = 0;
    //look for transition manager
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
    //look for player info
    if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
      playerInfo.foodCostPerDay = foodCostPerDay;
      playerInfo.houseCostPerDay = houseCostPerDay;
      playerInfo.taxCostPerDay = taxCostPerDay;
    }
    //look for game over checker
    if (gameOverCheck == null)
    {
      gameOverCheck = FindObjectOfType<GameOverChecker>();
    }

    // Reset all newspaper objects
    leftLongBody.SetActive(false);
    leftShortBody.SetActive(false);
    leftImageObj.SetActive(false);
    rightLongBody.SetActive(false);
    rightShortBody.SetActive(false);
    rightImageObj.SetActive(false);
    newspaperCanvas.SetActive(false);

    // Reset all email objects
    emailCanvas.SetActive(false);

    // Start with newspaper if there is one
    if (todaysNewspaper != "null")
    {
      currentGameState = GameState.Newspaper;
      ParseNewspaperFile();
      UpdateNewspaperObjects();
      // Show newspaper
      newspaperCanvas.SetActive(true);
    }

    // Prepare email
    if (todaysEmail != "null")
    {
      ParseEmailFile();
      UpdateEmailObjects();
    }
  }

  void ParseNewspaperFile()
  {
    //StreamReader reader = new StreamReader(todaysNewspaper);
    string[] data = todaysNewspaper.Split('|');
    // Set title
    newsTitleText = data[0];
    // Set left column title
    leftColumnTitle = data[1];
    // Check if left column is long or short
    string tempLen = data[2].ToLower();
    if (tempLen == "long")
      leftColumnLong = true;
    else
      leftColumnLong = false;
    // Set left column text
    leftColumnBodyText = data[3];
    // Set right column title
    rightColumnTitle = data[4];
    // Check if right column is long or short
    tempLen = data[5].ToLower();
    if (tempLen == "long")
      rightColumnLong = true;
    else
      rightColumnLong = false;
    // Set right column text
    rightColumnBodyText = data[6];
    // Cleanup
  }

  void UpdateNewspaperObjects()
  {
    // Set newspaper fields
    newsTitle.GetComponent<Text>().text = newsTitleText;
    leftTitle.GetComponent<Text>().text = leftColumnTitle;
    leftLongBody.GetComponent<Text>().text = leftColumnBodyText;
    leftShortBody.GetComponent<Text>().text = leftColumnBodyText;
    rightTitle.GetComponent<Text>().text = rightColumnTitle;
    rightShortBody.GetComponent<Text>().text = rightColumnBodyText;
    rightLongBody.GetComponent<Text>().text = rightColumnBodyText;
    leftImageObj.GetComponent<Image>().sprite = leftColumnImage;
    rightImageObj.GetComponent<Image>().sprite = rightColumnImage;

    // Reveal appropriate text objects
    if (leftColumnLong)
      leftLongBody.SetActive(true);
    else
    {
      leftShortBody.SetActive(true);
      leftImageObj.SetActive(true);
    }
    if (rightColumnLong)
      rightLongBody.SetActive(true);
    else
    {
      rightShortBody.SetActive(true);
      rightImageObj.SetActive(true);
    }
  }

  void ParseEmailFile()
  {
    //StreamReader reader = new StreamReader(todaysEmail);
    string[] data = todaysEmail.Split('|');
    senderName = data[0];
    subject = data[1];
    emailBody = data[2];
    // replace ~ with newline
    emailBody = emailBody.Replace("~", "\n");
    // cleanup
  }

  void UpdateEmailObjects()
  {
    emailImageObj.GetComponent<Image>().sprite = senderImage;
    emailSenderText.GetComponent<Text>().text = senderName;
    emailSubjectText.GetComponent<Text>().text = subject;
    emailBodyText.GetComponent<Text>().text = emailBody;
  }

  // Update is called once per frame
  void Update()
  {
    playerInfo.foodCostPerDay = foodCostPerDay;
    playerInfo.houseCostPerDay = houseCostPerDay;
    playerInfo.taxCostPerDay = taxCostPerDay;
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
    }
    //look for game over checker
    if (gameOverCheck == null)
    {
      gameOverCheck = FindObjectOfType<GameOverChecker>();
    }
    //look for player info
    if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
    }
    //Play the tutorial
    if (inTutorial && currentGameState == GameState.Game)
    {
      TutorialPages[currPage].gameObject.SetActive(true);
    }
    if (onCostPage)
    {
      TotalField.text = playerInfo.currMoney.ToString();
      foodCostField.text = playerInfo.foodCostPerDay.ToString();
      houseCostField.text = playerInfo.houseCostPerDay.ToString();
      TaxCostField.text = playerInfo.taxCostPerDay.ToString();
    }

    // Check for mouse click to advance newspaper / email
    if (Input.GetMouseButtonDown(0))
    {
      if (currentGameState == GameState.Newspaper)
      {
        // Hide newspaper
        newspaperCanvas.SetActive(false);

        // Advance to email if there is one
        if (todaysEmail != "null")
        {
          currentGameState = GameState.Email;
          emailCanvas.SetActive(true);
        }

        // Otherwise, proceed to in-game state
        else
          currentGameState = GameState.Game;
      }

      else if (currentGameState == GameState.Email)
      {
        // Hide email
        emailCanvas.SetActive(false);

        // Proceed to in-game state
        currentGameState = GameState.Game;
      }
    }
  }

  public void AdvanceTutorial()
  {
    if (inTutorial)
    {
      Debug.Log("here");
      TutorialPages[currPage].gameObject.SetActive(false);
      currPage++;
      if (currPage >= TutorialPages.Count)
      {
        inTutorial = false;
        TutorialCanvas.gameObject.SetActive(false);
      }
    }
  }

  public void EndSellingDay()
  {
    MainGameCanvas.GetComponent<CanvasGroup>().interactable = false;
    playerInfo.currProfileSeen = 0;
    if(playerInfo.hasDatabreach)
    {
      playerInfo.currProfitForDay = Mathf.FloorToInt(playerInfo.currProfitForDay / 2);
    }
    //save money and reset
    playerInfo.currMoney += playerInfo.currProfitForDay;
    playerInfo.currProfitForDay = 0;
    StartCoroutine(LoadExpensePage());
  }

  public void ContinueToNextDay()
  {
    EndStates endState = gameOverCheck.CheckEndOfDay();
    if(playerInfo.dataBreachCount >=2)
    {
      endState = EndStates.DataBreach;
    }
    switch(endState)
    {
      case EndStates.NoFood:
        playerInfo.currState = EndStates.NoFood;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.NoHouse:
        playerInfo.currState = EndStates.NoHouse;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.NoTax:
        playerInfo.currState = EndStates.NoTax;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.NoMoney:
        playerInfo.currState = EndStates.NoMoney;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.TooManyStrikes:
        playerInfo.currState = EndStates.TooManyStrikes;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.KidDies:
        playerInfo.currState = EndStates.KidDies;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.DataBreach:
        playerInfo.currState = EndStates.DataBreach;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.Conspiring:
        playerInfo.currState = EndStates.Conspiring;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.FalseInfo:
        playerInfo.currState = EndStates.FalseInfo;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.Dabbnapping:
        playerInfo.currState = EndStates.Dabbnapping;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      case EndStates.None:
        //TODO: Change this
        playerInfo.ResetPlayer();
        switch (gameButton.CurrDay)
        {
          case 1:
            StartCoroutine(transitionManager.TransitionScene("Day2"));
            break;
          case 2:
            StartCoroutine(transitionManager.TransitionScene("Day3"));
            break;
          case 3:
            StartCoroutine(transitionManager.TransitionScene("Day4"));
            break;
          case 4:
            StartCoroutine(transitionManager.TransitionScene("Day5"));
            break;
          case 5:
            StartCoroutine(transitionManager.TransitionScene("Day6"));
            break;
          case 6:
            StartCoroutine(transitionManager.TransitionScene("Day7"));
            break;
          case 7:
            StartCoroutine(transitionManager.TransitionScene("End"));
            break;
          default:
            StartCoroutine(transitionManager.TransitionScene("MainMenu"));
            break;
        }
        
        break;
    }
  }

  private IEnumerator LoadExpensePage()
  {
    transitionManager.FadeScreenOut();
    while (transitionManager.isTransitioning)
    {
      yield return null;
    }
    //load cost canvases
    CostCanvas.gameObject.SetActive(true);
    TutorialCanvas.gameObject.SetActive(true);
    transitionManager.FadeScreenIn();
    //set data on expense report page
    onCostPage = true;
    TotalField.text = playerInfo.currMoney.ToString();
    foodCostField.text = playerInfo.foodCostPerDay.ToString();
    houseCostField.text = playerInfo.houseCostPerDay.ToString();
    TaxCostField.text = playerInfo.taxCostPerDay.ToString();
    while (transitionManager.isTransitioning)
    {
      yield return null;
    }
  }
}
