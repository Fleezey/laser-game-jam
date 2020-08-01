using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform m_ReflectionPosition;

    public Transform ReflectionPosition => m_ReflectionPosition;
}
