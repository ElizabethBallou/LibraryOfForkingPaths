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

    private TextMeshProUGUI leftPageText;
    private TextMeshProUGUI rightPageText;
    private List<string> sourceTextList;
    private List<string> sourceTextFileLocation;
    private string megaString;
    private string RandomString;
    public int ChunkSize; //how many chunks will be printed into the text object
    public int WordChunkLength = 0; //how many words are in each chunk
    private List<string> splitString = new List<string>();
    private int SpaceCounter = 0;
    private string tempString;
    public Button leftButton;
    public Button rightButton;
    public Button exitButton;
    public TextMeshProUGUI bookTitle;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        megaString = "";
        sourceTextFileLocation = new List<string>();
        sourceTextList = new List<string>();


        //locate the source .txt file
        string ArabianNightsSourceTextPath = "Assets/Resources/Procedural_Text/Arabian Nights text.txt";
        sourceTextFileLocation.Add(ArabianNightsSourceTextPath);
        string TheogonySourceTextPath = "Assets/Resources/Procedural_Text/Theogony text.txt";
        sourceTextFileLocation.Add(TheogonySourceTextPath);
        string PopolVuhSourceTextPath = "Assets/Resources/Procedural_Text/Popol Vuh text.txt";
        sourceTextFileLocation.Add(PopolVuhSourceTextPath);
        Debug.Log("The count of sourceTextFileLocation is " + sourceTextFileLocation.Count);
        //get all the text of the book
        for (int i = 0; i < sourceTextFileLocation.Count; i++)
        {
            sourceTextList.Add(File.ReadAllText(sourceTextFileLocation[i]));
        }

        foreach (var epicText in sourceTextList)
        {
            megaString += epicText;
        }


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
                splitString.Add(tempString);
                SpaceCounter = 0;
                //set equal to an empty string
                tempString = "";
            }
        }


    }

    private void Start()
    {
        leftPageText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        rightPageText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame


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

    private string RemovePunctuation(string toModify)
    {
        var charsToRemove = new string[] { ",", ".", "!", "?", ";", ":" };
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
        title += splitString[Random.Range(0, splitString.Count)];
        string[] fiveWordTitleString = title.Split(' ');

        for (int i = 0; i < fiveWordTitleString.Length - 1; i++)
        {
            fiveWordTitleString[i] = UppercaseFirst(fiveWordTitleString[i]);
            fiveWordTitleString[i] = RemovePunctuation(fiveWordTitleString[i]);
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

    public string FormatRandomBook()
    {
        //this function randomly picks a case from this switch case, then formats the text accordingly.
        // 'text' is the string you should always be altering in the following cases.
        string text = "";
        switch (UnityEngine.Random.Range(0, 5))
        {
            case 0:
                //this case takes 5 random strings, then inserts a paragraph break. It does this 4 times.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        text += splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    string firstLetter = capitalizedLetter[0].ToString();
                    capitalizedLetter = capitalizedLetter.Remove(0, 1);
                    capitalizedLetter = capitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += capitalizedLetter;

                }
                break;
            case 1:
                text = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                string[] twoWordString = text.Split(' ');
                text = "";
                text += twoWordString[0];
                text += " " + twoWordString[1];
                break;
            case 2:
                for (int i = 0; i < ChunkSize; i++)
                {
                    text = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    var newPar = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    string firstLetter = newPar[0].ToString();
                    newPar = newPar.Remove(0, 1);
                    newPar = newPar.Insert(0, firstLetter.ToUpper());
                    text += newPar;

                }
                break;
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        text += splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    string firstLetter = capitalizedLetter[0].ToString();
                    capitalizedLetter = capitalizedLetter.Remove(0, 1);
                    capitalizedLetter = capitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += capitalizedLetter;
                }
                break;
            case 4:
                //this case takes 3 random strings, then inserts a paragraph break. It does this 6 times.
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        text += splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    string firstLetter = capitalizedLetter[0].ToString();
                    capitalizedLetter = capitalizedLetter.Remove(0, 1);
                    capitalizedLetter = capitalizedLetter.Insert(0, firstLetter.ToUpper());
                    text += capitalizedLetter;
                }

                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        text += splitString[UnityEngine.Random.Range(0, splitString.Count)];
                    }

                    //this code removes the space from the last string, then adds a paragraph break.
                    text = text.Remove(text.Length - 1);
                    text += ".\n      ";
                    //this code takes the first letter of the next string and capitalizes it.
                    var capitalizedLetter = splitString[UnityEngine.Random.Range(0, splitString.Count)];
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