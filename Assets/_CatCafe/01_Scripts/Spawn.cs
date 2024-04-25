using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    [SerializeField] public GameObject gamePrefab;
    [SerializeField] private GameObject spawnPoint;

    private void Start()
    {
        // Instantiate(gamePrefab, new Vector3(15f, 1.5f, -8.5f), Quaternion.Euler(45,0,0));
        if (gamePrefab.tag == "Cat")
        {
            Instantiate(gamePrefab, spawnPoint.transform.position + new Vector3(0, 2f, 0), Quaternion.Euler(45,0,0));
        }
        else
        {
            Instantiate(gamePrefab, spawnPoint.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
        }
    }

    public GameObject spawnNewObject()
    {
        
        return Instantiate(gamePrefab, spawnPoint.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
    }
    
    public GameObject spawnNewObject(string tag)
    {
        return Instantiate(gamePrefab, spawnPoint.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
    }

    public GameObject spawnNewObject(string tag, Quaternion rotation)
    {
        return Instantiate(gamePrefab, spawnPoint.transform.position + new Vector3(0, 2f, 0), rotation);
    }


}
