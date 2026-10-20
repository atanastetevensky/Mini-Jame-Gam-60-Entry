using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;


    private void Start()
    {

        if (_prefabs.Length == 0)
        {
            Debug.Log("Prefab spawner array is empty.");
            Destroy(this.gameObject);
            return;
        }
        int randomIndex = Mathf.RoundToInt(Random.Range(0, _prefabs.Length - 1f));
        GameObject prefab = _prefabs[randomIndex];

        GameObject instance = Instantiate(prefab, this.transform.position, Quaternion.identity, transform.parent);

        Destroy(this.gameObject);
    }
}
