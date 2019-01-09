using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyShip;



    [SerializeField]
    private GameObject[] _PowerUps;

    [SerializeField]
    private float _TimeEnemy = 6.0f;

    [SerializeField]
    private float _TimeBuff = 10.0f;


    public bool _Spawn = false;

    public void StartSpawning() {
        StartCoroutine(EnemySpawn());
        StartCoroutine(BuffSpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (_Spawn == true)
        {
            Instantiate(_EnemyShip, _EnemyShip.transform.position, _EnemyShip.transform.rotation);
            yield return new WaitForSeconds(_TimeEnemy);

        }

    }

    IEnumerator BuffSpawn()
    {
        while (_Spawn == true)
        {
            int RandomPowerUps = Random.Range(0, 3);

            Instantiate(_PowerUps[RandomPowerUps], new Vector3(Random.Range(-9f, 9f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(_TimeBuff);

        }


    }

    public void ActivateSpawn()
    {
        _Spawn = true;
    }

    public void DeactivateSpawn()
    {
        _Spawn = false;
    }

    public void IncreaseDifficulty()
    {
        _TimeEnemy -= 0.5f;
        _TimeBuff += 1.0f;
    }

    public void ResetDifficulty()
    {
        _TimeEnemy = 6.0f;
        _TimeBuff = 10.0f;
    }




}
