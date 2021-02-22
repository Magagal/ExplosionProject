using System.Collections;
using UnityEngine;

public class PartObject : MonoBehaviour
{
    public Rigidbody partRigidbody;
    public Vector3 direction;
    public Vector3 lastPsotion;
    public float forceParticle = 15;
    public bool exploded = false;
    public GameObject subPart;

    public float delayPartocle = 0.01f;
    public float minWaitToAddForce = 0.05f;
    public float maxWaitToAddForce = 0.2f;

    void Start()
    {
        StartCoroutine(CreateParticle());
    }

    private void Awake()
    {
        Events.OnExplodeProjectile += CheckObject;
    }

    private void OnDestroy()
    {
        Events.OnExplodeProjectile -= CheckObject;
    }

    public void CheckObject(Collider[] _colliders, float _force, Transform _position, float _rangeExplosion)
    {
        if (exploded == false)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (Object.ReferenceEquals(_colliders[i].gameObject, gameObject) == true)
                {
                    SetExplosion(_force, _position, _rangeExplosion);
                }
            }
        }
    }

    public void SetExplosion(float _force, Transform _position, float _rangeExplosion)
    {
        exploded = true;

        gameObject.gameObject.SetActive(false);

        SubPartObject subPartObject = Instantiate(subPart, transform.position, transform.rotation, transform.parent).GetComponent<SubPartObject>();

        subPartObject.SetExplosion(_force, _position, _rangeExplosion);

        Destroy(gameObject);
    }

    void OnJointBreak(float breakForce)
    {
        partRigidbody.useGravity = true;
    }

    IEnumerator CreateParticle()
    {
        yield return new WaitForSeconds(delayPartocle);

        GameObject partible = Instantiate(ObjectsHelper.Instance.woodParticles,transform.position, transform.rotation, transform);

        yield return new WaitForSeconds(Random.Range(minWaitToAddForce, maxWaitToAddForce));

        partible.AddComponent<Rigidbody>().AddForceAtPosition(direction * forceParticle, transform.position, ForceMode.Impulse);
    }

    public void Update()
    {
        direction = lastPsotion - transform.position;
        lastPsotion = transform.position;
    }
}
