using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iTextSharp.text.pdf;

public class Book
{
    private string bookPath;
    private string bookName;
    private int numberOfPages;
    private float bookWidth;

    public Book(string path, string name)
    {
        bookPath = path;
        bookName = name;

        PdfReader reader = new PdfReader(path);
        numberOfPages = reader.NumberOfPages;

        determineWidth();
    }

    private void determineWidth()
    {
        if (numberOfPages <= 10) bookWidth = 0.5f;
        else if (numberOfPages >= 1000) bookWidth = 1.25f;
        else
        {
            bookWidth = ((((float) numberOfPages) / (1000f)) * 0.75f) + 0.5f;
        }
    }

    public string GetName()
    {
        return bookName;
    }

    public int GetPages()
    {
        return numberOfPages;
    }

    public float GetWidth()
    {
        return bookWidth;
    }

    public void SetName(string newName)
    {
        bookName = newName;
    }
}
