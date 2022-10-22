using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine;

namespace Character.Bot
{
    public class BotMove : Properties.Properties
    {
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] NavMeshAgent navMesh;

        [SerializeField] private Animator botAnimator;
        private const float Delay = .3f;
        private void Update()
        {
            if (!hasTarget && targetStacks.Count > 0)
            {
                ClaimStack();
                
                if (!botAnimator.GetBool(StringCache.IsRunning))
                    botAnimator.SetBool(StringCache.IsRunning,true);
            }
            
            else if (!hasTarget && targetStacks.Count == 0)
            {
                StartCoroutine(BuildBridge());
            }
        }

        private IEnumerator BuildBridge()
        {
            navMesh.SetDestination(transform.position);
            yield return new WaitForSeconds(Delay);

            if (targetStacks.Count == 0)
            {
                hasTarget = true;
                targetPosition = targetBridge.transform.position;
                navMesh.SetDestination(targetPosition);
            }
        }

        private void ClaimStack()
        {
            hasTarget = true;
            targetPosition = targetStacks[0].transform.position;
            navMesh.SetDestination(targetPosition);
        }
    }
}
