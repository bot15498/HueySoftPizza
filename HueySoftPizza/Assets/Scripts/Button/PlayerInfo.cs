using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
  public int currMoney;
  public int currProfileSeen;

  public int taxCostPerDay = 2;
  public int foodCostPerDay = 2;
  public int houseCostPerDay = 1;

  // Use this for initialization
  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    currMoney = 10; //start with 10
    currProfileSeen = 0;
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
      return true;
    }
    return false;
  }

  public bool PayHouse()
  {
    if (currMoney >= houseCostPerDay)
    {
      currMoney -= houseCostPerDay;
      return true;
    }
    return false;
  }

  public bool PayFood()
  {
    if (currMoney >= foodCostPerDay)
    {
      currMoney -= foodCostPerDay;
      return true;
    }
    return false;
  }

  public void IncreaseMoney()
  {
    currMoney++;
  }
}
