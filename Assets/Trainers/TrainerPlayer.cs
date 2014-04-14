using UnityEngine;
using System.Collections;

public class TrainerPlayer : Trainer { //Characteristics specific to the player
	public TrainerObj obj;

	public TrainerPlayer(): base("Red") {
	}

	public override MonoBehaviour GetTrainerBaseObj() {
		return (MonoBehaviour)(System.Object)obj;
	}
	
	public override GameObject Instantiate(UnityEngine.Object resource) {
		var tmp = TrainerObj.Instantiate(resource);
		return (GameObject)(System.Object)tmp;
	}
}
