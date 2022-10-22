using System.Collections;
using System.Collections.Generic;
using Bridge;
using Character.Player;
using Managers;
using Stack;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character.Properties
{
    public class Properties : MonoBehaviour
    {
        [Header ("Components & Variables")]
        public Color color;
        public List<GameObject> targetStacks;
        public Animator animator;
        public bool hasTarget;
        public bool isBot;
        public int index;
        private const float Delay = .3f;
        private const float EndPositionY = 27.34f;
        private const float EndPositionZ = 194.0f;
            
        [Header ("Stage GameObjects")]
        public GameObject currentStage;
        public GameObject currentStackSpawner;
        public GameObject currentTargetStacks;
        public GameObject currentBridges;
        public GameObject targetBridge;
        public CharacterState characterState;

        public void Start()
        {
            characterState = CharacterState.ClaimStack;
            targetStacks = StackManager.Instance.Stacks[index];
            
            FindCurrentStage();
            StartCoroutine(FindCurrentTargetStacks());
            hasTarget = false;
        }

        private void FindCurrentStage() // Add Components
        {
            currentStackSpawner = currentStage.transform.GetChild(1).gameObject; // Stage1
            currentBridges = currentStage.transform.GetChild(0).gameObject;

            var target = Random.Range(0, currentBridges.transform.childCount);
            targetBridge = currentBridges.transform.GetChild(target).GetChild(0).gameObject;
            currentTargetStacks = currentStage.transform.GetChild(1).gameObject;
        }

        public IEnumerator FindCurrentTargetStacks()
        {
            yield return new WaitForSeconds(Delay);
            targetStacks.Clear();
            var length = currentTargetStacks.transform.childCount;

            for (int i = 0; i < length; i++)
            {
                if (currentTargetStacks.transform.GetChild(i).GetComponent<Renderer>().material.color.Equals(color))
                {
                    targetStacks.Add(currentTargetStacks.transform.GetChild(i).gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(StringCache.NextStage))
            {
                currentStage = other.gameObject.GetComponent<ChangeState>().state;
                FindCurrentStage();

                StartCoroutine(currentStackSpawner.GetComponent<StackSpawner>().SpawnCharacterStack(index));
                StartCoroutine(FindCurrentTargetStacks());

                hasTarget = false;
            }
            
            if (other.gameObject.CompareTag(StringCache.WinPosition))
            {
                if (isBot) { SetGameLose(); }
                else { SetGameWin(); }
            }
        }

        private void SetGameLose()
        {
            if (!isBot)
                GetComponent<Move>().enabled = false;
            
            transform.GetChild(1).gameObject.SetActive(false);
            UIManager.Instance.LosePanel();
            SoundManager.Instance.PlaySound(SoundManager.Instance.loseSound);
        }
        
        private void SetGameWin()
        {
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<Move>().enabled = false;
            PlayerAnimations.Instance.Dancing();
            UIManager.Instance.WinPanel();
            SoundManager.Instance.PlaySound(SoundManager.Instance.winSound);
            
            var t = transform;
            t.position = new Vector3(t.position.x, EndPositionY, EndPositionZ);
        }
    }
}