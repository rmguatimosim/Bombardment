
using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float ExplosionDelay = 5f;
    public GameObject ExplosionPrefab;
    public GameObject WoodBreakingPrefab;
    public float BlastRadius = 5f;
    public int BlastDamage = 10;

    void Start()
    {
        StartCoroutine(ExplosionCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ExplosionCoroutine()
    {
        //Wait
        yield return new WaitForSeconds(ExplosionDelay);

        //Explode
        Explode();
    }

    private void Explode()
    {
        //Create Explosion
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        //Destroy platforms
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider collider in colliders)
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                HealthScript healthScript = hitObject.GetComponent<HealthScript>();
                if (healthScript != null)
                {
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius,0,1);
                    float damageRate = 1f * distanceRate;
                    int damage = (int)(damageRate * BlastDamage);
                    healthScript.health -= damage;
                    if (healthScript.health <= 0)
                    {
                        Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);
                        Destroy(collider.gameObject);
                    }
                }
            }            
        }

        //Destroy bomb
        Destroy(gameObject);
    }

}
