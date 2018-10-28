using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
  [System.NonSerialized]
  public int currMoney = 10;
  public int currProfileSeen;
  public int currProfitForDay = 0;

  public int taxCostPerDay = 2;
  public int foodCostPerDay = 2;
  public int houseCostPerDay = 1;

  public bool paidFood;
  public bool paidTax;
  public bool paidHouse;
  public bool paidMedicine;
  public bool hasDatabreach;
  public int incorrectCount = 0;
  public int dataBreachCount = 0;
  public EndStates currState;

  private static PlayerInfo playerInfo;

  public void Awake()
  {
    if (playerInfo != null && SceneManager.GetActiveScene().name == "Day1")
    {
      playerInfo.NewPlayer();
    }
    //If we are making a new PlayerInfo but it isn't the original, delete it.
    if (playerInfo != null && playerInfo != this)
    {
      Destroy(this.gameObject);
      return;
    }
    playerInfo = this;
    DontDestroyOnLoad(this.gameObject);
  }

  void Start()
  {
    currMoney = 10; //start with 10
    currProfileSeen = 0;
    paidFood = false;
    paidTax = false;
    paidHouse = false;
    paidMedicine = false;
  }

  void Update()
  {

  }

  public bool PayTax()
  {
    if (currMoney >= taxCostPerDay)
    {
      currMoney -= taxCostPerDay;
      paidTax = true;
      return true;
    }
    return false;
  }

  public bool PayHouse()
  {
    if (currMoney >= houseCostPerDay)
    {
      currMoney -= houseCostPerDay;
      paidHouse = true;
      return true;
    }
    return false;
  }

  public bool PayFood()
  {
    if (currMoney >= foodCostPerDay)
    {
      currMoney -= foodCostPerDay;
      paidFood = true;
      return true;
    }
    return false;
  }

  public void IncreaseMoney()
  {
    currProfitForDay++;
  }

  public void IncreaseIncorrect()
  {
    incorrectCount++;
    if(incorrectCount >= 3)
    {
      hasDatabreach = true;
      dataBreachCount++;
    }
  }

  /// <summary>
  /// Resets the player info to the start of the day.
  /// This mostly resets if they paid or not.
  /// </summary>
  public void ResetPlayer()
  {
    paidFood = false;
    paidTax = false;
    paidHouse = false;
    paidMedicine = false;
    hasDatabreach = false;
    currProfitForDay = 0;
    incorrectCount = 0;
  }

  /// <summary>
  /// Resets a player to the start of a new game.
  /// </summary>
  public void NewPlayer()
  {
    paidFood = false;
    paidTax = false;
    paidHouse = false;
    paidMedicine = false;
    hasDatabreach = false;
    dataBreachCount = 0;
    currProfitForDay = 0;
    incorrectCount = 0;
    currMoney = 10;
    currState = EndStates.None;
  }
}
