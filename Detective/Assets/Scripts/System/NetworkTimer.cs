using Photon.Pun;
using UnityEngine;
using TMPro;
using System.Collections;

public class NetworkTimer : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject _timerObj;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField, Min(0)] private byte _timerValue;

    private byte _time;

    public byte TimerValue
    {
        set
        {
            if(value > 0)
                _timerValue = value;
        }
    }

    public System.Action EndTimer;

    public byte Time
    {
        get 
        { 
            return _time; 
        }

        private set 
        { 
            _time = value; 
            if(value == 0 && PhotonNetwork.IsMasterClient)
            {
                EndTimer?.Invoke();
            }
        }
    }

    private Coroutine _timerCoroutine;

    private void Awake()
    {
        _timerObj?.SetActive(false);
        _timerText.gameObject.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(Time);
        }
        else if(stream.IsReading)
        {
            Time = (byte)stream.ReceiveNext();
            _timerText.text = string.Format("{0:00}", Time);
        }
    }

    public void StartTimer()
    {
        _timerObj?.SetActive(true);
        _timerText.gameObject.SetActive(true);
        if(_timerCoroutine == null)
        {
            _timerCoroutine = StartCoroutine(CR_Timer());
        }

    }
    public void StopTimer()
    {
        _timerObj?.SetActive(false);
        _timerText.gameObject.SetActive(false);
        if(_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    private IEnumerator CR_Timer()
    {
        if(!PhotonNetwork.IsMasterClient)
            yield break;

        Time = _timerValue;
        while(Time >= 0)
        {
            _timerText.text = string.Format("{0:00}", Time);

            yield return new WaitForSecondsRealtime(1);
            Time--;
        }
    }
}
