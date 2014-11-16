using UnityEngine;
using System.Collections;
using Leap;

public class headControll : MonoBehaviour {
public Vector rightHandPos;
	public Vector rightHandNormal;
	public double rightHandPitch;
	public double prevousPitch;
	public double pitchDif;
	public float headCubeY;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Controller controller = new Controller();
		if(controller.IsConnected) //controller is a Controller object
        {
            Frame frame = controller.Frame(); //The latest frame
			
			Hand rightHand = frame.Hands.Rightmost;
			rightHandNormal = rightHand.PalmNormal;
			prevousPitch = rightHandPitch;
			rightHandPitch = RadianToDegree(rightHandNormal.Pitch);
			rightHandPitch = (rightHandPitch + 90);
			
			pitchDif = prevousPitch - rightHandPitch;
			pitchDif = Mathf.Abs(((float)pitchDif));
			
			headCubeY = ((float)rightHandPitch / 50);
			
			if(pitchDif <10){
				transform.localPosition = new Vector3(0, headCubeY, 0);
			}
				
			if(frame.Hands.Count == 0){
				transform.localPosition = new Vector3(0, 0, 0);
			}
        }
	}
	
	static double RadianToDegree(double angle) {
   		return angle * (180.0 / Mathf.PI);
	}
}