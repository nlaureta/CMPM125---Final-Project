using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBlockTest : MonoBehaviour
{
    private bool state = false;
    [SerializeField] GameObject blockIcon;
    [SerializeField] Animator enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator changeState()
    {
        while (true)
        {
            enemy.SetBool("blocking", state);
            blockIcon.SetActive(state);
            state = !state;
            yield return new WaitForSeconds(3);
        }
    }
}
