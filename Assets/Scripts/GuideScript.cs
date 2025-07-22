using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GuideScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> allGuideImages;
    [System.NonSerialized] public int level = 0;
    public GameObject guidebUtton;
    public void ShowInfo()
    {
        guidebUtton.SetActive(false);
        allGuideImages[level].SetActive(true);
        Time.timeScale = 0;
    }

    public void HideInfo()
    {
        guidebUtton.SetActive(true);
        Time.timeScale = 1;
        allGuideImages[level].SetActive(false);
    }
}
