/*
 * Unity player Trainer object. 
 */

using UnityEngine;
using System.Collections;

public class TrainerObj : MonoBehaviour
{
	TrainerPlayer trainer;

	// Use this for initialization
	void Start ()
	{
		trainer = Player.trainer; //Temporary, just focusing on getting the new code to work
		trainer.obj = this;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

