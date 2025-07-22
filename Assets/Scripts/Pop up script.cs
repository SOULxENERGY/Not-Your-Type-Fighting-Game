using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Popupscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float duration = 2f;
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

   public IEnumerator Disable()
    {

        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
