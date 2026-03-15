using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [System.Serializable]
    public class CollisionEvent : UnityEvent<GameObject, GameObject, Collision> { }

    [System.Serializable]
    public class TriggerEvent : UnityEvent<GameObject, GameObject, Collider> { }

    [Header("Tag Filter (leave empty to allow all)")]
    public string requiredTag;

    [HideInInspector] public bool showCollisionEnter;
    [HideInInspector] public bool showCollisionStay;
    [HideInInspector] public bool showCollisionExit;

    [HideInInspector] public bool showTriggerEnter;
    [HideInInspector] public bool showTriggerStay;
    [HideInInspector] public bool showTriggerExit;

    public CollisionEvent OnCollisionEnterEvent;
    public CollisionEvent OnCollisionStayEvent;
    public CollisionEvent OnCollisionExitEvent;

    public TriggerEvent OnTriggerEnterEvent;
    public TriggerEvent OnTriggerStayEvent;
    public TriggerEvent OnTriggerExitEvent;

    bool TagMatches(GameObject other)
    {
        if (string.IsNullOrEmpty(requiredTag))
            return true;

        return other.CompareTag(requiredTag);
    }

    void OnCollisionEnter(Collision c)
    {
        if (showCollisionEnter && TagMatches(c.gameObject))
            OnCollisionEnterEvent?.Invoke(gameObject, c.gameObject, c);
    }

    void OnCollisionStay(Collision c)
    {
        if (showCollisionStay && TagMatches(c.gameObject))
            OnCollisionStayEvent?.Invoke(gameObject, c.gameObject, c);
    }

    void OnCollisionExit(Collision c)
    {
        if (showCollisionExit && TagMatches(c.gameObject))
            OnCollisionExitEvent?.Invoke(gameObject, c.gameObject, c);
    }

    void OnTriggerEnter(Collider other)
    {
        if (showTriggerEnter && TagMatches(other.gameObject))
            OnTriggerEnterEvent?.Invoke(gameObject, other.gameObject, other);
    }

    void OnTriggerStay(Collider other)
    {
        if (showTriggerStay && TagMatches(other.gameObject))
            OnTriggerStayEvent?.Invoke(gameObject, other.gameObject, other);
    }

    void OnTriggerExit(Collider other)
    {
        if (showTriggerExit && TagMatches(other.gameObject))
            OnTriggerExitEvent?.Invoke(gameObject, other.gameObject, other);
    }
}