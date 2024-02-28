using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        SetAsSingleton();
    }

    protected virtual void SetAsSingleton()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
