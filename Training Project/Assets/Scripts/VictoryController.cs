using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VictoryController : MonoBehaviour
{
    private ParticleSystem _particles;

    [SerializeField] GameObject exit;
    // Start is called before the first frame update

    void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }
    
    void OnEnable()
    {
        GameEventDispatcher.EnemiesAllDefeated += Celebrate;
    }
    void OnDisable()
    {
        GameEventDispatcher.EnemiesAllDefeated -= Celebrate;
    }

    private void Celebrate()
    {
        _particles.Play();

        exit.SetActive(true);
    }
}
