﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class GameButton : MonoBehaviour
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
  public Text RemainingProfilesField;
  public Text TotalCredits;
  public Day1 Day1Controller;
  public Day2 Day2Controller;
  /*public Day3 Day3Controller;
  public Day4 Day4Controller;
  public Day5 Day5Controller;
  public Day6 Day6Controller;
  public Day7 Day7Controller;*/
  public int MaxProfileForDay;
  public int PricePerProfile = 1;
  public int CurrDay;

  public GameObject recordingText;
  public GameObject recordingText2;
  public GameObject recordingText3;
  public LevelTransition transitionManager;

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
  private List<Sprite> profilePictures = new List<Sprite>();
  private List<string> recentActivities = new List<string>();
  //The list of generated profiles is now done at the start.
  //each profiles is [age, sex, hobby, education]
  [SerializeField]
  private List<List<int>> generatedProfiles = new List<List<int>>();
  private int currProfile = 0;


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
    reader.Close();
    //load profil pictures
    LoadProfilePictures();
    /*if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
    }
    TotalCredits.text = playerInfo.currMoney.ToString();
    ShowNewPerson();*/

    //Generate profiles for this level
    GenerateProfiles();
    currProfile = 0;
  }

  // Update is called once per frame
  void Update()
  {
    //I hacked this here instead because it will probably happen after we delete the duplicate playerinfo
    if (playerInfo == null)
    {
      playerInfo = FindObjectOfType<PlayerInfo>();
      TotalCredits.text = (playerInfo.currMoney + playerInfo.currProfitForDay).ToString();
      ShowNewPerson();
    }
  }

  public void SellProfile()
  {
    playerInfo.currProfileSeen++;
    if (CurrDay != 6)
    {
      playerInfo.IncreaseMoney();
    }
    if (playerInfo.currProfileSeen >= MaxProfileForDay)
    {
      TotalCredits.text = (playerInfo.currMoney + playerInfo.currProfitForDay).ToString();
      EndSelling();
    }
    else
    {
      CheckDaySpecificConstraints();
      ShowNewPerson();
    }
  }

  public void SkipProfile()
  {
    playerInfo.currProfileSeen++;
    if (playerInfo.currProfileSeen >= MaxProfileForDay)
    {
      HueyCheck();
      TotalCredits.text = (playerInfo.currMoney + playerInfo.currProfitForDay).ToString();
      EndSelling();
    }
    else
    {
      HueyCheck();
      TotalCredits.text = (playerInfo.currMoney + playerInfo.currProfitForDay).ToString();
      ShowNewPerson();
    }
  }

  public void EndSelling()
  {
    switch (CurrDay)
    {
      case 1:
        Day1Controller.EndSellingDay();
        break;
      case 2:
        Day2Controller.EndSellingDay();
        break;
      case 3:
        Day2Controller.EndSellingDay();
        break;
      case 4:
        Day2Controller.EndSellingDay();
        break;
      case 5:
        Day2Controller.EndSellingDay();
        break;
      case 6:
        Day2Controller.EndSellingDay();
        break;
    }
  }

  public void HueyCheck()
  {
    if (CurrDay == 6 && currFirstName == "Huey" && currLastName == "Fields")
    {
      playerInfo.noSellHuey = true;
      playerInfo.IncreaseIncorrect();
      //payout
      for (int i = 0; i < 10; i++)
      {
        playerInfo.IncreaseMoney();
      }
    }
    else if (CurrDay == 6)
    {
      playerInfo.IncreaseIncorrect();
    }
    switch (playerInfo.incorrectCount)
    {
      case 1:
        ShowRecordingWarning();
        break;
      case 2:
        // all profits are taken away and no more profits for the day
        playerInfo.recordedViolation = true;
        playerInfo.currProfitForDay = 0;
        ShowRecordingWarning2();
        break;
      case 3:
        // game over
        playerInfo.currState = EndStates.TooManyStrikes;
        StartCoroutine(transitionManager.TransitionScene("BadEnd"));
        break;
      default:
        break;
    }
  }

  public void CheckDaySpecificConstraints()
  {
    switch (CurrDay)
    {
      case 2:
        if (currSex != Sex.Male)
        {
          playerInfo.IncreaseIncorrect();
        }
        else if (currAge != Age.Young)
        {
          playerInfo.IncreaseIncorrect();
        }
        break;
      case 3:
        if (currSex == Sex.Male && currHobby == Hobbies.Anime && currAge == Age.Old)
        {
          playerInfo.IncreaseIncorrect();

          switch (playerInfo.incorrectCount)
          {
            case 1:
              ShowRecordingWarning();
              break;
            case 2:
              // all profits are taken away and no more profits for the day
              playerInfo.recordedViolation = true;
              playerInfo.currProfitForDay = 0;
              ShowRecordingWarning2();
              break;
            case 3:
              // game over
              playerInfo.currState = EndStates.TooManyStrikes;
              StartCoroutine(transitionManager.TransitionScene("BadEnd"));
              break;
            default:
              break;
          }

        }


        break;
      default:
        break;
      case 4:
        if (currHobby == Hobbies.Fortnite)
        {
          playerInfo.IncreaseIncorrect();
          //Also add another credit because it's double day
          playerInfo.IncreaseMoney();
        }
        break;
      case 6:
        break;
    }
  }

  public void ShowRecordingWarning()
  {
    recordingText.SetActive(false);
    recordingText2.SetActive(true);
  }

  public void ShowRecordingWarning2()
  {
    recordingText2.SetActive(false);
    recordingText3.SetActive(true);
  }

  public void ShowNewPerson()
  {
    //update profiles remaining just in case
    RemainingProfilesField.text = (MaxProfileForDay - playerInfo.currProfileSeen).ToString();
    TotalCredits.text = (playerInfo.currMoney + playerInfo.currProfitForDay).ToString();

    //Autogenerate name, sex, age, hobby, education, and recent activities.
    currFirstName = firstNames[Random.Range(0, firstNames.Count)];
    currLastName = lastNames[Random.Range(0, lastNames.Count)];
    currName = currFirstName + " " + currLastName;
    /*currAge = (Age)Random.Range(0, System.Enum.GetNames(typeof(Age)).Length - 1);
    currAge = (Age)Random.Range(0, System.Enum.GetNames(typeof(Age)).Length);
    currSex = (Sex)Random.Range(0, 3);
    currHobby = (Hobbies)Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length);
    currEd = (Education)Random.Range(0, System.Enum.GetNames(typeof(Education)).Length);*/
    currAge = (Age)generatedProfiles[currProfile][0];
    currSex = (Sex)generatedProfiles[currProfile][1];
    currHobby = (Hobbies)generatedProfiles[currProfile][2];
    currEd = (Education)generatedProfiles[currProfile][3];
    if(CurrDay == 6 && generatedProfiles[currProfile].Count > 4)
    {
      currFirstName = "Huey";
      currLastName = "Fields";
      currName = currFirstName + " " + currLastName;
    }
    currProfile++;
    currActivities = recentActivities[Random.Range(0, recentActivities.Count)];
    currPnA = (PronounsA)(currSex);

    //selecting profile picture
    Portrait.sprite = profilePictures[Random.Range(0, profilePictures.Count)];

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
    if (Random.Range(0, 2) == 0)
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

  public void LoadProfilePictures()
  {
    DirectoryInfo dir = new DirectoryInfo("Assets/Sprites/ProfilePictures/");
    foreach (FileInfo file in dir.GetFiles("*.png"))
    {
      Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
      tex.LoadImage(File.ReadAllBytes("Assets/Sprites/ProfilePictures/" + file.Name));
      Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
      profilePictures.Add(sprite);
    }
  }

  public void GenerateProfiles()
  {
    //The list of generated profiles is now done at the start.
    //each profiles is [age, sex, hobby, education]
    switch (CurrDay)
    {
      case 1: //4 random
        for (int i = 0; i < 4; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 2: //4 out of 8, young males
        for (int i = 0; i < 4; i++)
        {
          List<int> profile = new List<int>();
          profile.Add((int)Age.Young);
          profile.Add((int)Sex.Male);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 4; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(RandomExclusive((int)Age.Young, 0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(RandomExclusive((int)Sex.Male, 0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 3: //2 out of 6, old male anime
        for (int i = 0; i < 2; i++)
        {
          List<int> profile = new List<int>();
          profile.Add((int)Age.Old);
          profile.Add((int)Sex.Male);
          profile.Add((int)Hobbies.Anime);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 6; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(RandomExclusive((int)Age.Old, 0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(RandomExclusive((int)Sex.Male, 0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(RandomExclusive((int)Hobbies.Anime, 0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 4: //3 out of 7, fortnite
        for (int i = 0; i < 3; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add((int)Hobbies.Fortnite);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 4; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(RandomExclusive((int)Hobbies.Fortnite, 0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 5: //1 out of 5, young dabbingfemale
        for (int i = 0; i < 1; i++)
        {
          List<int> profile = new List<int>();
          profile.Add((int)Age.Young);
          profile.Add((int)Sex.Female);
          profile.Add((int)Hobbies.Dabbing);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 4; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(RandomExclusive((int)Age.Young, 0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(RandomExclusive((int)Sex.Female, 0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(RandomExclusive((int)Hobbies.Fortnite, 0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 6: //6 random
        for (int i = 0; i < 1; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          profile.Add(1);
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 6; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      case 7: //2 young, 5 dabbers, 3 random
        for (int i = 0; i < 2; i++)
        {
          List<int> profile = new List<int>();
          profile.Add((int)Age.Young);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(RandomExclusive((int)Hobbies.Dabbing, 0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 5; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(RandomExclusive((int)Age.Young, 0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add((int)Hobbies.Dabbing);
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        for (int i = 0; i < 3; i++)
        {
          List<int> profile = new List<int>();
          profile.Add(RandomExclusive((int)Age.Young, 0, System.Enum.GetNames(typeof(Age)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Sex)).Length));
          profile.Add(RandomExclusive((int)Hobbies.Dabbing, 0, System.Enum.GetNames(typeof(Hobbies)).Length));
          profile.Add(Random.Range(0, System.Enum.GetNames(typeof(Education)).Length));
          generatedProfiles.Add(profile);
        }
        break;
      default:
        break;
    }
    //now shuffle the list
    int count = generatedProfiles.Count;
    int last = count - 1;
    for (int i = 0; i < last; i++)
    {
      int random = Random.Range(i, count);
      List<int> temp = generatedProfiles[i];
      generatedProfiles[i] = generatedProfiles[random];
      generatedProfiles[random] = temp;
    }
  }

  /// <summary>
  /// Returns a random number between the two constraints, but makes it so it doesn't return the exclude number
  /// </summary>
  /// <param name="exclude">The number to exclude</param>
  /// <param name="min"></param>
  /// <param name="max"></param>
  /// <returns></returns>
  private int RandomExclusive(int exclude, int min, int max)
  {
    int toReturn;
    do
    {
      toReturn = Random.Range(min, max);
    }
    while (toReturn == exclude);
    return toReturn;
  }

  /// <summary>
  /// The same shit but with two exclude;
  /// </summary>
  /// <param name="exclude1">The number to exclude</param>
  /// <param name="exclude2">The number to exclude</param>
  /// <param name="min"></param>
  /// <param name="max"></param>
  /// <returns></returns>
  private int RandomExclusive(int exclude1, int exclude2, int min, int max)
  {
    int toReturn;
    do
    {
      toReturn = Random.Range(min, max);
    }
    while (toReturn == exclude1 || toReturn == exclude2);
    return toReturn;
  }
}
