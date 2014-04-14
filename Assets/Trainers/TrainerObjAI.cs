/*
 * Unity AI Trainer object. 
 */

using UnityEngine;
using System.Collections;

public class TrainerObjAI : MonoBehaviour
{
	TrainerAI trainer;
	
	Trainer enemyTrainer = null;
	
	Pokemon currentPokemon = null;
	Vector3 trainerPos = Vector3.zero;
	enum States {Idle, InBattle, Defeated};
	States currentState = States.Idle;

	// Use this for initialization
	void Start ()
	{
		//trainer = GetComponent<Trainer>();
		trainer = new TrainerAI(this);
	}
	
	void Update(){
		switch(currentState){
			
		case States.Idle:{
			var trainerObj = Player.trainer.obj;
			Vector3 direct = trainerObj.transform.position - transform.position;
			if (direct.sqrMagnitude<10*10 && Vector3.Dot(direct, transform.forward)>0){
				
				Dialog.inDialog = true;
				Dialog.NPCobj = gameObject;
				Dialog.NPCname = "Young Trainer";
				Dialog.text = "You're a pokemon trainer right? That means we have to battle!";
				if (Dialog.doneDialog){
					Dialog.inDialog = false;
					
					currentState = States.InBattle;
					trainerPos = transform.position - direct.normalized*10;
					enemyTrainer = Player.trainer;
				}
			}
			break;}
			
		case States.InBattle:	InBattle();	break;
			
		}
	}
	
	void InBattle(){
		//move trainer to position
		Vector3 direct = trainerPos-transform.position;
		direct.y = 0;
		if (direct.sqrMagnitude>2){
			transform.rotation = Quaternion.LookRotation(direct);
			GetComponent<Animator>().SetBool("run", true);
		}else{
			if (direct.sqrMagnitude>1)	transform.position += direct;
			currentPokemon = trainer.party.GetActivePokemon();
			
			if (currentPokemon.obj!=null){
				direct = currentPokemon.obj.transform.position-transform.position;
			}else{
				var trainerObj = enemyTrainer.GetTrainerBaseObj();
				direct = trainerObj.transform.position-transform.position;
			}
			direct.y = 0;
			transform.rotation = Quaternion.LookRotation(direct);
			GetComponent<Animator>().SetBool("run", false);
			if (currentPokemon.obj==null)	trainer.ThrowPokemon(trainer.party.GetSlot(0).pokemon); //Only 1 pokemon is throwable
		}
		
		/*if (currentPokemonObj!=null){
			PokemonTrainer pokeComp = currentPokemonObj.GetComponent<PokemonTrainer>;
			if (pokeComp!=null){
				if (Player.pokemonObj!=null){
					pokeComp.AttackEnemy(Player.pokemonObj);
				}
			}
		}*/
	}
}

