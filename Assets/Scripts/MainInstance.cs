using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class MainInstance : MonoBehaviour
{
    public GameObject book;
    public Camera UICamera;

    public Button newBook;
    public GameObject newBookPanel;

    public Button browse;
    public InputField pathDisplay;
    public Dropdown colorDrop;
    public InputField bookName;
    public Button newBookDone;

    public Button camShelf1;
    public Button camShelf2;
    public Button camShelf3;
    public Button camShelf4;
    public GameObject moveCamPanel;
    public GameObject shelfPanels;
    public Button shelfBack;

    private Vector3 defCamPos = new Vector3(0, 10, -21);
    private Vector3 currentCamPos = new Vector3(0, 10, -21);
    private Vector3 newCamPos = new Vector3(0, 10, -21);
    private float startTime;
    private float camMoveLength;
    private bool newBookPanelActive = false;
    private bool defaultPanelActive = true;
    private bool shelfPanelActive = false;

    private Book[] books;
    private GameObject[] bookObjects;
    private int numberOfBooks = 0;
    private float totalShelfSpace = 0f;
    private int currentShelfLevel = 1;
    private string currentBookPath;

    // Start is called before the first frame update
    void Start()
    {
        UICamera.transform.position = defCamPos;
        newBook.onClick.AddListener(NewBookClick);
        browse.onClick.AddListener(BrowseEvent);
        newBookDone.onClick.AddListener(newBookDoneEvent);

        camShelf1.onClick.AddListener(camShelf1Event);
        camShelf2.onClick.AddListener(camShelf2Event);
        camShelf3.onClick.AddListener(camShelf3Event);
        camShelf4.onClick.AddListener(camShelf4Event);

        shelfBack.onClick.AddListener(shelfBackEvent);

        books = new Book[100];
        bookObjects = new GameObject[100];
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * 15.0f;
        float fractionOfJourney = distCovered / camMoveLength;
        UICamera.transform.position = Vector3.Lerp(currentCamPos, newCamPos, fractionOfJourney);
        if (UICamera.transform.position == newCamPos)
        {
            currentCamPos = newCamPos;
            updatePanels();
        }
    }

    private void NewBookClick()
    {
        Vector3 newPos = new Vector3(12.5f, 10.6f, -21.7f);
        moveCamera(newPos);
        moveCamPanel.SetActive(false);
        defaultPanelActive = false;
        newBookPanelActive = true;
    }

    private void BrowseEvent()
    {
        FileBrowser.ShowLoadDialog(OnSuccess, null);
    }

    private void newBookDoneEvent()
    {
        AddNewBook(currentBookPath);
        if(!bookName.text.Equals("")) books[numberOfBooks].SetName(bookName.text);
        setColor(colorDrop.options[colorDrop.value].text);
        totalShelfSpace = totalShelfSpace + books[numberOfBooks].GetWidth();
        numberOfBooks++;
        newBookPanel.SetActive(false);
        newBookPanelActive = false;
        moveCamera(defCamPos);
        defaultPanelActive = true;
    }

    private void setColor(string c)
    {
        if (c.Equals("Red")) bookObjects[numberOfBooks].GetComponent<Renderer>().material.color = Color.red;
        if (c.Equals("Blue")) bookObjects[numberOfBooks].GetComponent<Renderer>().material.color = Color.blue;
        if (c.Equals("Green")) bookObjects[numberOfBooks].GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnSuccess(string[] path)
    {
        pathDisplay.text = path[0];
        currentBookPath = path[0];
    }

    private void AddNewBook(string p)
    {
        books[numberOfBooks] = new Book(p, "");
        bookObjects[numberOfBooks] = Instantiate(book, NewBookPos(books[numberOfBooks]), Quaternion.identity);
        bookObjects[numberOfBooks].transform.localScale = new Vector3(books[numberOfBooks].GetWidth(), 3f, 2f);
    }

    private Vector3 NewBookPos(Book b)
    {
        float x;
        float y;
        float z = 0.4f;

        float width = b.GetWidth();
        float space = totalShelfSpace;

        x = (width / 2f) + space + 10.66f;
        if (currentShelfLevel == 1) y = 13.76f;
        else y = 0; // temp

        return new Vector3(x, y, z);
    }

    private void camShelf1Event()
    {
        Vector3 newPos = new Vector3(13.2f, 13.8f, -5.05f);
        moveCamera(newPos);
        shelfPanelActive = true;
        defaultPanelActive = false;
        moveCamPanel.SetActive(false);
    }
    private void camShelf2Event()
    {
        Vector3 newPos = new Vector3(12.3f, 10.3f, -5.05f);
        moveCamera(newPos);
        shelfPanelActive = true;
        defaultPanelActive = false;
        moveCamPanel.SetActive(false);
    }
    private void camShelf3Event()
    {
        Vector3 newPos = new Vector3(13.2f, 6.6f, -5.05f);
        moveCamera(newPos);
        shelfPanelActive = true;
        defaultPanelActive = false;
        moveCamPanel.SetActive(false);
    }
    private void camShelf4Event()
    {
        Vector3 newPos = new Vector3(12.3f, 2.85f, -5.05f);
        moveCamera(newPos);
        shelfPanelActive = true;
        defaultPanelActive = false;
        moveCamPanel.SetActive(false);
    }

    private void camDeskEvent()
    {
        Vector3 newPos = new Vector3(-1.98f, 17.45f, -5.58f);
        moveCamera(newPos);
        shelfPanelActive = true;
        defaultPanelActive = false;
        moveCamPanel.SetActive(false);
    }

    private void shelfBackEvent()
    {
        moveCamera(defCamPos);
        shelfPanels.SetActive(false);
        shelfPanelActive = false;
        defaultPanelActive = true;
    }

    private void moveCamera(Vector3 newPos)
    {
        startTime = Time.time;
        camMoveLength = Vector3.Distance(currentCamPos, newPos);
        newCamPos = newPos;
    }

    private void updatePanels()
    {
        if (newBookPanelActive == true) newBookPanel.SetActive(true);
        else newBookPanel.SetActive(false);
        if (shelfPanelActive == true) shelfPanels.SetActive(true);
        else shelfPanels.SetActive(false);
        if (defaultPanelActive == true) moveCamPanel.SetActive(true);
        else moveCamPanel.SetActive(false);
    }
}
