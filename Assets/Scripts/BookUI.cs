using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BookUI : MonoBehaviour
{
    private GameObject openBook;
    private GameObject closedBook;
    private GameObject bookTitle;
    private SpriteRenderer mySpr;
    private GameObject shelves;

    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<SpriteRenderer>();
        closedBook = UIManager.instance.closedBook;
        openBook = UIManager.instance.bookPanel;
        bookTitle = UIManager.instance.bookTitle;
        shelves = UIManager.instance.shelves;
    }

    private void OnMouseEnter()
    {
        transform.localScale *= 1.6f;
        mySpr.color = UIManager.instance.mouseOverColor;
        //mySpr.sortingLayerName = "Zoomed";
        closedBook.SetActive(true);
        bookTitle.SetActive(true);
        BookController.instance.ChooseRandomTitle();
    }

    private void OnMouseExit()
    {
        transform.localScale /= 1.6f;
        mySpr.color = Color.white;
        mySpr.sortingLayerName = "Default";
        closedBook.SetActive(false);
        bookTitle.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        
        transform.localScale /= 1.6f;
        mySpr.color = Color.white;
        mySpr.sortingLayerName = "Default";
        openBook.SetActive(true);
        closedBook.SetActive(false);
        bookTitle.SetActive(false);
        bookTitle.SetActive(false);
        shelves.SetActive(false);
        BookController.instance.ChooseRandomString();
        Debug.Log("attempting to open the book");
    }
}
