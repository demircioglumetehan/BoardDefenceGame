using System.Collections.Generic;
using UnityEngine;

namespace BoardDefenceGame.ObjectPooler
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Fields
        private List<GameObject> pooledObjects = new List<GameObject>();
        [SerializeField] private GameObject objectToPool;
        #endregion

        #region Public Methods
        public void SetPoolingObject(GameObject poolingObject)
        {
            objectToPool = poolingObject;
        }
        public GameObject GetPooledObject(Vector3 spawnPosition)
        {
            GameObject gettingObject = null;
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    gettingObject = pooledObjects[i];
                    break;
                }
            }
            if (gettingObject == null)
            {
                gettingObject = GenerateExtraObject();
            }
            gettingObject.SetActive(true);
            var returningobjinterface = gettingObject.GetComponentsInChildren<IPooledObject>();
            foreach (var item in returningobjinterface)
            {
                item.OnSpawnObject(this);
            }
            gettingObject.transform.position = spawnPosition;
            return gettingObject;
        }

        public GameObject GenerateExtraObject()
        {
            GameObject generatedObject = Instantiate(objectToPool, this.transform);
            generatedObject.SetActive(false);
            pooledObjects.Add(generatedObject);
            return generatedObject;
        }

        public void GeneratePoolObjects(int amount)
        {
            GameObject generatedObject;
            for (int i = 0; i < amount; i++)
            {
                generatedObject = Instantiate(objectToPool, this.transform);
                generatedObject.SetActive(false);
                pooledObjects.Add(generatedObject);
            }
        }
        #endregion

    }
}

