using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatant : Combatant
{
	public int health;
	public float timeToDecideAttack = 0.5f;
	public List<ParamaterAttack> moves = new List<ParamaterAttack>();

	public DamageIndicator damageIndicator;

	public ValueMeter healthMeter;

	public override void AdjustHealth(int amount)
	{
		health += amount;
		if (health <= 0) {health = 0;}
		else if (health > maxHealth) {health = maxHealth;}
		healthMeter.Value = health;
	}
	public override int GetHealth() => health;

	private Move nextMove;
	public override AnimatedAction DecideNextMove()
	{
		nextMove = moves[Random.Range(0, moves.Count)];
		return AnimatedAction.Delay(timeToDecideAttack);
	}
	public override Move GetDecidedMove() => nextMove;
	public override List<Combatant> GetDecidedTargets() => new List<Combatant>(new Combatant[] {Manager.player});

	private Dictionary<Element, ElementalAffinity> elementalAffinities = new Dictionary<Element, ElementalAffinity>();
	public override Dictionary<Element, ElementalAffinity> GetElementalAffinities() => elementalAffinities;

	public List<Element> WeakTo = new List<Element>();
	public List<Element> Resist = new List<Element>();
	public List<Element> ImmuneTo = new List<Element>();
	public List<Element> Reflect = new List<Element>();


	void Awake(){
		foreach (var element in WeakTo) {elementalAffinities[element] = ElementalAffinity.WEAK_TO;}
		foreach (var element in Resist) {elementalAffinities[element] = ElementalAffinity.RESIST;}
		foreach (var element in ImmuneTo) {elementalAffinities[element] = ElementalAffinity.IMMUNE_TO;}
		foreach (var element in Reflect) {elementalAffinities[element] = ElementalAffinity.REFLECT;}
		foreach (var element in System.Enum.GetValues(typeof (Element))){
			if (!elementalAffinities.ContainsKey((Element)element)) {elementalAffinities[(Element)element] = ElementalAffinity.NEUTRAL;}
		}
		maxHealth = health;
		healthMeter.Max = maxHealth;
		healthMeter.Value = health;
	}

	private int maxHealth;
	public override int GetMaxHealth() => maxHealth;

	public override AnimatedAction PlayAnimation(AnimationType type)
	{
		//TODO: animations :)
		Debug.Log($"Animation: {type}");
		if (type == AnimationType.DEAD) {
			transform.position = transform.position - new Vector3(0, 100f, 0);
		}
        return type switch
        {
            AnimationType.DAMAGED => GenericShakeAnimation.Create(transform, new Vector3(1, 0, 0), 0.3f, 50, 1),
			AnimationType.ATTACK_REGULAR => GenericShakeAnimation.Create(transform, new Vector3(0, 1, 0), 0.3f, 1*Mathf.PI, 1),
			AnimationType.DEAD => AnimatedAction.Instant(),
            _ => AnimatedAction.Instant(),
        };
    }

	public override AnimatedAction PlayDamageIndicator(int amount, bool crit = false, ElementalAffinity affinity = ElementalAffinity.NEUTRAL)
	{
		Debug.Log($"Damage: {amount} {crit} {affinity}");
		//TODO: Crits, actually nice animation
		return damageIndicator.CreateInstance(amount.ToString(),crit, affinity);
	}
}