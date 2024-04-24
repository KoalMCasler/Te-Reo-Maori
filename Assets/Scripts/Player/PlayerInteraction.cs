using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public GameObject currentInterObj = null;
    public InteractableObject currentInterObjScript = null;
    public GameObject indicator;
    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") == true)
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractableObject>();
            indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") == true)  
        {
            currentInterObj = null;
            indicator.SetActive(false);
        }
    }

    void OnInteract()
    {
        if(currentInterObj == true)
        {
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Info)
            {
                currentInterObjScript.Info();
            }
        }
        else
        {
            Debug.Log("Nothing to interact with.");
        }
    }
}
