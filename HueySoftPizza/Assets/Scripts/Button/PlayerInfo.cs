using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
  public int currMoney = 10;
  public int currProfileSeen;

  public int taxCostPerDay = 2;
  public int foodCostPerDay = 2;
  public int houseCostPerDay = 1;

  public bool paidFood;
  public bool paidTax;
  public bool paidHouse;
  public bool paidMedicine;

  // Use this for initialization
  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    currMoney = 10; //start with 10
    currProfileSeen = 0;
    paidFood = false;
    paidTax = false;
    paidHouse = false;
    paidMedicine = false;
  }

  // Update is called once per frame
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
    currMoney++;
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
  }
}
