using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Data;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PassengerQueue: MonoBehaviour
{
    private List<Passenger> queue = new List<Passenger>();
    private Vector3 startPosition;
    public GameObject[] passengerPrefabs;
    public int queueGap = 0;
    public int queueSize = 0;
    [SerializeField] private Transform door;
    [SerializeField] private float timeMove;
    private int queueLength;
    private GameObject nextPassengerObj;
    private Passenger  nextPassenger;
    private InputAction jumpAction;
    private bool isQueueMoving = false;
    private float lastMoveTime = 0;
    

    public void Start()
    {
        startPosition = transform.position;
        queueLength = 0;
        jumpAction = InputSystem.actions.FindAction("Jump");
        FormQueue(queueSize);
    }

    public void Update()
    {
        if (Time.timeSinceLevelLoad - lastMoveTime > timeMove)
            isQueueMoving = false;
        if (jumpAction.IsPressed() && !isQueueMoving)
            PopQueue();
    }

    public void AddToQueue(int type)
    {
        if (passengerPrefabs.Length > type && type >= 0)
        {
            queueLength++;
            nextPassengerObj = Instantiate(passengerPrefabs[type], (Vector3)CalcPosition(queueLength - 1), Quaternion.identity, transform);
            nextPassenger = nextPassengerObj.GetComponent<Passenger>();
            queue.Add(nextPassenger);
            nextPassenger.AddToQueue(queueLength - 1);
        }
    }

    public void FormQueue(int numPassengers)
    {
        for (int i = 0; i < numPassengers; i++)
        {
            AddToQueue(Random.Range(0, passengerPrefabs.Length));
        }
    }

    public void PopQueue()
    {
        isQueueMoving = true;
        lastMoveTime = Time.timeSinceLevelLoad;
        queueLength--;
        queue[0].MoveToPosition(door.position, timeMove, true);
        queue.RemoveAt(0);
        foreach (Passenger passenger in queue)
        {
            passenger.MoveInQueue();
            passenger.MoveToPosition(CalcPosition(passenger.queuePosition), timeMove, false);
        }
    }

    public PassengerType GetFirstInQueue()
    {
        if (queue.Count > 0)
            return queue[0].PType;
        return PassengerType.VISION;
    }
    private Vector2 CalcPosition(int index)
    {
        return new Vector2(startPosition.x - queueGap * index, startPosition.y);
    }
}