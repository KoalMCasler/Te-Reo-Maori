using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject door;

    public Sprite openGate;
    // Update is called once per frame
    void Update()
    {
        if(door.GetComponent<InteractableObject>() != null)
        {
            if(door.GetComponent<InteractableObject>().isLocked == false)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = openGate;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
