using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float[] timeScales;

    public float forceProjectile = 180;
    public float forceExplosion = 50;

    public bool explosive = false;

    public Transform cameraTarget;

    public Sprite spriteExplosive;
    public Sprite spriteCommun;
    public Image explosiveButton;

    public int indexObject = 0;
    public int indexTime = 0;

    public GameObject menu;

    public float delayShootTime = 0.3f;

    Coroutine coroutine;

    public Sprite[] timeSprites;
    public Image timeButtonImage;
    public Image timeImage;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.02f);

        ResetObjects();
    }

    public void SetSpawnProjectile()
    {
        menu.SetActive(false);

        RemoveAllProjectiles();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

       

        coroutine = StartCoroutine(DelayShoot());
    }

    IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(delayShootTime);

        Transform _startProjectile = ObjectsHelper.Instance.startProjectile;
        Projectile projectile = Instantiate(ObjectsHelper.Instance.prefabProjectile, _startProjectile.position, _startProjectile.rotation, ObjectsHelper.Instance.conteinerProjectiles).GetComponent<Projectile>();
        projectile.SetProjectile(forceProjectile, forceExplosion, explosive);
        ObjectsHelper.Instance.shootParticle.SetActive(true);
    }

    public void ResetObjects()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        Events.OnShakeCamera?.Invoke(false);

        menu.SetActive(true);

        PhysicsPropertyManager.Instance.SetTimeScale(false);

        PhysicsPropertyManager.Instance.timeScale = timeScales[indexTime];
        timeImage.sprite = timeSprites[indexTime];
        timeButtonImage.sprite = timeSprites[indexTime];

        RemoveAllObjects();
        RemoveAllProjectiles();

        Transform _explodeObjectPostion = ObjectsHelper.Instance.explodeObjectPostion;
        GameObject objectToExplode = Instantiate(ObjectsHelper.Instance.prefabObjectsToExplode[indexObject], _explodeObjectPostion.position, ObjectsHelper.Instance.prefabObjectsToExplode[indexObject].transform.rotation, ObjectsHelper.Instance.conteinerObjectsToExplode);
    }

    public void RemoveAllObjects()
    {
        for (int i = 0; i < ObjectsHelper.Instance.conteinerObjectsToExplode.childCount; i++)
        {
            Destroy(ObjectsHelper.Instance.conteinerObjectsToExplode.GetChild(i).gameObject);
        }
    }

    public void RemoveAllProjectiles()
    {
        ObjectsHelper.Instance.shootParticle.SetActive(false);

        for (int i = 0; i < ObjectsHelper.Instance.conteinerProjectiles.childCount; i++)
        {
            Destroy(ObjectsHelper.Instance.conteinerProjectiles.GetChild(i).gameObject);
        }
    }

    public void SetExplosive()
    {
        explosive = !explosive;

        explosiveButton.sprite = explosive == true ? spriteExplosive : spriteCommun;
    }

    public void ChangeObject()
    {
        indexObject++;

        if (indexObject >= ObjectsHelper.Instance.prefabObjectsToExplode.Length)
        {
            indexObject = 0;
        }

        ResetObjects();
    }

    public void ChangeTimeScale()
    {
        indexTime++;

        if (indexTime >= timeScales.Length)
        {
            indexTime = 0;
        }

        ResetObjects();
    }
}

public enum GameLayers
{
    Trigger = 11,
    TriggerProjectile = 12,
    Target = 13,
    Projectile = 14,
    Particles = 15,
}
