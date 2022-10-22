using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Pool;

namespace Stack
{
    public class StackSpawner : MonoBehaviour
    {
        public int stackPerCharacter;
        public int characterCount;
        public bool isStartStage;
        public List<string> stackTags;
        public bool[][] HasStack;
        public bool[] isSpawned;
        public int mapSizeX = 5;
        public int mapSizeY = 9;

        private void Start()
        {
            MapSizes();
        }
        
        private void MapSizes()
        {
            isSpawned = new bool[characterCount];
            HasStack = new bool[mapSizeX][];

            for (int i = 0; i < mapSizeX; i++)
            {
                HasStack[i] = new bool[mapSizeY];
            }

            if (isStartStage)
                StartCoroutine(SpawnStacks());
        }

        private IEnumerator SpawnStacks()
        {
            yield return null;
            for (int i = 0; i < stackPerCharacter; i++)
            {
                for (int j = 0; j < characterCount; j++)
                {
                    Vector3 spawnPosition = RandomSpawnPoint();

                    if (!spawnPosition.Equals(Vector3.zero))
                    {
                        GameObject newStack = ObjectPool.Instance.SpawnFromPool(stackTags[j]);
                        newStack.transform.SetParent(transform);
                        newStack.transform.localPosition = spawnPosition;
                    }
                }
            }
        }

        public IEnumerator SpawnCharacterStack(int index)
        {
            yield return null;

            if (!isSpawned[index])
            {
                for (int i = 0; i < stackPerCharacter; i++)
                {
                    Vector3 spawnPosition = RandomSpawnPoint();
                    
                    if (!spawnPosition.Equals(Vector3.zero))
                    {
                        GameObject newStack = ObjectPool.Instance.SpawnFromPool(stackTags[index]);
                        newStack.transform.SetParent(transform);
                        newStack.transform.localPosition = spawnPosition;
                    }
                }
                
                isSpawned[index] = true;
            }
        }

        public Vector3 RandomSpawnPoint()
        {
            int xPosition = Random.Range(-2, 3) * 4;
            int zPosition = Random.Range(-4, 5) * 4;

            Vector3 spawnPosition = new Vector3(xPosition, -0.06f, zPosition);

            int x = ((int)spawnPosition.x + 8) / 4;
            int z = ((int)spawnPosition.z + 16) / 4;

            if (HasStack[x][z])
                spawnPosition = FindSpawnPoint();
            else
                HasStack[x][z] = true;
            
            return spawnPosition;
        }

        private Vector3 FindSpawnPoint()
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    if (!HasStack[i][j])
                    {
                        HasStack[i][j] = true;
                        Vector3 position = new Vector3(4 * i - 8, -0.06f, 4 * j - 16);
                        return position;
                    }
                }
            }

            return Vector3.zero;
        }
    }
}
