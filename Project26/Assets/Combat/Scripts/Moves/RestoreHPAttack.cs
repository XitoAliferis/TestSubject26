using System.Collections.Generic;

public class RestoreHPAttack : Move
{
	public int amount;
	public bool allowOverheal = false;
    public IEnumerable<AnimatedAction> Perform(Combatant self, List<Combatant> targets)
    {
        yield return self.PlayAnimation(Combatant.AnimationType.ATTACK_SKILL);
		if (!allowOverheal && self.GetHealth() > self.GetMaxHealth()) {
			yield break;
		}
		self.AdjustHealth(amount);
		if (!allowOverheal) {
			if (self.GetHealth() > self.GetMaxHealth()) {
				self.AdjustHealth(-self.GetHealth() + self.GetMaxHealth());
			}
		}
    }
}