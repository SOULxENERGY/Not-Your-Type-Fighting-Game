using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelInitializer : MonoBehaviour
{
    public List<Transform> objectsTodisabled = new List<Transform>();
    public List<Transform> objectsToEnabled = new List<Transform>();
    public DoorScript door;
    public Transform nextBoss;
    public PlayerStateManager player;
    [System.NonSerialized] public bool isLevelStarted = false;
    public GuideScript guideBox;
    public int level;
  
    
    private void OnTriggerEnter(Collider other)
    {
        guideBox.level = level;
        guideBox.ShowInfo();
        player.GetComponent<HealthManagement>().Heal(35);
        if (nextBoss)
        {
            player.enemy = nextBoss;
        }
        
        door.open = false;
        foreach(Transform obj in objectsTodisabled)
        {
            if (obj)
            {
                obj.gameObject.SetActive(false);
            }
            
        }

      
        isLevelStarted = true;
        foreach (Transform obj in objectsToEnabled)
        {
            if (obj)
            {
                obj.gameObject.SetActive(true);
            }
            
        }
        gameObject.SetActive(false);


    }
}
