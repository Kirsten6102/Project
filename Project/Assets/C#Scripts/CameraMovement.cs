using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject target;
    public float followDistance;

    private Vector3 offset;
    private Vector3 targetPosition;
    public float smoothMovement;

    public bool followTarget;

    void Start()
    {
        offset = transform.position - target.transform.position;
        followTarget = true;
    }

    void Update()
    {
        if(followTarget)
        {
            transform.position = target.transform.position + offset;

            //setting value camera will move to
            targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

            //moves camera ahead if facing right or back if facing left
            if (target.transform.localScale.x > 0f)
            {
                targetPosition = new Vector3(targetPosition.x + followDistance, targetPosition.y, targetPosition.z);
            }
            else
            {
                targetPosition = new Vector3(targetPosition.x - followDistance, targetPosition.y, targetPosition.z);
            }
            
        }
        
    }

}
