using System.Collections.Generic;
using UnityEngine;
using Singleton;

namespace Stack
{
    public class StackManager : MonoBehaviour
    {
        public List<GameObject>[] Stacks;

        public static StackManager Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            Stacks = new List<GameObject>[4];

            for (int i = 0; i < 4; i++)
            {
                Stacks[i] = new List<GameObject>();
            }
        }
    }
}
