 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject Prefab;
    public int poolSize;

    private Queue<GameObject> _pool;
    
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get {return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Object 풀에 새로운 오브젝트 생성 메서드
    /// </summary>
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(Prefab);
        newObject.SetActive(false);
        _pool.Enqueue(newObject);
    }

    /// <summary>
    /// 오브젝트 풀에 있는 오브젝트 반환 메서드
    /// </summary>
    /// <returns>오브젝트 풀에 있는 오브젝트</returns>
    public GameObject GetObject()
    {
        if (_pool.Count == 0) CreateNewObject();
        
        GameObject card = _pool.Dequeue();
        card.SetActive(true);
        return card;
    }

    /// <summary>
    /// 사용한 오브젝트를 오브젝트 풀로 되돌려 주는 메서드
    /// </summary>
    /// <param name="returnObject">반환할 오브젝트</param>
    public void ReturnObject(GameObject returnObject)
    {
        returnObject.SetActive(false);
        _pool.Enqueue(returnObject);
    }
}
