using UnityEngine;
using UnityEngine.Events;

public class vSendScore : MonoBehaviour
{
    public int displayID;
    public vShooterScore shooterScore;
    public UnityEvent onAdd;
    public void SendScore(float value)
    {
        if(shooterScore==null)
        {
            shooterScore = FindObjectOfType<vShooterScore>();
        }

        if(shooterScore!=null)
        {
            shooterScore.AddScore(new vShooterScore.ScorePoint(displayID,value));
        }
        onAdd.Invoke();
    }
}
