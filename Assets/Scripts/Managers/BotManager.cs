using UnityEngine;

namespace Managers
{
    public class BotManager : MonoBehaviour
    {
        [SerializeField] private GameObject blueBot;
        [SerializeField] private GameObject greenBot;
        [SerializeField] private GameObject yellowBot;

        private void Start()
        {
            Invoke(nameof(SetBlueBot), 0.025f);
            Invoke(nameof(SetGreenBot), 0.050f);
            // Invoke(nameof(SetYellowBot), 0.09f);
        }

        private void SetBlueBot()
        {
            blueBot.SetActive(true);
        }

        private void SetGreenBot()
        {
            greenBot.SetActive(true);
        }

        private void SetYellowBot()
        {
            yellowBot.SetActive(true);
        }
    }
}
