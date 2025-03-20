using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;
    [SerializeField] private int entranceNumber = -1;

    public string GetScene()
    {
        return sceneToLoad;
    }

    public int GetSceneEntranceNumber()
    {
        return entranceNumber;
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
