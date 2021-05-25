using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 _startPoint, _endPoint;
    private Vector3 _currentTarget;
    private float _speed = 4f;

    void Start()
    {
        Transform child;
        for (int i=0;i<transform.childCount;i++)
        {
            child = transform.GetChild(i);
            switch (child.name)
            {
                case "Start Position":
                    _startPoint = child.position;
                    break;
                case "End Position":
                    _endPoint = child.position;
                    break;
            }
        }

        if (_startPoint == null)
        {
            Debug.LogError("Moving Platform could not locate Start Position.");
        }
        else
        {
            transform.position = _startPoint;
        }
        if (_endPoint == null)
        {
            Debug.LogError("Moving Platform could not locate End Position.");
        }
        else
        {
            _currentTarget = _endPoint;
            if (_startPoint == _endPoint)
            {
                Debug.LogAssertion("The Start and End Positions of the Moving Platform are the same.");
            }
        }
    }
        
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        
        if (transform.position == _startPoint)
        {
            _currentTarget = _endPoint;
        }
        if (transform.position == _endPoint)
        {
            _currentTarget = _startPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}