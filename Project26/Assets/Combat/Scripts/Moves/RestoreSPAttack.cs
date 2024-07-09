using System.Collections.Generic;

public class RestoreSPAttack : Move
{
	public int amount;
    public IEnumerable<AnimatedAction> Perform(Combatant self, List<Combatant> targets)
    {
        yield return self.PlayAnimation(Combatant.AnimationType.ATTACK_SKILL);
		
        PlayerStats.Instance.CurrentSP += amount;
        if (PlayerStats.Instance.CurrentSP > PlayerStats.Instance.MaxSP) { PlayerStats.Instance.CurrentSP = PlayerStats.Instance.MaxSP; }

        if (self is PlayerCombatant player) { player.UpdateAllMeters(); }
    }
}