using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PictureSlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    public int orderPosition;
    public bool isSlotedCorrectly;
    private PuzzleManager puzzleManager;
    [SerializeField] PuzzleAsset puzzleAsset;
    [SerializeField] GameObject tape;

    private void Start()
    {   
        puzzleManager = FindObjectOfType<PuzzleManager>();
        if(tape != null)
            tape.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0 && isSlotedCorrectly == false)
        {
            dropped = eventData.pointerDrag;
            dropped.GetComponent<Draggable>().parentAfterDrag = transform;
            puzzleManager.StartPuzzle(puzzleAsset);

            if(dropped.GetComponent<Draggable>().orderPosition == orderPosition)
            {
                dropped.GetComponent<Draggable>().enabled = false;
                dropped.transform.SetParent(transform);
                isSlotedCorrectly = true;
                if(tape != null)
                    tape.SetActive(true);
            }
        }
        puzzleManager.CheckSecondPuzzle(puzzleAsset);
    }
}
