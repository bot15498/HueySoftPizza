using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostPage : MonoBehaviour
{
  //This is a simple class that disables buttons when clicked.
  //Don't put day specific code here.
  public Button FoodButton;
  public Button HouseButton;
  public Button TaxButton;
  private PlayerInfo info;

  public void Start()
  {
    if (info == null)
    {
      info = FindObjectOfType<PlayerInfo>();
    }
  }

  public void Update()
  {
    if (info == null)
    {
      info = FindObjectOfType<PlayerInfo>();
    }
  }

  public void PayFoodCost()
  {
    if (info.PayFood())
    {
      FoodButton.GetComponentInChildren<Text>().text = "Paid!";
      FoodButton.interactable = false;
    }
  }

  public void PayHouseCost()
  {
    if (info.PayHouse())
    {
      HouseButton.GetComponentInChildren<Text>().text = "Paid!";
      HouseButton.interactable = false;
    }
  }

  public void PayTaxCost()
  {
    if (info.PayTax())
    {
      TaxButton.GetComponentInChildren<Text>().text = "Paid!";
      TaxButton.interactable = false;
    }
  }
}
