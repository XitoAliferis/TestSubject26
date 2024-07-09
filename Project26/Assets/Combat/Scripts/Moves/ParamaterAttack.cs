using System;
using System.Collections.Generic;
[Serializable]
public class ParamaterAttack : Move
{
    public int damage;
    public Element element;
    //chance of critical hit (e.g. 0.2 if 20% chance of crit)
    public double critChance;
    Random rnd = new Random();
    public IEnumerable<AnimatedAction> Perform(Combatant self, List<Combatant> targets)
    {
        //TODO: animation types
        yield return self.PlayAnimation(Combatant.AnimationType.ATTACK_REGULAR);
       
        List<AnimatedAction> damageActions = new List<AnimatedAction>();
        foreach (var target in targets)
        {
            double randValue = rnd.NextDouble();
            int effectiveDamage = damage;
            bool crit = false;


            Combatant.ElementalAffinity affinity = target.GetElementalAffinities()[element];
            //attempted criticals
            if (randValue <= critChance) {
                crit = true;
                effectiveDamage = damage * 2; }
            switch (affinity) {
                //TODO: affinities
                case Combatant.ElementalAffinity.REFLECT:
                    self.AdjustHealth(-effectiveDamage);
                    effectiveDamage = 0;
                    break;
                case Combatant.ElementalAffinity.RESIST:
                    effectiveDamage = (int)(effectiveDamage*0.5);
                    break;
                case Combatant.ElementalAffinity.NEUTRAL:
                    break;
                case Combatant.ElementalAffinity.WEAK_TO:
                    effectiveDamage *=2;
                    break;
                case Combatant.ElementalAffinity.IMMUNE_TO:
                    effectiveDamage = 0;
                    break;

                default: break;
             }   
            damageActions.Add(target.PlayAnimation(Combatant.AnimationType.DAMAGED));
            damageActions.Add(target.PlayDamageIndicator(effectiveDamage, crit, affinity));
            target.AdjustHealth(-effectiveDamage);
        }
        yield return AnimatedAction.All(damageActions.ToArray());
    }
}