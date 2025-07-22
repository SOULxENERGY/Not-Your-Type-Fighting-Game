using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ButtonScriptForReload : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float reloadTime;
   private float timeSinceLastTimeUsed;
    public TMP_Text text;
    public string actualString;
    public int usageNo = 2;
    private int noOfTimeAlreadyUsed = 0;
    public GameObject loadingBackground;

    public UnityEvent onUse;


    private void Start()
    {
        timeSinceLastTimeUsed = reloadTime;
    }

    private void Update()
    {
        if (timeSinceLastTimeUsed < reloadTime)
        {
            if (loadingBackground)
            {
                if (!loadingBackground.activeInHierarchy)
                {
                    loadingBackground.SetActive(true);
                }
            }
            
            
            timeSinceLastTimeUsed += Time.deltaTime;
            text.text = $"{Mathf.Round(timeSinceLastTimeUsed)}";
        }
        else
        {
            if (loadingBackground)
            {
                loadingBackground.SetActive(false);
            }
            
            text.text = actualString;
        }
    }

    public void Functionality()
    {
        if (timeSinceLastTimeUsed >= reloadTime)
        {
            //call the function
            
            onUse.Invoke();
            noOfTimeAlreadyUsed++;
            if (noOfTimeAlreadyUsed >= usageNo)
            {
                timeSinceLastTimeUsed = 0f;
                noOfTimeAlreadyUsed = 0;
            }
            
        }
    }
}
