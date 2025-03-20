using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneNavigator : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var exiter = other.transform.GetComponent<ExitArea>();

        if (exiter)
        {
            //GameEventDispatcher.TriggerSceneExited();
            SceneManager.LoadScene(exiter.GetScene());
        }
    }
    
}
