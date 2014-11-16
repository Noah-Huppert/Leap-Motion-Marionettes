using UnityEngine;
using System.Collections;
using Leap;

public class mainControll : MonoBehaviour {
	public GameObject headCube;
	public GameObject leftLegCube;
	public GameObject rightLegCube;
	
	public Vector rightHandPos;
	public Vector rightHandNormal;
	public Vector rightHandDirection;
	public Vector3 lastKnownHandPos;
	public double rightHandRoll;
	public double prevousRoll;
	public double prevousPitch;
	public double pitchDif;
	public double rollDif;
	public double rightHandPitch;
	public double rightHandYaw;
	//Right Leg
	public float rightLegCubeY;
	//Left Leg
	public float leftLegCubeY;
	//Head
	public double headPitchDif;
	public float headCubeY;
	
	// Use this for initialization
	void Start () {
		headCube = GameObject.Find("headCube");
		leftLegCube = GameObject.Find("leftLegCube");
		rightLegCube = GameObject.Find("rightLegCube");
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
			prevousRoll = rightHandRoll;
			rightHandRoll = RadianToDegree(rightHandNormal.Roll);
			prevousPitch = rightHandPitch;
			rightHandPitch = RadianToDegree(rightHandNormal.Pitch);
			rightHandPitch = (rightHandPitch + 90);
			rightHandYaw = RadianToDegree(rightHandNormal.Yaw);
			
			rollDif = prevousRoll - rightHandRoll;
			rollDif = Mathf.Abs(((float)rollDif));
			
			pitchDif = prevousPitch - rightHandPitch;
			pitchDif = Mathf.Abs(((float)pitchDif));
			
			if(debug == true){
				Debug.Log("Right Hand Info: \n	Right Hand Pos: " + rightHandPos + "	Right Hand Rotation Info: Roll: " + rightHandRoll + "| Pitch: " + rightHandPitch + 
					"| Yaw: " + rightHandYaw);
			}
			
			
			//Main controll cube\\
			transform.localPosition = new Vector3(((rightHandPos.x / 50) * -1), rightHandPos.y / 50, rightHandPos.z / 50);
			//transform.eulerAngles = new Vector3(((float)rightHandPitch), 180, ((float)rightHandRoll));
			
			if(frame.Hands.Count > 0){
				lastKnownHandPos = new Vector3(0, 10, 0);
				transform.localPosition = new Vector3(((rightHandPos.x / 50) * -1), rightHandPos.y / 50, rightHandPos.z / 50);
				
				//Right leg\\
				rightLegCubeY = ((float)rightHandRoll * -1) / 75;
			
				if(rollDif < 10){
					rightLegCube.transform.localPosition = new Vector3(0.375f, rightLegCubeY, 0);
				}
				//*************************\\
			
				//Left leg\\
				leftLegCubeY = ((float)rightHandRoll) / 75;
			
				if(rollDif < 10){
					leftLegCube.transform.localPosition = new Vector3(-0.375f, leftLegCubeY, 0);
				}
				//*************************\\
			
				//Head\
				headCubeY = ((float)rightHandPitch / 75)- 0.7f;
			
				if(pitchDif <10){
					headCube.transform.localPosition = new Vector3(0, headCubeY, 0);
				}
				//*************************\\
			}
			
			if(frame.Hands.Count == 0){
				transform.localPosition = new Vector3(0, 6, 0);
				rightLegCube.transform.localPosition = new Vector3(0.375f, 0, 0);
				leftLegCube.transform.localPosition = new Vector3(-0.375f, 0, 0);
				headCube.transform.localPosition = new Vector3(0, 0, 0);
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
			//*************************\\
			
			
        }
	}
	
	static double RadianToDegree(double angle) {
   		return angle * (180.0 / Mathf.PI);
	}
}