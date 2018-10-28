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
  }

  // Update is called once per frame
  void Update()
  {
    if (transitionManager == null)
    {
      transitionManager = FindObjectOfType<LevelTransition>();
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
