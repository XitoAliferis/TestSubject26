using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour {
	public abstract int GetHealth();
	public abstract int GetMaxHealth();

	public abstract void AdjustHealth(int amount);

	public enum ElementalAffinity {
		NEUTRAL, WEAK_TO, RESIST, IMMUNE_TO, REFLECT
	}
	public abstract Dictionary<Element, ElementalAffinity> GetElementalAffinities();

	public enum AnimationType {
		ATTACK_REGULAR, ATTACK_CRIT, ATTACK_SKILL, ATTACK_SKILL_GROUP, SKILL_SELF, USE_ITEM,
		DAMAGED, DEAD
	}
	public abstract AnimatedAction PlayAnimation(AnimationType type);

	public abstract AnimatedAction PlayDamageIndicator(int amount, bool crit = false, ElementalAffinity affinity = ElementalAffinity.NEUTRAL);

	public CombatManager Manager { get; set; }

	public abstract AnimatedAction DecideNextMove(); //1. Wait for a move to be decided.
	public abstract Move GetDecidedMove(); //2. Get the decided move.
	public abstract List<Combatant> GetDecidedTargets(); //3. Contemplate life decisions.
}