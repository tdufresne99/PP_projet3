using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Girl;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private Transform[] _ghostTransforms;

    [SerializeField] private Transform[] _goodEndingTransforms;

    [SerializeField] private Transform[] _badEndingTransforms;
    [SerializeField] private GameObject _girlObject;

    [SerializeField] private GirlVoiceLineManager _girlVoicelineManager;
    [SerializeField] private GirlStateManager _girlStateManager;

    [SerializeField] private GameObject _EndingCanvas;

    public void OnGoodEnding()
    {
        Debug.Log("Good ending");

        _girlObject.SetActive(false);

        for (int i = 0; i < _ghostTransforms.Length; i++)
        {
            _ghostTransforms[i].position = _goodEndingTransforms[i].position;
            Destroy(_ghostTransforms[i].gameObject.GetComponent<Rigidbody>());
            Destroy(_ghostTransforms[i].gameObject.GetComponent<CapsuleCollider>());
            var move = _ghostTransforms[i].GetComponent<MoveUpwards>();
            move.enabled = true;
        }

        // Girl voice line
        _girlVoicelineManager.PlayEndingVoiceline(true);

        // call good ending scene
        Invoke("ActivateGoodEndingCanvas", 3f);
    }

    public void OnBadEnding()
    {
        Debug.Log("Bad ending");

        _girlObject.SetActive(false);

        for (int i = 0; i < _ghostTransforms.Length; i++)
        {
            _ghostTransforms[i].position = _badEndingTransforms[i].position;
            var move = _ghostTransforms[i].GetComponent<MoveTowardsObject>();
            move.enabled = true;
            move.targetObject = _girlStateManager.playerTransform;
        }

        // Girl voice line
        _girlVoicelineManager.PlayEndingVoiceline(false);

        // call bad ending scene
        Invoke("ActivateBadEndingCanvas", 3f);
    }

    private void ActivateGoodEndingCanvas()
    {
        _EndingCanvas.SetActive(true);
        Invoke("LoadGoodEndingScene", 3f);
    }

    private void LoadGoodEndingScene()
    {
        GameManager.instance.LoadSceneWithString("GoodEnding");
    }

    private void ActivateBadEndingCanvas()
    {
        _EndingCanvas.SetActive(true);
        Invoke("LoadBadEndingScene", 3f);
    }

    private void LoadBadEndingScene()
    {
        GameManager.instance.LoadSceneWithString("BadEnding");
    }
}
