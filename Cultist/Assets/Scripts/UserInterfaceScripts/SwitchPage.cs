using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPage : MonoBehaviour
{
    public int numberOfPages;
    public int currentPage = 1;
    
    public void NextPage()
    {
        numberOfPages = GetComponent<TextMeshProUGUI>().textInfo.pageCount;
        if (currentPage < numberOfPages - 1)
        {
            currentPage++;
            GetComponent<TextMeshProUGUI>().pageToDisplay++;
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            GetComponent<TextMeshProUGUI>().pageToDisplay--;
        }
    }
}
