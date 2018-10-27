using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    [SerializeField]
    private bool inTutorial;

    // Use this for initialization
    void Start()
    {
        //load tutorial things
        inTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(inTutorial)
        {

        }
    }
}
