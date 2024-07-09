using UnityEngine;

public class PlayerStats
{
    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance ??= new PlayerStats(); } }

    public static void Wipe() => _instance = null;

    // Player stats
    public int MaxHP { get; set; } = 100; // Default value, can be modified
    public int MaxSP { get; set; } = 100; // Default value, can be modified

    private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        set => _currentHP = Mathf.Max(value, 0); // Ensures HP doesn't go below 0
    }

    private int _currentSP;
    public int CurrentSP
    {
        get => _currentSP;
        set => _currentSP = Mathf.Max(value, 0); // Ensures SP doesn't go below 0
    }

    // Initialize default stats
    private PlayerStats() {
        CurrentHP = MaxHP;
        CurrentSP = MaxSP;
    }

    // Example method to modify HP
    public void TakeDamage(int damageAmount)
    {
        CurrentHP -= damageAmount;
    }

    // Example method to modify SP
    public void UseSP(int spAmount)
    {
        CurrentSP -= spAmount;
    }

    // Used for items that increase HP
    public void GainHealth(int healthAmount)
    {
        CurrentHP += healthAmount;

        // Ensures HP cannot be greater than 100
        if (CurrentHP > 100)
        {
            CurrentHP = 100;
        }
    }

    // Used for items that increase SP
    public void GainSP(int spAmount)
    {
        CurrentSP += spAmount;

        // Ensures SP cannot be greater than 100
        if (CurrentSP > 100)
        {
            CurrentSP = 100;
        }
    }

    // Add other methods as needed...
}
