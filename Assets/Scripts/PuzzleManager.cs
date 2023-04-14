using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Girl;
using UnityEngine.XR.Interaction.Toolkit;
public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject _girlWaypointManager;
    [SerializeField] private bool[] _activatedPuzzleItems = new bool[] { false, false, false, false };
    [SerializeField] private GameObject _endCanvas;
    [SerializeField] private GameObject _smokeCanvas;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject[] _ghostsObjects;
    [SerializeField] private GameObject[] _endingGhostsObjects;
    [SerializeField] private GameObject[] _puzzleObjects;
    [SerializeField] private GameObject[] _socketedPuzzleObjects;
    [SerializeField] private GameObject _bookObject;
    [SerializeField] private GameObject _bookLightObject;
    [SerializeField] private GirlVoiceLineManager _girlVoiceLineManagerCS;
    [SerializeField] private GirlWaypointManager _girlWaypointManagerCS;
    [SerializeField] private GirlStateManager _girlStateManagerCS;
    [SerializeField] private RoomSmoke[] _roomSmokeCSs;

    void Start()
    {
        SetItemsActivity();
    }

    public void OnPuzzleItemPlaced(int index)
    {
        _girlStateManagerCS.girlAudioSource.pitch = 1f;
        _girlStateManagerCS.girlAudioSource.PlayOneShot(_girlStateManagerCS.girlIdleClip);

        if(index == 0) _girlWaypointManagerCS.TeleportGirl();
        _socketedPuzzleObjects[index].GetComponent<XRGrabInteractable>().interactionLayers &= ~(1 << 0);
        _activatedPuzzleItems[index] = true;

        if(index == 0) Destroy(_girlWaypointManager);

        if(index < _ghostsObjects.Length) ActivateNextObject(index);
        else OnPuzzleCompleted();
    }

    private void SetItemsActivity()
    {
        for (int i = 0; i < _ghostsObjects.Length; i++)
        {
            _ghostsObjects[i].SetActive(false);
            _puzzleObjects[i].SetActive(false);
        }
        _bookObject.SetActive(false);
    }

    private void ActivateNextObject(int index)
    {
        _girlVoiceLineManagerCS.PlayGirlVoicelineObject(index);
        _ghostsObjects[index].SetActive(true);
        _puzzleObjects[index].SetActive(true);
    }

    public void OnPuzzleItemRemoved(int index)
    {
        _activatedPuzzleItems[index] = false;
    }

    private void VerifyPuzzle()
    {
        bool puzzelCompleted = true;
        for (int i = 0; i < _activatedPuzzleItems.Length; i++)
        {
            if (_activatedPuzzleItems[i] == false)
            {
                puzzelCompleted = false;
                break;
            }
        }
        if (puzzelCompleted == true) OnPuzzleCompleted();
    }

    private void OnPuzzleCompleted()
    {
        Debug.Log("Puzzle is completed!");

        for (int i = 0; i < _ghostsObjects.Length; i++)
        {
            _ghostsObjects[i].SetActive(false);
        }
        for (int i = 0; i < _endingGhostsObjects.Length; i++)
        {
            _endingGhostsObjects[i].SetActive(true);
        }
        for (int i = 0; i < _roomSmokeCSs.Length; i++)
        {
            _roomSmokeCSs[i].SmokeIsOn = false;
        }
        _smokeCanvas.SetActive(false);
        _deathCanvas.SetActive(false);

        _girlVoiceLineManagerCS.PlayBookVoiceline();
        ActivateBook();
    }

    private void ActivateBook()
    {
        _bookObject.SetActive(true);
        _bookLightObject.SetActive(true);
    }
}
