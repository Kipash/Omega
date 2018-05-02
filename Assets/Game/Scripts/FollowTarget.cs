using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float offset;
    
    void Update()
    {
        transform.position = 
            new Vector3(
                Mathf.Lerp(
                    transform.position.x
                    , offset + target.position.x
                    , Time.time)
                , transform.position.y
                , transform.position.z);        
    }
}
