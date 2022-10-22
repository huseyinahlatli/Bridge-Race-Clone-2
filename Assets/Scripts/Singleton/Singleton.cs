using Unity.VisualScripting;
using UnityEngine;

namespace Singleton
{
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance != null) return _instance;
                    var gameObject = new GameObject { name = typeof(T).Name };
                    _instance = gameObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this.GameObject());
            }
            else
            {
                Debug.Log("Destroyed GameObject!");
                // Destroy(gameObject);
            }
        }
    }
}
