using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int currMoney = 10; //start with 10

    [SerializeField]
    private int taxCostPerDay = 2;
    [SerializeField]
    private int foodCostPerDay = 2;
    [SerializeField]
    private int houseCostPerDay = 1; 

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
