using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotateSpeed;
    public GameObject ShipModel;

    private Rigidbody rigidbody;
    private bool isStarted;
    private GameObject DynamicObjects;
    private TimeSpan RestartTime;
    private DateTime? collisionDate;

    private int ActualScores = 0;
    private int ActualLives = 3;

    void Start()
    {
        Aggregator.Subscribe<bool>("StartGame", StartGameAction);
        Aggregator.Subscribe<int>("AddPoints", AddPointsAction);
        Aggregator.Subscribe<bool>("Shot", Shot);

        rigidbody = this.GetComponent<Rigidbody>();
        DynamicObjects = GameObject.Find("DynamicObjects");
        isStarted = false;
        RestartTime = new TimeSpan(0, 0, 2);
    }

    private void AddPointsAction(int points)
    {
        ActualScores += points;
        Aggregator.Publish<int>("SendPointsToUI", ActualScores);
    }

    private void StartGameAction(bool obj)
    {
        ActualScores = 0;
        ActualLives = 3;
        ResetPositionAndStart();
    }

    void Update()
    {
        if (isStarted)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            this.transform.Rotate(new Vector3(0, 0, -1) * horizontal + new Vector3(1, 0, 0) * vertical, RotateSpeed * Time.deltaTime);
            ShipModel.transform.localEulerAngles = new Vector3(vertical * 10, 0, horizontal * -10);

            rigidbody.velocity = transform.rotation * Vector3.forward * Speed;
            if (Input.GetKeyDown(KeyCode.Space))            
                Shot(false);            
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            if (collisionDate != null && DateTime.Now - collisionDate > RestartTime)
            {
                collisionDate = null;
                if (ActualLives == 0)
                {
                    Aggregator.Publish<int>("GameOver", ActualScores);
                    return;
                }
                ResetPositionAndStart();
            }
        }
    }

    private void ResetPositionAndStart()
    {
        foreach (Transform child in DynamicObjects.transform)
        {
            Destroy(child.gameObject);
        }
        isStarted = true;
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;

        ShipModel.SetActive(true);
    }

    private void Shot(bool value)
    {

        var bullet = PrefabsHelper.CreatePrefab(PrefabEnum.Bullet, ShipModel.transform.position, this.transform.rotation);
        var rig = bullet.GetComponent<Rigidbody>();
        rig.velocity = transform.rotation * Vector3.forward * Speed * 20;

    }

    private void OnCollisionEnter(Collision collision)
    {
        isStarted = false;
        ShipModel.SetActive(false);
        collisionDate = DateTime.Now;
        ActualLives--;
        Aggregator.Publish<int>("SendLivesToUI", ActualLives);
    }
}
