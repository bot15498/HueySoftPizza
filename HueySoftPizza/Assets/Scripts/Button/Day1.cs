using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day1 : MonoBehaviour
{
  public List<Image> TutorialPages;
  public List<Image> CostTutorialPages;
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

  [SerializeField]
  private int currPage;
  [SerializeField]
  private bool inTutorial;
  [SerializeField]
  private bool inCostTutorial;
  [SerializeField]
  private bool onCostPage;
  private PlayerInfo playerInfo;
  private GameOverChecker gameOverCheck;

  // Use this for initialization
  void Start()
  {
    onCostPage = false;
    //load tutorial things
    inTutorial = true;
    inCostTutorial = false;
    foreach (Image i in TutorialPages)
    {
      i.gameObject.SetActive(false);
    }
    TutorialPages[0].gameObject.SetActive(true);
    currPage = 0;
    //set cost tutorial pages to hide
    foreach (Image i in CostTutorialPages)
    {
      i.gameObject.SetActive(false);
    }
    CostCanvas.gameObject.SetActive(false);
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
    if(gameOverCheck == null)
    {
      gameOverCheck = FindObjectOfType<GameOverChecker>();
    }
  }

  // Update is called once per frame
  void Update()
  {
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
    if (inTutorial)
    {

    }
    if (inCostTutorial)
    {

    }
    if (onCostPage)
    {
      TotalField.text = playerInfo.currMoney.ToString();
      foodCostField.text = playerInfo.foodCostPerDay.ToString();
      houseCostField.text = playerInfo.houseCostPerDay.ToString();
      TaxCostField.text = playerInfo.taxCostPerDay.ToString();
    }
  }

  public void AdvanceTutorial()
  {
    if (inTutorial)
    {
      TutorialPages[currPage].gameObject.SetActive(false);
      currPage++;
      if (currPage >= TutorialPages.Count)
      {
        inTutorial = false;
        TutorialCanvas.gameObject.SetActive(false);
      }
      else
      {
        TutorialPages[currPage].gameObject.SetActive(true);
      }
    }
  }

  public void AdvanceCostTutorial()
  {
    if (inCostTutorial)
    {
      CostTutorialPages[currPage].gameObject.SetActive(false);
      currPage++;
      if (currPage >= CostTutorialPages.Count)
      {
        inCostTutorial = false;
        TutorialCanvas.gameObject.SetActive(false);
      }
      else
      {
        CostTutorialPages[currPage].gameObject.SetActive(true);
      }
    }
  }

  public void EndSellingDay()
  {
    MainGameCanvas.GetComponent<CanvasGroup>().interactable = false;
    playerInfo.currProfileSeen = 0;
    StartCoroutine(LoadExpensePage());
  }

  public void ContinueToNextDay()
  {
    EndStates endState = gameOverCheck.CheckEndOfDay();
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
      case EndStates.None:
        //TODO: Change this
        StartCoroutine(transitionManager.TransitionScene("MainMenu"));
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
    inCostTutorial = true;
    currPage = 0;
    //load cost canvases
    CostCanvas.gameObject.SetActive(true);
    TutorialCanvas.gameObject.SetActive(true);
    //load first page in cost tutorial
    CostTutorialPages[0].gameObject.SetActive(true);
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
