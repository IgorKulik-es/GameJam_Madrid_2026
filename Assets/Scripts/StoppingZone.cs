using DG.Tweening;
using UnityEngine;

public class StoppingZone : MonoBehaviour
{
    
    
    public void ExternalUpdate(float delta)
    {
        transform.position = new Vector3(transform.position.x - delta, transform.position.y, transform.position.z);
    }
    
    public bool CheckIfPlayerIsInStoppingZone(float playerXPosition)
    {
        return (transform.position.x - 5) <= playerXPosition && (transform.position.x + 5) >= playerXPosition;
    }
}
