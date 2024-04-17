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

        private bool _isDeathProceeded;
        
        private void Start()
        {
            _thirdPersonController = GetComponent<OUThirdPersonController>();
            if (OUGameManager.IsPlayerDead || _thirdPersonController.isDead)
            {
                _thirdPersonController.isDead = false;
                OUGameManager.IsPlayerDead = false;
            }
            _isDeathProceeded = true;
        }

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
                OUGameUIController.Instance.EnergyAmount += 2;
            }
        }
        
        private void OnPlayerDeath()
        {
            if (!OUGameManager.IsPlayerDead && _isDeathProceeded)
            {
                _isDeathProceeded = false;
                OUGameUIController.Instance.OnGameOver();
                OUGameManager.IsPlayerDead = true;
            }
        }

        private bool _isRespawning;
        
        public void OnCheckpointRespawnClick()
        {
            _isDeathProceeded = true;
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