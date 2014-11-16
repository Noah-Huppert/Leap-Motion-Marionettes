using UnityEngine;
using System.Collections;
using Leap;

public class maroneteCubeControll : MonoBehaviour {
	public Vector rightHandPos;
	public Vector rightHandNormal;
	public Vector rightHandDirection;
	public Vector3 lastKnownHandPos;
	public double rightHandRoll;
	public double rightHandPitch;
	public double rightHandYaw;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool debug = false;
		Controller controller = new Controller();
		if(controller.IsConnected) //controller is a Controller object
        {
            Frame frame = controller.Frame(); //The latest frame
			
			Hand rightHand = frame.Hands.Rightmost;
			rightHandPos = rightHand.PalmPosition;
			rightHandNormal = rightHand.PalmNormal;
			rightHandDirection = rightHand.Direction;
			rightHandRoll = RadianToDegree(rightHandNormal.Roll);
			rightHandPitch = RadianToDegree(rightHandNormal.Pitch);
			rightHandPitch = (rightHandPitch + 270) * -1;
			rightHandYaw = RadianToDegree(rightHandNormal.Yaw);
			
			if(debug == true){
				Debug.Log("Right Hand Info: \n	Right Hand Pos: " + rightHandPos + "	Right Hand Rotation Info: Roll: " + rightHandRoll + "| Pitch: " + rightHandPitch + 
					"| Yaw: " + rightHandYaw);
			}
			
			transform.localPosition = new Vector3(((rightHandPos.x / 50) * -1), rightHandPos.y / 50, rightHandPos.z / 50);
			transform.eulerAngles = new Vector3(((float)rightHandRoll), 90, ((float)rightHandPitch));
			
			if(frame.Hands.Count > 0){
				lastKnownHandPos = new Vector3(((rightHandPos.x / 50) * -1), (rightHandPos.y / 50) + 1, rightHandPos.z / 50);
			}
			
			if(frame.Hands.Count == 0){
				transform.localPosition = new Vector3(0, 6, 0);
				transform.eulerAngles = new Vector3(0, 90, 0);
			}
        }
	}
	
	static double RadianToDegree(double angle) {
   		return angle * (180.0 / Mathf.PI);
	}
}