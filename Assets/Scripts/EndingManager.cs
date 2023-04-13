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

    [SerializeField] private GirlVoiceLineManager _girlVoicelineManager;
    [SerializeField] private GirlStateManager _girlStateManager;

    [SerializeField] private GameObject _EndingCanvas;

    public void OnGoodEnding()
    {
        Debug.Log("Good ending");

        for (int i = 0; i < _ghostTransforms.Length; i++)
        {
            _ghostTransforms[i].position = _goodEndingTransforms[i].position;
        }
        _girlStateManager.girlNavMeshAgentManager.ChangeDestination(_girlStateManager.girlTransform.position);
        var lookAt = _girlStateManager.gameObject.AddComponent<LookAt>();
        lookAt.target = _girlStateManager.playerTransform;

        // Girl voice line
        _girlVoicelineManager.PlayEndingVoiceline(true);

        // call good ending scene
        Invoke("ActivateGoodEndingCanvas", 3f);
    }

    public void OnBadEnding()
    {
        Debug.Log("Bad ending");
        for (int i = 0; i < _ghostTransforms.Length; i++)
        {
            _ghostTransforms[i].position = _badEndingTransforms[i].position;
        }
        _girlStateManager.girlNavMeshAgentManager.ChangeDestination(_girlStateManager.girlTransform.position);
        var lookAt = _girlStateManager.gameObject.AddComponent<LookAt>();
        lookAt.target = _girlStateManager.playerTransform;

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
        SceneManager.LoadScene("goodEnding");
    }

    private void ActivateBadEndingCanvas()
    {
        _EndingCanvas.SetActive(true);
        Invoke("LoadBadEndingScene", 3f);
    }

    private void LoadBadEndingScene()
    {
        SceneManager.LoadScene("badEnding");
    }
}
