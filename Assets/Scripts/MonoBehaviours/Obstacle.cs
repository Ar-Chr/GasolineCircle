using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destructionThreshold;
    [Space]
    [SerializeField] private Status[] inflictedStatuses;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > destructionThreshold)
            OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            foreach (Status inflictedStatus in inflictedStatuses)
            {
                if (inflictedStatus != null)
                    player.AddEffect(inflictedStatus);
                Destroy(gameObject);
            }
        }
    }
}
