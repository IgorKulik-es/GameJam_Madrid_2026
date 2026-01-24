using UnityEngine;
using DG.Tweening;

public enum PassengerType
{
    VISION,
    HEARING_BAD,
    SOUND_SENSITIVE,
    LIGHT_SENSITIVE,
    MOBILITY,
    CHILD,
    NUM_TYPES
}

public class Passenger: MonoBehaviour
{
    public int queuePosition;
    public PassengerType pType;

    public Passenger (PassengerType pType)
    {
        this.pType = pType;
    }
    public void MoveInQueue()
    {
        if (queuePosition > 0)
            queuePosition--;
    }
    public void AddToQueue(int position)
    {
        queuePosition = position;
    }

    public void MoveToPosition(Vector2 position, float time, bool isFinal)
    {
        if (isFinal)
            transform.DOMove(position, time).OnComplete(() => { Destroy(gameObject); });
        else
            transform.DOMove(position, time);
    }
}