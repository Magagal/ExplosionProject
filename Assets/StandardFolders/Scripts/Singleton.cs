using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    protected static T _instance;
    public static T Instance { get => _instance; }

    protected bool _isDestroying;
    bool _shouldRemove;

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            _shouldRemove = true;
            Destroy(gameObject);
            return;
        }

        _instance = gameObject.GetComponent<T>();
    }

    protected virtual void OnDestroy()
    {
        if (_shouldRemove) return;

        _isDestroying = true;
        _instance = default(T);
    }
}