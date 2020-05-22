using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonTemplate<T> : MonoBehaviour where T : SingletonTemplate<T>
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError($"Null instance for singleton of {typeof(T).ToString()}");
            }

            return m_instance;
        }
    }

    public static bool IsInitialized
    {
        get { return m_instance != null; }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (m_instance == null)
            {
                m_instance = this as T; // (T)this
            }
            else
            {
                //DestroyImmediate(gameObject);
                //Debug.LogError("Multiple instanes of singleton class");
            }
        }
    }

    protected virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T; // (T)this
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError($"Multiple instanes of singleton class of {typeof(T).ToString()}");
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this)
        {
            m_instance = null;
        }
    }
}
