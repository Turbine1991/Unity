using UnityEngine;
using System.Collections;

public class TrainerAI : Trainer { //Characteristics specific to the NPC
	public TrainerObjAI obj;

	public TrainerAI(): base("Tom") {

	}

	public override MonoBehaviour GetTrainerBaseObj() {
		return (MonoBehaviour)(Object)obj;
	}
	
	public override GameObject Instantiate(UnityEngine.Object resource) {
		return (GameObject)(Object)TrainerObjAI.Instantiate(resource);
	}
}