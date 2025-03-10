using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {
                    //_instance = new GameObject( typeof(T) + " (singleton)").AddComponent<T>();
                    GameObject singletonObject = new GameObject(typeof(T).Name + " (singleton)");
                    _instance = singletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// If there is already an instance when this one is attempting
    /// to Awake, destroy self!
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
