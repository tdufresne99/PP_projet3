using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookGrab : MonoBehaviour
{
    [SerializeField] private GameObject _bookLight;
    private Animator _bookAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _bookAnimator = GetComponent<Animator>();
    }

    public void OnBookGrab()
    {
        if(_bookAnimator != null) Destroy(_bookAnimator);
        if(_bookLight != null) Destroy(_bookLight);
    }
}
