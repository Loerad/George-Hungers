using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject garbage;
    [SerializeField]
    private int spawnCooldown = 6; //this is something to balance
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnGarbage();
            timer = spawnCooldown;
        }
    }

    void SpawnGarbage()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = -bounds.extents.y;
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        Instantiate(garbage);
        garbage.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
    }
}
