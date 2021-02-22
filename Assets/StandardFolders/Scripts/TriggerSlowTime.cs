using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlowTime : MonoBehaviour
{
    Rigidbody rigidbodyTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyTrigger = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        PhysicsPropertyManager.Instance.SetTimeScale(true);
    }
}
