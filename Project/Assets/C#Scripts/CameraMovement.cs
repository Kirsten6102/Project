using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float followDistance;

    private Vector3 offsetFromPlayer;
    private Vector3 playerPosition;
    public float smoothTheMovement;

    public bool followPlayer;

    void Start()
    {
        offsetFromPlayer = transform.position - player.transform.position;
        followPlayer = true;
    }

    void Update()
    {
        if(followPlayer)
        {
            transform.position = player.transform.position + offsetFromPlayer;

            //setting value camera will move to
            playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

            //moves camera ahead if facing right or back if facing left
            if (player.transform.localScale.x > 0f)
            {
                playerPosition = new Vector3(playerPosition.x + followDistance, playerPosition.y, playerPosition.z);
            }
            else
            {
                playerPosition = new Vector3(playerPosition.x - followDistance, playerPosition.y, playerPosition.z);
            }
            
        }
        
    }

}
