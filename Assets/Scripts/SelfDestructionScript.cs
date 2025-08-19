using System.Collections;
using UnityEngine;

public class SelfDestructionScript : MonoBehaviour
{
    public float Delay = 1f;
    void Start()
    {
        StartCoroutine(BeginSelfDestruction());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator BeginSelfDestruction()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
