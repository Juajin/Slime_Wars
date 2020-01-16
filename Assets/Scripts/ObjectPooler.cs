using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] preFab;
    }
    public List<Pool> pools;
    public bool isPoolCreated = false;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    #region SINGLETON
    public static ObjectPooler Instance;
    #endregion
    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.preFab.Length; i++)
            {
                GameObject obj = Instantiate(pool.preFab[i],transform.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        isPoolCreated = true;
    }
    public GameObject SpawnFromPool(string tag,Vector3 position) {
        
        GameObject objectToSpawn=poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<RectTransform>().localPosition = position;
        objectToSpawn.GetComponent<RectTransform>().rotation = Quaternion.identity;
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
