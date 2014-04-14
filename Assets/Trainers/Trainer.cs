using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Trainer : Target {
	public string name;

	public PokeParty party;
	public Inventory inventory;
	public Inventory.Item item {get{return inventory.selected;} set{}}

	Vector3 velocity = Vector3.zero;

	public Trainer(string name): base() { //Will give to both the player and the npc (just like in multiplayer)
		this.name = name;

		party = new PokeParty(this);
		inventory = new Inventory(this);

		//kanto starters, why not
		party.AddPokemon(new Pokemon(1, true));
		party.AddPokemon(new Pokemon(4, true));
		party.AddPokemon(new Pokemon(7, true));
		Pokedex.states [1] = Pokedex.State.Captured;
		Pokedex.states [4] = Pokedex.State.Captured;
		Pokedex.states [7] = Pokedex.State.Captured;
		
		inventory.Add(1, 5); //New inventory code references shared item data. (id, quantity)
		inventory.Add(4, 2);
	}

	public override Target.TARGETS GetTargetType() {
		return Target.TARGETS.TRAINER;
	}

	public void ThrowPokemon(Pokemon poke){
		if (poke.thrown)	return;

		var trainerObj = GetTrainerBaseObj();

		poke.thrown = true;
		GameObject ball = Instantiate(Resources.Load("Pokeball"));
		ball.transform.position = trainerObj.transform.position;
		ball.rigidbody.AddForce( (trainerObj.transform.forward*2+ trainerObj.transform.up)*400 );
		ball.GetComponent<Pokeball>().pokemon = poke;
		ball.GetComponent<Pokeball>().trainer = this;
		//gamegui.SetChatWindow(ball.GetComponent<Pokeball>().pokemon.GetName() + "! I choose you!");
	}

	public void SetVelocity(Vector3 vel){
		velocity = vel;

		var trainerObj = GetTrainerBaseObj();

		Animator ani = trainerObj.GetComponent<Animator>();

		if (vel.magnitude>0.1f){
			ani.SetBool("run",true);
			trainerObj.transform.rotation = Quaternion.LookRotation(vel);
		}else{
			ani.SetBool("run",false);
		}
	}

	public virtual void Update() {

	}

	public abstract MonoBehaviour GetTrainerBaseObj(); //The base allows generalisation of the commonly used transform class, if the trainer object is needed though trainer, cast it into the object you need
	public abstract GameObject Instantiate(UnityEngine.Object resource);
}