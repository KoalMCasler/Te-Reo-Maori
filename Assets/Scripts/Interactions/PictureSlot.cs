using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PictureSlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    public int orderPosition;
    public bool isSlotedCorrectly;
    private PuzzleManager puzzleManager;
    private SoundManager soundManager;
    [SerializeField] PuzzleAsset puzzleAsset;
    [SerializeField] GameObject tape;

    private void Start()
    {   
        puzzleManager = FindObjectOfType<PuzzleManager>();
        soundManager = FindObjectOfType<SoundManager>();
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
                soundManager.PlaySfxAudio("correct");
                this.GetComponent<Button>().interactable = false;
                if(tape != null)
                    tape.SetActive(true);
            }
        }
        if(puzzleAsset == puzzleManager.puzzlesToComplete[1])
        {
            puzzleManager.CheckSecondPuzzle(puzzleAsset);
        }
        if(puzzleAsset == puzzleManager.puzzlesToComplete[2])
        {
            puzzleManager.CheckThirdPuzzle(puzzleAsset);
        }
    }
    public void OnControllerDrop(GameObject item)
    {
        if(item.GetComponent<Draggable>() != null)
        {
            dropped = item;
            if(dropped.GetComponent<Draggable>().orderPosition == orderPosition)
            {
                dropped.GetComponent<Draggable>().enabled = false;
                dropped.transform.SetParent(transform);
                isSlotedCorrectly = true;
                soundManager.PlaySfxAudio("correct");
                this.GetComponent<Button>().interactable = false;
                if(tape != null)
                    tape.SetActive(true);
            }
        }
        if(puzzleAsset == puzzleManager.puzzlesToComplete[1])
        {
            puzzleManager.CheckSecondPuzzle(puzzleAsset);
        }
        if(puzzleAsset == puzzleManager.puzzlesToComplete[2])
        {
            puzzleManager.CheckThirdPuzzle(puzzleAsset);
        }
    }
}
