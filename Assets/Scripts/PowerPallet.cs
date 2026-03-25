using UnityEngine;

public class PowerPallet : Pallet
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        Object.FindAnyObjectByType<GameManager>()?.PowerPalletEaten(this);
    }
}
