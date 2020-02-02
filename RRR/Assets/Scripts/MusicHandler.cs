using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler instance = null;

    [SerializeField] private AudioSource source;

    public bool insideLoop = false;
    
    private float loopStarts = 22.154f;
    private float loopEnds = 51.692f;
    void Awake()
    {
        if(instance == null) {
            instance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!insideLoop) return;
        if (!(source.time > loopEnds)) return;
        source.time = loopStarts;
    }
}
