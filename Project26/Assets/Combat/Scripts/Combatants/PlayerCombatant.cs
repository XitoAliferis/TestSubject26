using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static CombatManager;

public class PlayerCombatant : Combatant
{
	public class PlayerAttack {
		public string name;
		public string description;
		public int cost;
		public Move move;
	}
	private readonly List<PlayerAttack> moves = new List<PlayerAttack>() {
		{new PlayerAttack() {
			name = "Basic Attack",
			description = "Deals slight CLOBBER damage. Low chance to crit. Pretend you're punching. ",
			cost = 0,
			move = new ParamaterAttack() {
				damage = 5,
				critChance = 0.1,
				element = Element.CLOBBER
			}}
		},
		{new PlayerAttack() {
			name = "Ignite",
			description = "Deal slight FLAME damage. They'll get the gist.",
			cost = 5,
			move = new ParamaterAttack() {
				damage = 6,
				critChance = 0.01,
				element = Element.FLAME
			}}
		},
		{new PlayerAttack() {
			name = "Splash",
			description = "Deal SOAK damage. Moderate chance to crit. Give their IPX a test.",
			cost = 17,
			move = new ParamaterAttack() {
				damage = 20,
				critChance = 0.15,
				element = Element.SOAK
			}}
		},
		{new PlayerAttack() {
			name = "Triproject",
			description = "Deal moderate RADIATE damage. Significant chance to crit.",
			cost = 33,
			move = new ParamaterAttack() {
				damage = 33,
				critChance = 0.33,
				element = Element.RADIATE
			}}
		},
		{new PlayerAttack() {
			name = "Restore",
			description = "Convert a little SP to HP.",
			cost = 10,
			move = new RestoreHPAttack() {
				amount = 30
			}
		}},
		{new PlayerAttack() {
			name = "Breathe",
			description = "Restore a little SP, at the cost of a turn. In, and out.",
			cost = 0,
			move = new RestoreSPAttack() {
				amount = 6
			}}
		},
	};

	public GameObject moveMenuEntryPrefab;
	public Transform menuPoint;
	public float itemOffset;
	public Transform theMenu;
	public TMP_Text menuDescriptionText;
	public float descriptionTargetFizz = 0.0024f;

	public DamageIndicator damageIndicator;

	public ValueMeter healthMeter;
	public ValueMeter spMeter;

	public override void AdjustHealth(int amount)
	{
		PlayerStats.Instance.TakeDamage(-amount);
		healthMeter.Value = PlayerStats.Instance.CurrentHP;
    }
    public override int GetHealth() => PlayerStats.Instance.CurrentHP;

	private AnimatedAction completeMoveDecision;
	public override AnimatedAction DecideNextMove()
	{
		return new AnimatedAction(a => {
			completeMoveDecision = a;
			enemyTargeted = 0;
			menuOpen = true;
			menuTargeting = false;
			menuDescriptionText.text = moves[menuOptionSelected].description;
			menuDescriptionText.fontMaterial.SetFloat("_Fizz", 0.5f);
		});
	}

	private bool menuOpen = false;
	private bool menuTargeting = false;
	private List<GameObject> menuOptions = new List<GameObject>();
	private int menuOptionSelected = 0; //This is horrible code and the worst way to do this but the game was supposed to ship 5 years ago.
	private int enemyTargeted = 0;
	private Vector3 menuPointOriginalPosition;
	private float originalMenuY;

	public override Move GetDecidedMove() => moves[menuOptionSelected].move;
	public override List<Combatant> GetDecidedTargets() => new List<Combatant>(new Combatant[] {Manager.enemies[enemyTargeted]});

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
		for (int i = 0; i < moves.Count; i++) {
			var newEntry = Instantiate(moveMenuEntryPrefab, menuPoint);
			newEntry.transform.position += new Vector3(0, -itemOffset * i, 0);
			newEntry.GetComponent<MenuListEntry>().Set(moves[i].name, moves[i].cost);
			menuOptions.Add(newEntry);
		}
		menuPointOriginalPosition = menuPoint.position;
		originalMenuY = theMenu.localScale.y;

