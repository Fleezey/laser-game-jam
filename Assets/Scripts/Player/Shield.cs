using UnityEngine;


public class Shield : MonoBehaviour
{
    public Transform ReflectionPosition => m_ReflectionPosition;

    [SerializeField] private Transform m_ReflectionPosition = null;
    [SerializeField] private GameObject m_ArmedColliders = null;
    [SerializeField] private GameObject m_DefaultCollider = null;


    private void Awake()
    {
        SetArmed(false);
    }


    public void SetArmed(bool armed)
    {
        m_ArmedColliders.SetActive(armed);
        m_DefaultCollider.SetActive(!armed);
    }
}
