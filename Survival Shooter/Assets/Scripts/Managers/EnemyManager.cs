using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Start ()
    {
        //mengeksekusi fungsi Spawn setiap beberapa detik sesuai dengan nilai spawnTme
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        //jika player telah mati maka tidak membuat enemy baru
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //mendapatkan nilai random
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0, 3);

        //menduplikasi enemy
        Factory.FactoryMethod(spawnEnemy);
    }
}
