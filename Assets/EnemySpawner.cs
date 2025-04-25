using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E)) 
       {
        spawnEnemy();
       }
    }

    private void spawnEnemy()
    {
        Instantiate(EnemyPrefab, gameObject.transform.position, Quaternion.identity);
    }


}
