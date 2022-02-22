using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Instantiate : MonoBehaviour
{
    [SerializeField] private List<GameObject> garbage;
    [SerializeField] private List<GameObject> debuff;
    private float spawnPerMinute = 30;
    private Vector3 _startPos;
    private Vector3 _secondStartPos;
    private Vector3 scale = new Vector3(2f, 2f, 2f);
    private float currentCountGarbage = 0;
    private float currentCountDebuff = 0;
    private float _speed = 3f;
    private float _delay = 1.5f;

    
    private System.Random rand = new System.Random();


    private void Start()
    {
        StartCoroutine(EncreaseSpawnCount());
    }

    private void Update()
    {
        float side = Random.Range(1f, 3f);
        float posH = Random.Range(-5f, 5f);
        float posV = Random.Range(0.5f, 2f);
        _startPos = new Vector3(posH, posV, 15f);
        _secondStartPos = new Vector3(-15f , posV, posH);
        CreateGarbageObjectVertical();
        //CreateDebuffObjectVertical();
            //CreateObjectHorizontal();
    }

    private void CreateGarbageObjectVertical()
    {
        float angle = rand.Next(0, 90);
        var targetCountVer = Time.time * (spawnPerMinute / 60);
        while (targetCountVer > currentCountGarbage)
        {
            GameObject garbagePart = Instantiate(garbage[Random.Range(0, 3)], _startPos, Quaternion.Euler(angle,angle,angle));
            garbagePart.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);
            //garbagePart.transform.localScale = scale;
            currentCountGarbage++;
        }
    }

    private void CreateDebuffObjectVertical()
    {
        var targetCountVerDebuff = Time.time * (spawnPerMinute / 150);
        while (targetCountVerDebuff > currentCountDebuff)
        {
            GameObject debuffPart = Instantiate(debuff[Random.Range(0, 2)], _startPos, Quaternion.identity);
            debuffPart.GetComponent<GameObject>().transform.Translate(Vector3.forward * Time.deltaTime * _speed * -1, Space.World);
            currentCountDebuff++;
        }
    }
    /*private void CreateObjectHorizontal()
    {
        float angle = rand.Next(0, 90);
        var targetCountVert = Time.time * (spawnPerMinute / 60);
        while (targetCountVert > currentCount)
        {
            var garbagePart = Instantiate(garbage[Random.Range(0, 3)], _secondStartPos, Quaternion.Euler(angle,angle,angle));
            currentCount++;
        }
    }*/

    IEnumerator EncreaseSpawnCount()
    {
        yield return new WaitForSeconds(3f);
        yield return spawnPerMinute++;
        StartCoroutine(EncreaseSpawnCount());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
