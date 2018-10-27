using UnityEngine; // MonoBehaviour
using System.IO; // StreamReader
using UnityEngine.UI; // Text

public class Story
{
  public string path;
  public StreamReader reader;
  public string currentLine;

  public Story(string p)
  {
    path = p;
    reader = new StreamReader(p);
  }

  ~Story()
  {
    reader.Close();
  }
}

public class StoryReader : MonoBehaviour
{
  public Text dialogueText; // The text component
  public GameObject dialogueUI; // The group of Dialogue UI Objects
  public string[] stories = new string[5];
  public Story currentStory = null;
  public int currentStoryIndex = -1;

  public void BeginStory(Story story)
  {
    // Unhide dialogue UI
    ShowDialogueUI();

    // Get the first line
    AdvanceStory(story);
  }


  public void AdvanceStory(Story story)
  {
    // Get the next line in the story
    story.currentLine = story.reader.ReadLine();

    // If the end is reached, finish the story
    if (story.currentLine == null)
      EndStory(story);

    // Else, show the line
    else
    {
      dialogueText.text = story.currentLine;
    }
  }


  public void EndStory(Story story)
  {
    // Hide Dialogue UI
    HideDialogueUI();

    // Clean up StreamReader stuff
    story.reader.Close();

    // Reset currentStory
    currentStory = null;
  }


  public void HideDialogueUI()
  {
    dialogueUI.SetActive(false);
  }


  public void ShowDialogueUI()
  {
    dialogueUI.SetActive(true);
  }


  void Start()
  {
    // LIST OF STORIES
    stories[0] = "Assets/Stories/Intro.txt";
    stories[1] = "Assets/Stories/Intro.txt";
    stories[2] = null;
    stories[3] = null;
    stories[4] = null;

    HideDialogueUI();
  }


  void Update()
  {
    // When space is pressed, start the next story or continue the current story
    if (Input.GetKeyDown("space"))
    {
      // If a story hasn't started, start the next story
      if (currentStory == null)
      {
        // Seek the next story in the array of stories
        currentStoryIndex++;

        // If array of stories is out of bounds, end the function
        if (currentStoryIndex > 4)
          return;

        // If the story array isnt empty
        if(stories[currentStoryIndex] != null)
        {
          // Start the next story
          currentStory = new Story(stories[currentStoryIndex]);
          BeginStory(currentStory);
        }

        // Debug: show stories advancing
        Debug.Log("story #" + currentStoryIndex.ToString());
      }

      // If a story is in progress, advance the current story
      else
        AdvanceStory(currentStory);
    }
  }
}
