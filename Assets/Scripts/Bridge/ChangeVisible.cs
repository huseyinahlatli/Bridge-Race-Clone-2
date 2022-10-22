using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Bridge
{
    public class ChangeVisible : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private const float Delay  = 1.5f;
        
        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            
            StartCoroutine(ChangeAlpha());
        }

        private IEnumerator ChangeAlpha()
        {
            while (true)
            {
                _meshRenderer.material.DOFade(.4f, Delay);
                yield return new WaitForSeconds(Delay);
                _meshRenderer.material.DOFade(0f, Delay);
                yield return new WaitForSeconds(Delay);
            }
        }
    }
}

