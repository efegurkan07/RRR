using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform transform;
 
    // Desired duration of the shake effect
    private float _shakeDuration = 0f;
 
    // A measure of magnitude for the shake. Tweak based on your preference
    private float _shakeMagnitude = 0.7f;
 
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
 
    // The initial position of the GameObject
    Vector3 initialPosition;
    
    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    
    void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * _shakeMagnitude;
   
            _shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    
    public void TriggerShake(float shakeDuration, float shakeMagnitude) {
        _shakeDuration = shakeDuration;
        _shakeMagnitude = shakeMagnitude;
    }
}
