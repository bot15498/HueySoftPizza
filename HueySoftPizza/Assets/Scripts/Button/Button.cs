using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Text FeedText2;

    private string currName;
    private Sex currSex;
    private Age currAge;
    private Hobbies currHobby;
    private Education currEd;

    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();


    // Use this for initialization
    void Start()
    {
        string line;
        //first names
        StreamReader reader = new StreamReader("Assets/Stories/FirstNames.txt");
        while ((line = reader.ReadLine()) != null)
        {
            firstNames.Add(line);
        }
        reader.Close();
        //last names
        reader = new StreamReader("Assets/Stories/LastNames.txt");
        while ((line = reader.ReadLine()) != null)
        {
            lastNames.Add(line);
        }
        reader.Close();
        ShowNewPerson();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNewPerson()
    {
        //Autogenerate name, sex, and age.
        currName = firstNames[Random.Range(0, firstNames.Count - 1)] + " " + lastNames[Random.Range(0, lastNames.Count - 1)];
        currAge = (Age)Random.Range(0, System.Enum.GetNames(typeof(Age)).Length - 1);
        currSex = (Sex)Random.Range(0, 2);

        //Update text on screen
        NameField.text = currName;
        int actualAge = currAge == Age.Young ? Random.Range(16, 25)
            : currAge == Age.Adult ? Random.Range(26, 49)
            : Random.Range(50, 98);
        SexAgeField.text = currSex + ", " + actualAge;
    }
}
