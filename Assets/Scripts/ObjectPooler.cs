using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public List<GameObject> preFab;
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
            var coppyList = pool.preFab;
            int tempNum = 0;
            while (coppyList.Count!=0)
            {
                GameObject obj = Instantiate(pool.preFab.ToArray()[tempNum],transform.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                coppyList.RemoveAt(tempNum);
                tempNum= Random.Range(0, coppyList.Count);
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
