using System.Collections;
using System.Collections.Generic;

public interface Move {
	public abstract IEnumerable<AnimatedAction> Perform(Combatant self, List<Combatant> targets);
}