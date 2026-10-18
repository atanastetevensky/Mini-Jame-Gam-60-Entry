using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;


    private void Start()
    {
        if (_prefabs.Length == 0)
        {
            Debug.Log("Prefab array is empty.");
            return;
        }
        int randomIndex = Mathf.RoundToInt(Random.Range(0, _prefabs.Length - 1f));
        GameObject prefab = _prefabs[randomIndex];

        Instantiate(prefab, transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }
}
