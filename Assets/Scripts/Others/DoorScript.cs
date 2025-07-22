using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.NonSerialized] public bool open = false;
    public float offset = 10f;
    private Vector3 closedPos;
    private Vector3 openedPos;
    public float speed;
    void Start()
    {
        closedPos = transform.position;
        openedPos = transform.position + transform.forward * -offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!open)
        {
            transform.position = Vector3.Lerp(transform.position, closedPos, Time.deltaTime*speed);
        }
        else
        {
            transform.position= Vector3.Lerp(transform.position, openedPos, Time.deltaTime * speed);
        }
    }
}