		healthMeter.Max = PlayerStats.Instance.MaxHP;
		healthMeter.Value = PlayerStats.Instance.CurrentHP;
		spMeter.Max = PlayerStats.Instance.MaxSP;
		spMeter.Value = PlayerStats.Instance.CurrentSP;
	}

	void Update() {
		if (menuOpen) {
			theMenu.localScale = Vector3.Lerp(theMenu.localScale, new Vector3(theMenu.localScale.x, originalMenuY, theMenu.localScale.z), 10f * Time.deltaTime);
			menuPoint.position = Vector3.Lerp(menuPoint.position, menuPointOriginalPosition + new Vector3(0, itemOffset * menuOptionSelected, 0), 10f * Time.deltaTime);
			for (int i = 0; i < menuOptions.Count; i++) {
				menuOptions[i].transform.localPosition = Vector3.Lerp(menuOptions[i].transform.localPosition, (menuOptionSelected == i)? new Vector3(0,menuOptions[i].transform.localPosition.y,-0.4f) : new Vector3(0,menuOptions[i].transform.localPosition.y,0), 10f * Time.deltaTime);
				menuOptions[i].transform.localScale = Vector3.Lerp(menuOptions[i].transform.localScale, (!menuTargeting || menuOptionSelected == i)? new Vector3(0.71f,0.71f,0.71f) : new Vector3(0.71f,0f,0.71f), 20f * Time.deltaTime);
			}
			menuDescriptionText.fontMaterial.SetFloat("_Fizz", Mathf.Lerp(menuDescriptionText.fontMaterial.GetFloat("_Fizz"), descriptionTargetFizz, 10f * Time.deltaTime));
			if (menuTargeting) {
                for (int i = 0; i < Manager.enemyUIControllers.Count; i++) {
                    MonsterUIController enemyUI = Manager.enemyUIControllers[i];
                    enemyUI.Targeted = (enemyTargeted == i);
				}
			} else {
				foreach (var enemyUI in Manager.enemyUIControllers) {
					enemyUI.Targeted = false;
				}
			}
		} else {
			theMenu.localScale = Vector3.Lerp(theMenu.localScale, new Vector3(theMenu.localScale.x, 0, theMenu.localScale.z), 10f * Time.deltaTime);
		}
	}

	public void InputMenuSelection(InputAction.CallbackContext context) {
		if (!menuOpen) {return;}
		if (context.performed) {
			menuOptionSelected = (menuOptionSelected - (int)context.ReadValue<float>()) % menuOptions.Count;
			if (menuOptionSelected < 0) {menuOptionSelected += menuOptions.Count;}
			menuDescriptionText.text = moves[menuOptionSelected].description;
			menuDescriptionText.fontMaterial.SetFloat("_Fizz", 0.5f);
		}
	}
	public void InputMenuNext(InputAction.CallbackContext context) {
		if (!menuOpen) {return;}
		if (context.performed) {
			if (!menuTargeting) {
				menuTargeting = true;
				Manager.SetCameraPosition(CameraPosition.FOCUS_ENEMY);
			} else {
				if (moves[menuOptionSelected].cost > PlayerStats.Instance.CurrentSP) {
					return;
				}
				PlayerStats.Instance.UseSP(moves[menuOptionSelected].cost);
				spMeter.Value = PlayerStats.Instance.CurrentSP;
				menuOpen = false;
				menuTargeting = false;
				foreach (var enemyUI in Manager.enemyUIControllers) {
					enemyUI.Targeted = false;
				}
				completeMoveDecision.Complete();
			}
		}
	}
	public void InputMenuBack(InputAction.CallbackContext context) {
		if (!menuOpen) {return;}
		if (context.performed) {
			if (menuTargeting) {
				menuTargeting = false;
				Manager.SetCameraPosition(CameraPosition.DEFAULT);
			}
		}
	}
	public void InputMenuLeftRight(InputAction.CallbackContext context) {
		if (!menuOpen) {return;}
		if (context.performed) {
			if (!menuTargeting) {
				if (context.ReadValue<float>() > 0) {
					menuTargeting = true;
					Manager.SetCameraPosition(CameraPosition.FOCUS_ENEMY);
				}
			} else {
				enemyTargeted = (enemyTargeted + (int)context.ReadValue<float>()) % Manager.enemies.Count;
				if (enemyTargeted < 0 || (Manager.enemies.Count == 1 && (int)context.ReadValue<float>() < 0)) {
					enemyTargeted = 0;
					menuTargeting = false;
					Manager.SetCameraPosition(CameraPosition.DEFAULT);
				}
			}
		}
	}

	public override int GetMaxHealth() => PlayerStats.Instance.MaxHP;

	public override AnimatedAction PlayAnimation(AnimationType type)
	{
		//TODO: animations :)
		Debug.Log($"Animation: {type}");
		if (type == AnimationType.DEAD) {
			spMeter.zeroIsErr = true;
			spMeter.Value = 0;
			return AnimatedAction.Instant();
		}
        return type switch
        {
            AnimationType.DAMAGED => GenericShakeAnimation.Create(transform, new Vector3(1, 0, 0), 0.1f, 50, 1),
			AnimationType.ATTACK_REGULAR => GenericShakeAnimation.Create(transform, new Vector3(0, 1, 0), 0.3f, 1*Mathf.PI, 1),
            _ => AnimatedAction.Instant(),
        };
    }

	public override AnimatedAction PlayDamageIndicator(int amount, bool crit = false, ElementalAffinity affinity = ElementalAffinity.NEUTRAL)
	{
		Debug.Log($"Damage: {amount} {crit} {affinity}");
		//TODO: Crits, actually nice animation
		return damageIndicator.CreateInstance(amount.ToString(), crit, affinity);
	}

	public void UpdateAllMeters() {
		healthMeter.Value = PlayerStats.Instance.CurrentHP;
		spMeter.Value = PlayerStats.Instance.CurrentSP;
	}
}