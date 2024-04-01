using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Invector.vCharacterController
{


    public class Player : MonoBehaviour
    {
        vThirdPersonController _vThirdPersonController;

        public static Player Instance { set; get; }

        Vector3 LastCheckpointPosition;



        private void OnDead()
        {
            if (!GameManager.isPlayerDie)
            {
                GUIManager.Instance.Gameover();
                GameManager.isPlayerDie = true;
            }
        }



        private void Start()
        {
            _vThirdPersonController = GetComponent<vThirdPersonController>();
            Instance = this;


        }

        private void Update()
        {
            if (_vThirdPersonController.isDead)
            {

                OnDead();
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                GameManager.isCheckPoint = true;
                LastCheckpointPosition = other.GetComponent<Checkpoint>().CheckPointObjcet.position;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Coin"))
            {
                AudioManager.Instance.EnergySound.Play();
                Destroy(other.gameObject);
                GUIManager.Instance.EnergyInt++;
                GUIManager.Instance.UpdateEnergy();
            }

        }

        public void OnCheckpoint()
        {
            // if (Advertisements.Instance.IsRewardVideoAvailable())
            // {
            //     Implementation.Instance.ShowRewardedVideo();
            //     Time.timeScale = 1;
            //     GameManager.isPlayerDie = false;
            //     transform.position = LastCheckpointPosition;
            //     GUIManager.Instance.RespawnPanal.SetActive(false);
            // }
            // else
            // {
            //     Debug.Log("VideoAdNotAvailable");
            // }
        }
    }
}