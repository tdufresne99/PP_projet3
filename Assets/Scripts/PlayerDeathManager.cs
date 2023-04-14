using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mother;
using Father;
using Boy;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField] private AudioSource _generalAudioSource;
    [SerializeField] private AudioClip _heartbeat;
    [SerializeField] private MotherStateManager _motherManagerCS;
    [SerializeField] private FatherStateManager _fatherManagerCS;
    [SerializeField] private BoyStateManager _boyManagerCS;

    [SerializeField] private Animator _deathCanvasAnimator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _playerRespawnTransform;
    private string _fadeOutTrigger = "fadeOut";
    private string _fadeInTrigger = "isFadingIn";

    [SerializeField] private Animator _smokeCanvasAnimator;
    private string _smokeOnTrigger = "smokeIsOn";
    private bool _smokeStarted = false;

    [SerializeField] private float _timeToKillPlayerWithSmoke = 3f;
    private Coroutine _coroutineSmokePlayer;

    private Coroutine _coroutineResetGame;

    void Start()
    {
        _deathCanvasAnimator.SetBool(_fadeInTrigger, true);
    }

    private IEnumerator CoroutineResetGameState()
    {
        ResetPlayer();
        yield return new WaitForSecondsRealtime(1f);
        ResetAIStates();
        ResetEnvironment();
    }


    private void ResetAIStates()
    {
        if (_boyManagerCS.gameObject.activeInHierarchy) _boyManagerCS.ResetState();
        if (_fatherManagerCS.gameObject.activeInHierarchy) _fatherManagerCS.ResetState();
        if (_motherManagerCS.gameObject.activeInHierarchy) _motherManagerCS.ResetState();
    }

    private void ResetEnvironment()
    {

    }

    private void ResetPlayer()
    {
        PlayDeathCanvasAnimations();
    }

    private void PlayDeathCanvasAnimations()
    {
        _generalAudioSource.PlayOneShot(_heartbeat);
        _deathCanvasAnimator.SetBool(_fadeInTrigger, false);
        Invoke("FadeOutPlayerDeathCanvas", 4f);
    }

    public void ResetPlayerPosition()
    {
        _playerTransform.position = _playerRespawnTransform.position;
    }

    private void FadeOutPlayerDeathCanvas()
    {
        _deathCanvasAnimator.SetBool(_fadeInTrigger, true);
    }

    public void StartCoroutineResetGame()
    {
        _coroutineResetGame = StartCoroutine(CoroutineResetGameState());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeleeAttack>() != null)
        {

            StartCoroutineResetGame();
        }
    }

    public void ToggleCoroutineSmoke(bool start)
    {
        if (start)
        {
            if (_smokeStarted == false) _coroutineSmokePlayer = StartCoroutine(CoroutineSmokePlayer());
        }
        else if (_smokeStarted == true)
        {
            if (_coroutineSmokePlayer != null) StopCoroutine(_coroutineSmokePlayer);
            _smokeCanvasAnimator.SetBool(_smokeOnTrigger, false);
            _smokeStarted = false;
        }
    }

    private IEnumerator CoroutineSmokePlayer()
    {
        _smokeStarted = true;
        _smokeCanvasAnimator.SetBool(_smokeOnTrigger, true);
        yield return new WaitForSecondsRealtime(_timeToKillPlayerWithSmoke);
        StartCoroutineResetGame();
        _smokeStarted = false;
    }
}
