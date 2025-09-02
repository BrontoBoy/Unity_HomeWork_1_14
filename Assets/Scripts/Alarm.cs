using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;
    private const float DefaultFadeSpeed = 1f;
    private const float InitialVolume = 0f;
    
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _fadeSpeed = DefaultFadeSpeed;
    
    private Coroutine _currentFadeCoroutine;
    private bool _isAlarmActive;

    private void Awake()
    {
        InitializeSound();
    }

    private void OnDestroy()
    {
        if (_currentFadeCoroutine != null)
        {
            StopCoroutine(_currentFadeCoroutine);
        }
    }
    private void InitializeSound()
    {
        if (_alarmSound == null)
        {
            return;
        }
        
        _alarmSound.volume = InitialVolume;
        _alarmSound.Stop();
        _isAlarmActive = false;
    }

    public void ActivateAlarm()
    {
        if (_isAlarmActive)
        {
            return;
        }
        
        _isAlarmActive = true;
        StartFadeCoroutine(MaxVolume);
        
        if (_alarmSound.isPlaying == false)
        {
            _alarmSound.Play();
        }
    }

    public void DeactivateAlarm()
    {
        if (_isAlarmActive == false)
        {
            return;
        }
        
        _isAlarmActive = false;
        StartFadeCoroutine(MinVolume);
    }

    private void StartFadeCoroutine(float targetVolume)
    {
        if (_currentFadeCoroutine != null)
        {
            StopCoroutine(_currentFadeCoroutine);
        }
        
        _currentFadeCoroutine = StartCoroutine(FadeVolumeCoroutine(targetVolume));
    }

    private IEnumerator FadeVolumeCoroutine(float targetVolume)
    {
        while (Mathf.Approximately(_alarmSound.volume, targetVolume) == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume,_fadeSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        if (_isAlarmActive == false && Mathf.Approximately(_alarmSound.volume, MinVolume))
        {
            _alarmSound.Stop();
        }
        
        _currentFadeCoroutine = null;
    }
}
