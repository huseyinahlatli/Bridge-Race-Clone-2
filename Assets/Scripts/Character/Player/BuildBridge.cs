using UnityEngine;

namespace Character.Player
{
    public class BuildBridge : MonoBehaviour
    {
        public Transform bag;
        public Properties.Properties properties;
        public StackControl stackControl;
            
        private void Update()
        {
            BuildTheBridge();
        }

        private void BuildTheBridge()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag(StringCache.Step))
                {
                    MeshRenderer hitMR = hit.transform.GetComponent<MeshRenderer>();
                    BoxCollider hitBox = hit.transform.GetChild(0).GetComponent<BoxCollider>();
                
                    if (hitMR != null)
                    {
                        if (bag.childCount > 0)
                        {
                            hitBox.enabled = false;

                            if (!hitMR.enabled)
                            {
                                hitMR.enabled = true;
                            }

                            if (!hitMR.material.color.Equals(properties.color))
                            {
                                hitMR.material.color = properties.color;
                                stackControl.RemoveStack();
                            }
                        }
                        if (bag.childCount == 0)
                        {
                            StartCoroutine(properties.FindCurrentTargetStacks());
                            
                            properties.hasTarget = false;
                            if (!properties.isBot)
                            {
                                if (!hitMR.material.color.Equals(properties.color))
                                {
                                    hitBox.enabled = true;
                                    gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
