using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class BookController : MonoBehaviour
{
    public static BookController instance;
    public List<TextAsset> sourceMaterials; //a list of TextAssets (.txt files). Drag and drop each TextAsset into the BookController in the inspector.
    private TextMeshProUGUI leftPageText;
    private TextMeshProUGUI rightPageText;
    private List<string> sourceMaterialsToString;
    private string megaString; //a massive string with all the words from every source text
    private string RandomString;
    public int WordChunkLength = 0; //how many words are in each chunk
    private List<string> wordChunkList = new List<string>();
    private int SpaceCounter = 0;
    private string tempString;
    private Button leftButton;
    private Button rightButton;
    public Button exitButton;
    private TextMeshProUGUI bookTitle;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        megaString = "";
        sourceMaterialsToString = new List<string>();

        foreach (TextAsset story in sourceMaterials)
        {
            sourceMaterialsToString.Add(story.text);
        }

        foreach (string wordsFromThisStory in sourceMaterialsToString)
        {
            megaString += wordsFromThisStory;
        }

        //put our new megaString through a function that removes characters we don't want
        StoryTextCleaner(megaString);

        for (int i = 0; i < megaString.Length; i++)
        {
            // add to tempString the character that's being run through in this for loop
            tempString += megaString[i];
            if (megaString[i] == ' ')
            {
                SpaceCounter++;
            }

            if (SpaceCounter == WordChunkLength)
            {
                wordChunkList.Add(tempString);
                SpaceCounter = 0;
                //set equal to an empty string
                tempString = "";
            }
        }


    }

    private void Start()
    {
        leftPageText = GameObject.FindWithTag("LeftPageText").GetComponent<TextMeshProUGUI>();
        rightPageText = GameObject.FindWithTag("RightPageText").GetComponent<TextMeshProUGUI>();
        bookTitle = GameObject.FindWithTag("BookTitle").GetComponent<TextMeshProUGUI>();
        leftButton = GameObject.FindWithTag("RandomizeLeft").GetComponent<Button>();
        rightButton = GameObject.FindWithTag("RandomizeRight").GetComponent<Button>();

        bookTitle.gameObject.SetActive(false);
    }

    private string StoryTextCleaner(string story)
    {
        foreach (Char thisLetter in story)
        {
            var forbiddenCharacters = new char[] {'*', '%', '#', '^', '\n', '+', '~', '<', '>', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '[', ']' };

            foreach (var forbiddenOne in forbiddenCharacters)
            {
                if (thisLetter == forbiddenOne)
                {
                    story.Remove(thisLetter);

                }
            }
        }
        return story;
    }

    private string UppercaseFirst(string toModify)
    {
        var articles = new string[] { "a", "an", "the", "at", "by", "for", "in", "of", "on", "to", "up", "and", "as", "but", "or", "nor" };
        foreach (var article in articles)
        {
            if (toModify == article)
            {
                return toModify;
            }
        }
        if (toModify.Length <= 0) return "";
        if (toModify.Length > 1) return Char.ToUpper(toModify[0]) + toModify.Substring(1);
        return Char.ToUpper(toModify[0]).ToString();
    }

    private string TitleCleaner(string toModify)
    {
        //remove punctuation and numbers from source material
        var charsToRemove = new string[] { ",", ".", "!", "?", ";", ":" , "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        foreach (var c in charsToRemove)
        {
            toModify = toModify.Replace(c, string.Empty);
        }
        return toModify;
    }

    public void ChooseRandomString()
    {
        leftPageText.text = ""; //resets the text
        rightPageText.text = "";

        leftPageText.text = FormatRandomBook();
        rightPageText.text = FormatRandomBook();
    }

    public void ChooseRandomTitle()
    {
        bookTitle.text = FormatRandomBookTitle();
    }

    public string FormatRandomBookTitle()
    {
        string title = "";
        title += wordChunkList[Random.Range(0, wordChunkList.Count)];
        string[] fiveWordTitleString = title.Split(' ');

        for (int i = 0; i < fiveWordTitleString.Length - 1; i++)
        {
            fiveWordTitleString[i] = UppercaseFirst(fiveWordTitleString[i]);
            fiveWordTitleString[i] = TitleCleaner(fiveWordTitleString[i]);
        }

        switch (Random.Range(0, 4))
        {
            case 0:
                title = "";
                title += fiveWordTitleString[0];
                break;
            case 1:
                title = "";
                title += fiveWordTitleString[0];
                title += " " + fiveWordTitleString[1];
                break;
            case 2:
                title = "";
                title += fiveWordTitleString[0];
                title += " " + fiveWordTitleString[1];
                title += " " + fiveWordTitleString[2];
                break;
            case 3:
                title = "";
                title += fiveWordTitleString[0];
                title += " " + fiveWordTitleString[1];
                title += " " + fiveWordTitleString[2];
                title += " " + fiveWordTitleString[3];
                break;
            case 4:
                title = "";
                title += fiveWordTitleString[0];
                title += " " + fiveWordTitleString[1];
                title += " " + fiveWordTitleString[2];
                title += " " + fiveWordTitleString[3];
                title += " " + fiveWordTitleString[4];
                break;
        }
        return title;
    }

    public string AddRandomPunctuation()
    {
        string punctuation = "";
        //this array contains possible punctuation types. It is weighted to be a . than a ! or ?
        var punctuationOptions = new string[] { ".", "?", ".", ".", "!", "." };
        punctuation = punctuationOptions[UnityEngine.Random.Range(0, punctuationOptions.Length)];
        return punctuation;
    }

    public string FormatRandomBook()
    {
        //this function randomly picks a case from this switch case, then formats the text accordingly.
        // 'text' is the string you should always be altering in the following cases.
        string text = "";
        switch (UnityEngine.Random.Range(0, 5))
        {
            case 0:
                //this case takes 5 random chunks, then inserts a paragraph break. It does this 4 times.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        text += wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    string firstLetter = capitalizedLetter[0].ToString();
                    capitalizedLetter = capitalizedLetter.Remove(0, 1);
                    capitalizedLetter = capitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += capitalizedLetter;

                }
                break;
            case 1:
                //this case takes a single random chunk, then takes the first two words alone and prints those.
                text = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                string[] twoWordString = text.Split(' ');
                text = "";
                text += twoWordString[0];
                text += " " + twoWordString[1];
                break;
            case 2:
                //this case takes 4 random chunks
                for (int i = 0; i < 4; i++)
                {
                    text += "I AM CASE 2.";
                    text = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    var newPar = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    string firstLetter = newPar[0].ToString();
                    newPar = newPar.Remove(0, 1);
                    newPar = newPar.Insert(0, firstLetter.ToUpper());
                    text += newPar;
                    text = text.Remove(text.Length - 1);
                    text += AddRandomPunctuation();

                }
                break;
            case 3:
                //This case takes 5 random chunks, then inserts a paragraph break followed by a single chunk.
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        text += wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    }

                    //this next line of code removes the space from the last string, then adds random punctuation, then finally adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += AddRandomPunctuation() + "\n      ";
                    //this next line of code takes the first letter of the next string and capitalizes it.
                    string ChunkWithCapitalizedLetter = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    string firstLetter = ChunkWithCapitalizedLetter[0].ToString();
                    ChunkWithCapitalizedLetter = ChunkWithCapitalizedLetter.Remove(0, 1);
                    ChunkWithCapitalizedLetter = ChunkWithCapitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += ChunkWithCapitalizedLetter;
                    text = text.Remove(text.Length - 1);
                    text += AddRandomPunctuation();
                }
                break;
            case 4:
                //this case takes 2 random chunks, then inserts a paragraph break. It does this 4 times.
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        text += wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    string ChunkWithCapitalizedLetter = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    string firstLetter = ChunkWithCapitalizedLetter[0].ToString();
                    ChunkWithCapitalizedLetter = ChunkWithCapitalizedLetter.Remove(0, 1);
                    ChunkWithCapitalizedLetter = ChunkWithCapitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += ChunkWithCapitalizedLetter;
                }

                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    text = "I AM CASE 5.";

                    for (int j = 0; j < 3; j++)
                    {
                        text += wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = wordChunkList[UnityEngine.Random.Range(0, wordChunkList.Count)];
                    string firstLetter = capitalizedLetter[0].ToString();
                    capitalizedLetter = capitalizedLetter.Remove(0, 1);
                    capitalizedLetter = capitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += capitalizedLetter;
                }

                break;

        }

        return text;
    }



}