using System.Collections;
using Invector.vCharacterController;
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
                OUAudioManager.Instance.EnergyCollectSound();
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

        private bool _isRespawning;
        
        public void OnCheckpointRespawnClick()
        {
            if (_isRespawning) return;
             Time.timeScale = 1;
             _thirdPersonController.isDead = false;
             OUGameManager.IsPlayerDead = false;
             transform.position = _lastCheckpointPosition;
             StartCoroutine(MoveTopCheckpointPos());
        }

        private IEnumerator MoveTopCheckpointPos()
        {
            _isRespawning = true;
            yield return new WaitForSeconds(0.03f);
            yield return new WaitForFixedUpdate();
            yield return new WaitForEndOfFrame();
            _thirdPersonController.GetComponent<vThirdPersonInput>().SetLockAllInput(false);
            StartCoroutine(_thirdPersonController.GetComponent<vThirdPersonInput>().CharacterInit());
            _thirdPersonController.Init();
            yield return new WaitForSeconds(0.5f);
            OUGameUIController.Instance.DisableRespawnPanel();
            _isRespawning = false;
        }
    }
}