using UnityEngine;

public class Intruder : MonoBehaviour
{
    private const float RotationSmoothness = 10f;
    private const float MinimumMovementThreshold = 0.1f;
    private const float ZeroVelocity = 0f;
    
    [Header("Настройки движения")]
    [SerializeField] private float _moveSpeed = 5f;
    
    private Rigidbody _rigidbody;
    private Vector3 _movement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        if (_rigidbody == null)
        {
            Debug.LogError("На жулике отсутствует Rigidbody! Добавьте компонент Rigidbody.", this);
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _movement = new Vector3(horizontalInput, ZeroVelocity, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null && _movement != Vector3.zero)
        {
            Vector3 moveVelocity = _movement * _moveSpeed;
            _rigidbody.linearVelocity = new Vector3(moveVelocity.x, _rigidbody.linearVelocity.y, moveVelocity.z);
            
            if (_movement.magnitude > MinimumMovementThreshold)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSmoothness);
            }
        }
        else if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = new Vector3(ZeroVelocity, _rigidbody.linearVelocity.y, ZeroVelocity);
        }
    }
}