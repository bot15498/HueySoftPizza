using System.Collections;
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
        Catgirl,
    }

    public Image Portrait;
    public Text NameField;
    public Text SexAgeField;
    public Text BioField;
    public Text FeedText1;
    public Text RemainingProfilesField;
    public Day1 Day1Controller;
    /*public Day2 Day2Controller;
    public Day3 Day3Controller;
    public Day4 Day4Controller;
    public Day5 Day5Controller;
    public Day6 Day6Controller;
    public Day7 Day7Controller;*/
    public int MaxProfileForDay;
    public int PricePerProfile = 1;
    public int CurrDay;

    [SerializeField]
    private string currName;
    [SerializeField]
    private Sex currSex;
    [SerializeField]
    private Age currAge;
    [SerializeField]
    private Hobbies currHobby;
    [SerializeField]
    private Education currEd;

    //private int currProfileSeen = 0;
    private PlayerInfo playerInfo;

    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();
    private List<string> hobbyAnime = new List<string>();
    private List<string> hobbyFortnite = new List<string>();
    private List<string> hobbyDabbing = new List<string>();
    private List<string> edHighSchool = new List<string>();
    private List<string> edCollege = new List<string>();


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
        if(playerInfo.currProfileSeen >= MaxProfileForDay)
        {
            EndSelling();
        }
        else
        {
            ShowNewPerson();
        }
    }

    public void SkipProfile()
    {
        playerInfo.currProfileSeen++;
        if (playerInfo.currProfileSeen >= MaxProfileForDay)
        {
            EndSelling();
        }
        else
        {
            ShowNewPerson();
        }
    }

    public void EndSelling()
    {
        switch(CurrDay)
        {
            case 1:
                Day1Controller.EndSellingDay();
                break;
        }
    }

    public void ShowNewPerson()
    {
        //update profiles remaining just in case
        RemainingProfilesField.text = (MaxProfileForDay - playerInfo.currProfileSeen).ToString();

        //Autogenerate name, sex, and age.
        currName = firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count - 1)];
        currAge = (Age)Random.Range(0, System.Enum.GetNames(typeof(Age)).Length - 1);
        currSex = (Sex)Random.Range(0, 2);
        currHobby = (Hobbies)Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length );
        currEd = (Education)Random.Range(0, System.Enum.GetNames(typeof(Education)).Length);

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
    }
}
