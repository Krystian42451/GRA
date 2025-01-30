using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform fireSpawn; // Zmienione z GameObject na Transform
    public float fireVelocity = 30;
    public float firePrefabLifeTime = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Firefire();
        }
    }

    private void Firefire()
    {
        GameObject fire = Instantiate(firePrefab, fireSpawn.position, Quaternion.identity);
        fire.GetComponent<Rigidbody>().AddForce(fireSpawn.forward.normalized * fireVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyFireAfterTime(fire, firePrefabLifeTime));
    }

    private IEnumerator DestroyFireAfterTime(GameObject fire, float firePrefabLifeTime)
    {
        yield return new WaitForSeconds(firePrefabLifeTime);
        Destroy(fire);
    }
}
