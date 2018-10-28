using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndStates
{
  None,
  NoMoney,
  NoFood,
  NoHouse,
  NoTax,
  DataBreach,
  KidDies,
  FalseInfo,
  TooManyStrikes,
  Conspiring,
  Dabbnapping
}

public class GameOverChecker : MonoBehaviour
{
  public PlayerInfo playerInfo;

  void Start()
  {
    if(playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
    }
  }

  void Update()
  {
    if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
    }
  }

  public EndStates CheckEndOfDay()
  {
    if(playerInfo.currMoney < 0)
    {
      return EndStates.NoMoney;
    }
    else if(!playerInfo.paidFood)
    {
      return EndStates.NoFood;
    }
    else if (!playerInfo.paidHouse)
    {
      return EndStates.NoHouse;
    }
    else if (!playerInfo.paidTax)
    {
      return EndStates.NoTax;
    }
    else
    {
      return EndStates.None;
    }
  }
}
