using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : Singleton.Singleton<UIManager>
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        public GameObject endCamera;
        private const float One = 1f;

        public void WinPanel()
        {
            winPanel.SetActive(true);
            endCamera.SetActive(true);
        }

        public void LosePanel()
        {
            losePanel.SetActive(true);
        }

        private IEnumerator GameWinScale()
        {
            while (true)
            {
                yield return new WaitForSeconds(One);
                GetComponent<RectTransform>().DOScale(Vector3.one * 2.5f, One);
                yield return new WaitForSeconds(One);
                GetComponent<RectTransform>().DOScale(Vector3.one * 1.5f, One);
                
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
