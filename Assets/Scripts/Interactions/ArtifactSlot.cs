using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtifactSlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    public int orderPosition;
    public bool isSlotedCorectly;
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0 && isSlotedCorectly == false)
        {
            dropped = eventData.pointerDrag;
            dropped.GetComponent<Draggable>().parentAfterDrag = transform;
            if(dropped.GetComponent<Draggable>().orderPosition == orderPosition)
            {
                dropped.GetComponent<Draggable>().enabled = false;
                dropped.transform.SetParent(transform);
                isSlotedCorectly = true;
            }
        }
    }
}
