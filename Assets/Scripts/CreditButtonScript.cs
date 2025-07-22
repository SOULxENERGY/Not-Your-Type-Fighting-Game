using UnityEngine;

public class CreditButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject creditInfo;
    public void Work()
    {
        if (creditInfo.activeInHierarchy)
        {
            creditInfo.SetActive(false);
        }
        else
        {
            creditInfo.SetActive(true);
        }
    }
}
