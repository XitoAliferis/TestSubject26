using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public TMP_Text valueText;
    public TMP_Text critText;
    public TMP_Text bonusText;

    public bool open;
    public float lerpSpeed = 10f;

    public string critLabel;
    public string neutralBonusLabel;
    public string weakToBonusLabel;
    public string resistBonusLabel;
    public string immuneToBonusLabel;
    public string reflectBonusLabel;

    void SetTo(string value,bool crit, Combatant.ElementalAffinity affinity) {
        valueText.text = value;
        critText.text = crit switch
        {
            true => critLabel,
            false => ""
        };
        bonusText.text = affinity switch {
            Combatant.ElementalAffinity.NEUTRAL => neutralBonusLabel,
            Combatant.ElementalAffinity.WEAK_TO => weakToBonusLabel,
            Combatant.ElementalAffinity.RESIST => resistBonusLabel,
            Combatant.ElementalAffinity.IMMUNE_TO => immuneToBonusLabel,
            Combatant.ElementalAffinity.REFLECT => reflectBonusLabel,
            _ => ""
        };
    }

    private Vector3 initialScale;
    void Start() {
        if (initialScale == Vector3.zero) {
            initialScale = transform.localScale;
        }
        if (!open) {
            transform.localScale = Vector3.zero;
        }
    }
    public void SetInitialScale(Vector3 scale) {
        initialScale = scale;
    }
    
    void Update() {
        transform.localScale = Vector3.Lerp(transform.localScale, open ? initialScale : new Vector3(initialScale.x, 0, initialScale.z), Time.deltaTime * lerpSpeed);
    }

    public AnimatedAction CreateInstance(string value,bool crit, Combatant.ElementalAffinity affinity) {
        this.open = false;
        var clone = Instantiate(this, this.transform.position, this.transform.rotation);
        clone.SetInitialScale(initialScale);
        clone.SetTo(value,crit,affinity);
        var action = new AnimatedAction(a => {
            clone.open = true;
            var delay = AnimatedAction.Delay(1);
            delay.onComplete = () => {
                clone.open = false;
                var otherDelay = AnimatedAction.Delay(0.5f);
                otherDelay.onComplete = () => {
                    Destroy(clone.gameObject);
                };
                otherDelay.Start();
                a.Complete();
            };
            delay.Start();
        });
        return action;
    }
}
