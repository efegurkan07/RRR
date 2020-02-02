using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRobot : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeAnimation());
    }

    IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            animator.SetTrigger(Random.Range(0, 10) > 5f ? "Stop" : "Run");
        }
    }
}
