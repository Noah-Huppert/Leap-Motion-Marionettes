using UnityEngine;
using System.Collections;
using Leap;

public class rightLegControll : MonoBehaviour {
	public Vector rightHandNormal;
	public double rightHandRoll;
	public double prevousRoll;
	public double rollDif;
	/*public double[] rightHandRollArray = new double[5];
	public int MAFIndex;//Moving average Filter(MAF)
	public int MAFIndexSuming;
	public double rightHandRollAverage;
	public double rightHandRollTempAverage;*/
	public float rightLegCubeY;
	
	// Use this for initialization
	void Start () {
		//MAFIndex = 0;
		//MAFIndexSuming = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Controller controller = new Controller();
		if(controller.IsConnected) //controller is a Controller object
        {
            Frame frame = controller.Frame(); //The latest frame
			
			Hand rightHand = frame.Hands.Rightmost;
			rightHandNormal = rightHand.PalmNormal;
			prevousRoll = rightHandRoll;
			rightHandRoll = RadianToDegree(rightHandNormal.Roll);
			
			rollDif = prevousRoll - rightHandRoll;
			rollDif = Mathf.Abs(((float)rollDif));
			
			/*if(MAFIndex <=4){
				rightHandRollArray[MAFIndex] = rightHandRoll;
				MAFIndex = MAFIndex + 1;
			}
			if(MAFIndex >=4){
				MAFIndex = 0;	
			}
			
			if(MAFIndexSuming <=4){
				rightHandRollTempAverage = rightHandRollTempAverage + rightHandRollArray[MAFIndexSuming];
				rightHandRollTempAverage = rightHandRollTempAverage - rightHandRollArray[4];
				rightHandRollAverage = rightHandRollTempAverage / 5;
				MAFIndexSuming = MAFIndexSuming + 1;
			}
			
			if(MAFIndexSuming >=4){
				MAFIndexSuming = 0;	
			}*/
			
			rightLegCubeY = ((float)rightHandRoll * -1) / 75;
			
			if(rollDif < 10){
				transform.localPosition = new Vector3(0.375f, rightLegCubeY, 0);
			}
			
			transform.localPosition = new Vector3(0.375f, rightLegCubeY, 0);
			
			if(frame.Hands.Count == 0){
				transform.localPosition = new Vector3(0.375f, 0, 0);
				//rightHandRollAverage = 1;
				//rightHandRollTempAverage = 1;
			}
        }
	}
	
	static double RadianToDegree(double angle) {
   		return angle * (180.0 / Mathf.PI);
	}
}