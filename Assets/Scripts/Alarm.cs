using UnityEngine;

public class Alarm : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;
    private const float InitialVolume = 0f;
    
    [Header("Настройки звука")]
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _fadeSpeed = 1f;
    
    [Header("Отладка")]
    [SerializeField] private bool _showDebugMessages = true;
    
    private bool _isIntruderInside;
    private float _targetVolume;

    private void Awake()
    {
        if (_alarmSound == null)
        {
            Debug.LogError("Звук сигнализации не назначен! Перетащите AudioSource в это поле.", this);
            
            return;
        }
        
        _alarmSound.volume = InitialVolume;
        _alarmSound.Stop();
        
        if (_showDebugMessages == true)
        {
            Debug.Log("Система сигнализации инициализирована. Ожидаем жулика...", this);
        }
    }

    private void Update()
    {
        if (_alarmSound != null)
        {
            _alarmSound.volume = Mathf.MoveTowards(
                current: _alarmSound.volume, 
                target: _targetVolume, 
                maxDelta: _fadeSpeed * Time.deltaTime
            );
            
            if (_isIntruderInside == false && Mathf.Approximately(_alarmSound.volume, MinVolume))
            {
                _alarmSound.Stop();
                
                if (_showDebugMessages == true)
                {
                    Debug.Log("Звук сигнализации полностью остановлен.", this);
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CapsuleCollider))
        {
            _isIntruderInside = true;
            _targetVolume = MaxVolume;
            
            if (_alarmSound.isPlaying == false)
            {
                _alarmSound.Play();
            }
            
            if (_showDebugMessages == true)
            {
                Debug.Log("Жулик вошел! Громкость сигнализации увеличивается...", this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetType() == typeof(CapsuleCollider))
        {
            _isIntruderInside = false;
            _targetVolume = 0f;
            
            if (_showDebugMessages == true)
            {
                Debug.Log("Жулик вышел! Громкость сигнализации уменьшается...", this);
            }
        }
    }
}
