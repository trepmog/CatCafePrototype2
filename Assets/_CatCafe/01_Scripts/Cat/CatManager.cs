using System.Collections.Generic;
using UnityEngine;

public class CatManager : Cat
{
    [SerializeField] private Spawn _spawn;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private int _catAmount = 3;

    private void Update() {
        if (GameObject.FindGameObjectsWithTag("Cat").Length < _catAmount)
        {
            Debug.Log("No cats in the scene");
            // randomly generate odd numbers from the given range
            var materialIndex = Random.Range(0, _materials.Count / 2) * 2 + 1;
            var tempCat = _spawn.spawnNewObject("Cat", Quaternion.Euler(45,0,0));
            tempCat.GetComponent<Renderer>().material = _materials[materialIndex - 1];
            tempCat.GetComponent<Cat>().materials = _materials.GetRange(materialIndex - 1, materialIndex);
        }
    }
}