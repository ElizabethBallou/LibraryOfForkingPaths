using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject bookPanel;
    public GameObject shelves;
    public GameObject bookshelf;
    public GameObject closedBook;
    public GameObject bookTitle;
    public GameObject titleHolder;
    private TextMeshProUGUI[] titleObjects;
    public Button enterButton;
    private TextMeshProUGUI enterButtonText;
    public Button quitButton;
    private TextMeshProUGUI quitButtonText;
    public Color mouseOverColor;
    public Transform[] shelfArray;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enterButtonText = enterButton.GetComponentInChildren<TextMeshProUGUI>();
        quitButtonText = quitButton.GetComponentInChildren<TextMeshProUGUI>();

        bookPanel.SetActive(false);
        titleObjects = titleHolder.GetComponentsInChildren<TextMeshProUGUI>();
        shelfArray = shelves.GetComponentsInChildren<Transform>();
        foreach (Transform shelfTransform in shelfArray)
        {
            shelfTransform.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    
    public void SummonBook()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 1000f);
        
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("openBook"))
                {
                bookPanel.SetActive(true);
                closedBook.SetActive(false);
                shelves.SetActive(false);
                    BookController.instance.ChooseRandomString();
                }
            }
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
        shelves.SetActive(true);
        
    }

    public void EnterLibrary()
    {
        foreach (TextMeshProUGUI textItem in titleObjects)
        {
            textItem.DOFade(0f, 1f).OnComplete(() => textItem.gameObject.SetActive(false));
            enterButton.image.DOFade(0f, 1f).OnComplete(() => enterButton.gameObject.SetActive(false));
            enterButtonText.DOFade(0f, 1f).OnComplete(() => enterButtonText.gameObject.SetActive(false));
        }

        foreach (Transform shelfTransform in shelfArray)
        {
            bookshelf.GetComponent<SpriteRenderer>().DOFade(1f, 1f).SetDelay(1f);
            shelfTransform.gameObject.SetActive(true);
            if (shelfTransform.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                shelfTransform.gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 1f).SetDelay(1f);
            }
        }

        quitButton.image.DOFade(.5f, 1f).SetDelay(1f);
        quitButtonText.DOFade(1f, 1f).SetDelay(1f);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

