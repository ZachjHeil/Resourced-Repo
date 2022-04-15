
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
	public Transform target;// target for camera movement. This will take the player in the scene as input
	public float smooth_speed = 0.120f; //speed for the camera movement to maintain smoothness

	public Vector3 offset; //offset for the camera with respect to the player character. Kept public so it can be adjusted later

	private void FixedUpdate()
	{
		Vector3 desired_position = target.position + offset; //new camera position
		Vector3 smoothing = Vector3.Lerp(transform.position, desired_position, smooth_speed); // smoothing specs
		transform.position = smoothing; //replaces old camera position with new camera position
	}

}
