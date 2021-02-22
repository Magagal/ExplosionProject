using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    Coroutine coroutine;
    public CinemachineFreeLook cinemachineFree;
    public float frequency = 1;
    public float amplitude = 20;

    public float timeShake = 2;

    CinemachineBasicMultiChannelPerlin channelPerlin;

    private void Awake()
    {
        Events.OnShakeCamera += DoShake;
    }

    public void Start()
    {
        channelPerlin = cinemachineFree.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnDestroy()
    {
        Events.OnShakeCamera -= DoShake;
    }

    public void DoShake(bool _shake)
    {
        if (cinemachineFree != null)
        {

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            if (_shake == true)
            {
                StartCoroutine(Shake());
            }
            else
            {
                StopShake();
            }
        }
    }

    IEnumerator Shake()
    {
        channelPerlin.m_AmplitudeGain = amplitude;
        channelPerlin.m_FrequencyGain = frequency;


        yield return new WaitForSeconds(timeShake);

        StopShake();
    }

    public void StopShake()
    {
        channelPerlin.m_AmplitudeGain = 0;
        channelPerlin.m_FrequencyGain = 0;
    }
}
