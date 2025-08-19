using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;

    public Vector2 timeInterval = new(1, 1);

    public GameObject spawnPoint;

    public GameObject target;

    public Vector2 force;

    public float arcDegrees = 45;

    public float rangeInDegrees;

    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        setCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        //ignore if game is over
        if (GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            setCooldown();
            Fire();
        }
    }

    private void Fire()
    {
        //Get prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];
        //Create bomb
        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);

        //Apply force
        Rigidbody bombRigidBody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        impulseVector.Scale(new(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, arcDegrees / 45f, 0);
        impulseVector.Normalize();        
        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f,1f), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);
        bombRigidBody.AddForce(impulseVector, ForceMode.Impulse);
    }

    //função para setar o valor de cooldown
    private void setCooldown()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }
}
