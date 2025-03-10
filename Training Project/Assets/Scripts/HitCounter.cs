
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HitCounter : MonoBehaviour
{
    [Header("Hit Counter")]
    [Tooltip("The number that the hit points start at")]
    public float hits = 3f;

    TMP_Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponent<TMP_Text>();
        textUI.text = hits.ToString();  
    }

    public void getsHit()
    {
        if (hits > 0)  
        {
            hits--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = hits.ToString();
    }
}
