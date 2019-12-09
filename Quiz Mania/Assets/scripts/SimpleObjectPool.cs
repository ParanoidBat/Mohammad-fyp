using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    // the prefab to be returned
    public GameObject prefab;

    // Stack inactive instances
    private Stack<GameObject> inactiveObjects = new Stack<GameObject>();

    public GameObject GetObject()
    {
        GameObject newGameObject; //instance of the prefab

        if (inactiveObjects.Count > 0) // return any inactive instance from stack
        {
            newGameObject = inactiveObjects.Pop();
        }

        // else, create a new instance to return
        else
        {
            newGameObject = (GameObject)GameObject.Instantiate(prefab);

            // add the PooledObject component to the prefab
            PooledObject pooledObject = newGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        // enable
        newGameObject.SetActive(true);
        
        return newGameObject;
    }

    public void ReturnObject(GameObject objectToBeReturned) //return object to the pool
    {
        PooledObject pooledObject = objectToBeReturned.GetComponent<PooledObject>();

        // if the instance came from this pool, return it
        if (pooledObject != null && pooledObject.pool == this)
        {
            // disable
            objectToBeReturned.SetActive(false);

            // store with inactive instances
            inactiveObjects.Push(objectToBeReturned);
        }
        // else destroy
        else
        {
            Destroy(objectToBeReturned);
        }
    }
}

// to identify the pool an object came from
public class PooledObject : MonoBehaviour
{
    public SimpleObjectPool pool;
}