using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    public event Action IntruderEntered;
    public event Action IntruderExited;
    
    private void OnTriggerEnter(Collider other)
    {
        Intruder intruder = other.GetComponent<Intruder>();
        
        if (intruder != null)
        {
            IntruderEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Intruder intruder = other.GetComponent<Intruder>();
        
        if (intruder != null)
        {
            IntruderExited?.Invoke();
        }
    }
}
