using System.Collections.Generic;
using UnityEngine;

namespace _Custom.Effect
{
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
        }
        
        public List<Pool> pools;

        public GameObject GetPrefabByTag(string tag)
        {
            foreach (var pool in pools)
            {
                if (pool.tag == tag)
                {
                    return pool.prefab;
                }
            }

            return null;
        }


       
    }
}