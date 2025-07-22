using UnityEngine;

public class PlayerFootSoundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource rightFootSound;
    public AudioSource leftFootSound;
    public float timeGapBetweenTwoFootSound = 1f;
    private float timesincelastFootStep = 0f;

    private void Start()
    {
        timesincelastFootStep = timeGapBetweenTwoFootSound;
    }

    private void Update()
    {
        if (timesincelastFootStep < timeGapBetweenTwoFootSound)
        {
            timesincelastFootStep += Time.deltaTime;
        }
    }
    public void PlayRightFOOT()
    {

        if (timesincelastFootStep >= timeGapBetweenTwoFootSound)
        {
            rightFootSound.Play();
            timesincelastFootStep = 0f;
        }
            
        
        
    }


    public void PlayLeftFoot()
    {
        if (timesincelastFootStep >= timeGapBetweenTwoFootSound)
        {
            leftFootSound.Play();
            timesincelastFootStep = 0f;
        }


       
        
        
    }
}
