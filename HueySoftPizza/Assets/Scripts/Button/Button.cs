﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class Button : MonoBehaviour
{

    public enum Age
    {
        Young,
        Adult,
        Old,
    }

    public enum Hobbies
    {
        Fortnite,
        Anime,
        Dabbing,
    }

    public enum Education
    {
        HighSchool,
        College,
    }

    public enum Sex
    {
        Male,
        Female,
        ElonMuskCatgirl,
    }
    
    public enum PronounsA
    {
        his,
        her,
        zer
    }

    public Image Portrait;
    public Text NameField;
    public Text SexAgeField;
    public Text BioField;
    public Text FeedText1;
    public int MaxProfileForDay;
    public int PricePerProfile = 1;

    [SerializeField]
    private string currName;
    [SerializeField]
    private string currFirstName;
    [SerializeField]
    private string currLastName;
    [SerializeField]
    private Sex currSex;
    [SerializeField]
    private Age currAge;
    [SerializeField]
    private Hobbies currHobby;
    [SerializeField]
    private Education currEd;
    [SerializeField]
    private string currActivities;
    [SerializeField]
    private PronounsA currPnA;

    //private int currProfileSeen = 0;
    private PlayerInfo playerInfo;

    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();
    private List<string> hobbyAnime = new List<string>();
    private List<string> hobbyFortnite = new List<string>();
    private List<string> hobbyDabbing = new List<string>();
    private List<string> edHighSchool = new List<string>();
    private List<string> edCollege = new List<string>();
    private List<string> recentActivities = new List<string>();

    // Use this for initialization
    void Start()
    {
        string line;
        //first names
        StreamReader reader = new StreamReader("Assets/Stories/FirstNames.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            firstNames.Add(line);
        }
        reader.Close();
        //last names
        reader = new StreamReader("Assets/Stories/LastNames.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            lastNames.Add(line);
        }
        reader.Close();
        //About me, hobby, anime
        reader = new StreamReader("Assets/Stories/AboutMes/Hobbies/Anime.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            hobbyAnime.Add(line);
        }
        reader.Close();
        //About me, hobby, fortnite
        reader = new StreamReader("Assets/Stories/AboutMes/Hobbies/Fortnite.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            hobbyFortnite.Add(line);
        }
        reader.Close();
        //About me, hobby, fortnite
        reader = new StreamReader("Assets/Stories/AboutMes/Hobbies/Dabbing.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            hobbyDabbing.Add(line);
        }
        reader.Close();
        //About me, education, high school
        reader = new StreamReader("Assets/Stories/AboutMes/Education/HighSchool.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            edHighSchool.Add(line);
        }
        reader.Close();
        //About me, education, college
        reader = new StreamReader("Assets/Stories/AboutMes/Education/College.txt", Encoding.GetEncoding(1252));
        while ((line = reader.ReadLine()) != null)
        {
            edCollege.Add(line);
        }
        reader.Close();
        //Recent activities
        reader = new StreamReader("Assets/Stories/RecentActivities.txt", Encoding.Default);
        while ((line = reader.ReadLine()) != null)
        {
            recentActivities.Add(line);
        }

        if(playerInfo == null)
        {
            playerInfo = FindObjectOfType<PlayerInfo>();
        }
        ShowNewPerson();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInfo == null)
        {
            playerInfo = FindObjectOfType<PlayerInfo>();
        }
    }

    public void SellProfile()
    {
        playerInfo.currProfileSeen++;
        playerInfo.IncreaseMoney();
        ShowNewPerson();
    }

    public void SkipProfile()
    {
        playerInfo.currProfileSeen++;
        ShowNewPerson();
    }

    public void EndSelling()
    {

    }

    public void ShowNewPerson()
    {
        //Autogenerate name, sex, age, hobby, education, and recent activities.
        currFirstName = firstNames[Random.Range(0, firstNames.Count)];
        currLastName = lastNames[Random.Range(0, lastNames.Count)];
        currName = currFirstName + " " + currLastName;
        currAge = (Age)Random.Range(0, System.Enum.GetNames(typeof(Age)).Length - 1);
        currSex = (Sex)Random.Range(0, 3);
        currHobby = (Hobbies)Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length );
        currEd = (Education)Random.Range(0, System.Enum.GetNames(typeof(Education)).Length);
        currActivities = recentActivities[Random.Range(0, recentActivities.Count)];
        currPnA = (PronounsA)(currSex);

        // Resize age/sex text if too big
        if (currSex == Sex.ElonMuskCatgirl)
            SexAgeField.fontSize = 26;
        else
            SexAgeField.fontSize = 34;

        // Generate a 2nd name to use in filling recent activities. If it's equal to first name, re-roll.
        string name2;
        do
          name2 = firstNames[Random.Range(0, firstNames.Count)];
        while (name2 == currFirstName);
        // Fill in Recent Activities text fields
        string newAct = currActivities;
        newAct = newAct.Replace("P1", currFirstName);
        newAct = newAct.Replace("P2", name2);
        newAct = newAct.Replace("PNA1", currPnA.ToString());
        currActivities = newAct;

        //Update text on screen
        NameField.text = currName;
        int actualAge = currAge == Age.Young ? Random.Range(16, 25)
            : currAge == Age.Adult ? Random.Range(26, 49)
            : Random.Range(50, 98);
        SexAgeField.text = currSex + ", " + actualAge;
        if(Random.Range(0,2) == 0)
        {
            FeedText1.text = currHobby == Hobbies.Anime ? hobbyAnime[Random.Range(0, hobbyAnime.Count)]
                : currHobby == Hobbies.Fortnite ? hobbyFortnite[Random.Range(0, hobbyFortnite.Count)]
                : hobbyDabbing[Random.Range(0, hobbyDabbing.Count)];
            FeedText1.text += "\n\n";
            FeedText1.text += currEd == Education.HighSchool ? edHighSchool[Random.Range(0, edHighSchool.Count)]
                : edCollege[Random.Range(0, edCollege.Count)];
        }
        else
        {
            FeedText1.text = currEd == Education.HighSchool ? edHighSchool[Random.Range(0, edHighSchool.Count)]
                : edCollege[Random.Range(0, edCollege.Count)];
            FeedText1.text += "\n\n";
            FeedText1.text += currHobby == Hobbies.Anime ? hobbyAnime[Random.Range(0, hobbyAnime.Count)]
                : currHobby == Hobbies.Fortnite ? hobbyFortnite[Random.Range(0, hobbyFortnite.Count)]
                : hobbyDabbing[Random.Range(0, hobbyDabbing.Count)];
        }
        BioField.text = currActivities;
    }
}
