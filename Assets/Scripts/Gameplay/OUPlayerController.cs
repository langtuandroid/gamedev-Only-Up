using Managers;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class OUPlayerController : MonoBehaviour
    {
        private OUThirdPersonController _thirdPersonController;
        private Vector3 _lastCheckpointPosition;
        
        private void Start() => _thirdPersonController = GetComponent<OUThirdPersonController>();

        private void Update()
        {
            if (_thirdPersonController.isDead) OnPlayerDeath();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                OUGameManager.IsCheckPointChecked = true;
                _lastCheckpointPosition = other.GetComponent<OUCheckpointController>().GetCheckpointPos();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Coin"))
            {
                OUAudioManager.Instance.EnergyCollectSoundSource.Play();
                Destroy(other.gameObject);
                OUGameUIController.Instance.EnergyAmount++;
            }
        }
        
        private void OnPlayerDeath()
        {
            if (!OUGameManager.IsPlayerDead)
            {
                OUGameUIController.Instance.OnGameOver();
                OUGameManager.IsPlayerDead = true;
            }
        }
        
        public void OnCheckpointRespawnClick()
        {
             Time.timeScale = 1;
             OUGameManager.IsPlayerDead = false;
             transform.position = _lastCheckpointPosition;
             OUGameUIController.Instance.DisableRespawnPanel();
        }
    }
}