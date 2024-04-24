using UnityEngine;
using UnityEngine.EventSystems;

public class WordHover : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource audioSource;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<AudioSource>().Play();	
    }
}
