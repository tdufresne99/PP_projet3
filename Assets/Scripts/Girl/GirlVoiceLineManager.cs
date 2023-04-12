using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girl
{
    public class GirlVoiceLineManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _girlAudioSource;
        [SerializeField] private AudioClip[] _girlVoicelinesIntro;
        [SerializeField] private AudioClip[] _girlVoicelinesObjects;
        [SerializeField] private AudioClip _bookVoiceline;
        [SerializeField] private AudioClip _goodEndingVoiceline;
        [SerializeField] private AudioClip _badEndingVoiceline;


        public void PlayGirlVoicelineIntro(int clipIndex)
        {
            // _girlAudioSource.Stop();
            // _girlAudioSource.clip = _girlVoicelinesIntro[clipIndex];
            // _girlAudioSource.Play();

            Debug.Log("Playing girl voiceline intro #" + clipIndex);
        }
        public void PlayGirlVoicelineObject(int clipIndex)
        {
            // _girlAudioSource.Stop();
            // _girlAudioSource.clip = _girlVoicelinesObjects[clipIndex];
            // _girlAudioSource.Play();

            Debug.Log("Playing girl voiceline object #" + clipIndex);
        }

        public void PlayBookVoiceline()
        {
            _girlAudioSource.Stop();
            _girlAudioSource.clip = _bookVoiceline;
            _girlAudioSource.Play();

            Debug.Log("Playing girl book voiceline");
        }

        public void PlayEndingVoiceline(bool good)
        {
            // var endingVoiceLine = good ? _goodEndingVoiceline : _badEndingVoiceline;
            // _girlAudioSource.Stop();
            // _girlAudioSource.clip = endingVoiceLine;
            // _girlAudioSource.Play();

            var endingVL = good ? "good" : "bad";
            Debug.Log("Playing girl ending voiceline " + endingVL);
        }
    }
}
