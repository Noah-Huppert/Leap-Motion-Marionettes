using UnityEngine;
using System.Collections;
using Leap;

public class leapControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Controller controller = new Controller();
		if(controller.IsConnected) //controller is a Controller object
        {
                Frame frame = controller.Frame(); //The latest frame
                Frame previous = controller.Frame(1); //The previous frame
				HandList hands = frame.Hands;
			Debug.Log("# of hands: ");
			Debug.Log(hands.Count);
        }
	}
	
	// Update is called once per frame
	void Update () {
		while(1==1){
			Debug.Log("W Down");
		}
	}
}
