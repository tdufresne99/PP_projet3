using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private bool[] _activatedPuzzleItems = new bool[] { false, false, false, false };
    [SerializeField] private GameObject _endCanvas;
    [SerializeField] private GameObject _smokeCanvas;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject _mother;
    [SerializeField] private GameObject _boy;
    [SerializeField] private GameObject _father;

    public void OnPuzzleItemPlaced(int index)
    {
        _activatedPuzzleItems[index] = true;

        VerifyPuzzle();
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
        _mother.SetActive(false);
        _boy.SetActive(false);
        _father.SetActive(false);

        _deathCanvas.SetActive(false);
        _smokeCanvas.SetActive(false);
        _endCanvas.SetActive(true);
    }
}
