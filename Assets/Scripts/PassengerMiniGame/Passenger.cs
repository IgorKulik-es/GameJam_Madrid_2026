using System;
using DG.Tweening;

using UnityEngine;

namespace PassengerMiniGame
{
    [Serializable]
    public enum PassengerType
    {
        NONE,
        REGULAR,
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
        public PassengerType PType;

        public Passenger (PassengerType pType)
        {
            PType = pType;
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
}