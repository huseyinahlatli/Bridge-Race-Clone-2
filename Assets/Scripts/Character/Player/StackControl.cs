using System;
using Character.Bot;
using Character.Properties;
using DG.Tweening;
using Managers;
using Stack;
using UnityEngine;

namespace Character.Player
{
    public class StackControl : MonoBehaviour
    {
        [SerializeField] private GameObject prevStack;
        private Properties.Properties _properties;
        private GameObject _bag;
        private Color _color;
        private float _height = .65f;
        private Rigidbody _stackRigidbody;
        
        private void Start()
        {
            _properties = FindObjectOfType<Properties.Properties>();
            _bag = transform.GetChild(1).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name is "BridgePoint" or "NextStage") return;
            
            //  if(other.GetComponent<Renderer>() != null)

            if (other.GetComponent<Renderer>() != null) 
            {
                _color = other.GetComponent<Renderer>().material.color;
            }

            if (other.gameObject.CompareTag(StringCache.Stack) && _color.Equals(_properties.color) &&
                _properties.characterState == CharacterState.ClaimStack)
            {
                AddStack(other.gameObject);
            }
        }

        private void AddStack(GameObject stack)
        {
            const float zPosition = -.45f;
            var stackPosition = stack.transform.localPosition;
            var x = ((int)stackPosition.x + 8) / 4;
            var z = ((int)stackPosition.z + 16) / 4;

            _properties.currentStackSpawner.GetComponent<StackSpawner>().HasStack[x][z] = false;
            stack.transform.SetParent(_bag.transform);
            
            SoundManager.Instance.PlaySound(SoundManager.Instance.removeStackSound);
            
            if (prevStack)
            {
                _height += .65f;
                var newPosition = Vector3.zero;
                newPosition.y += _height;
                newPosition.z = zPosition;
                
                stack.transform.DOLocalMove(newPosition, .5f).SetEase(Ease.OutQuint);
            }
            
            else { stack.transform.localPosition = _bag.transform.localPosition; }

            stack.transform.localRotation = Quaternion.identity;
            stack.tag = StringCache.Untagged;
            prevStack = stack;
            _properties.targetStacks.Remove(stack);
            _properties.hasTarget = false;
        }

        public void RemoveStack()
        {
            var stackCount = _bag.transform.childCount;
            if (stackCount == 0) { return; }
            
            var topStack = _bag.transform.GetChild(stackCount - 1).gameObject;

            switch (stackCount)
            {
                case 1:
                    SoundManager.Instance.PlaySound(SoundManager.Instance.removeStackSound);
                    prevStack = null;
                    break;
                case > 1:
                    prevStack = _bag.transform.GetChild(stackCount - 2).gameObject;
                    _height -= .65f;
                    SoundManager.Instance.PlaySound(SoundManager.Instance.removeStackSound);
                    break;
                default:
                    prevStack = prevStack;
                    break;
            }

            topStack.transform.SetParent(_properties.currentStackSpawner.transform);
            topStack.transform.localPosition = _properties.currentStackSpawner.GetComponent<StackSpawner>().RandomSpawnPoint();
            // topStack.transform.DOLocalMove(_properties.currentStackSpawner.GetComponent<StackSpawner>().RandomSpawnPoint(), .5f).SetEase(Ease.InQuint);
           
            topStack.transform.localRotation = Quaternion.identity;
            topStack.tag = StringCache.Stack;
        }
    }
}
