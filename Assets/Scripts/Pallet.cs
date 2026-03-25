using UnityEngine;

public class Pallet : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        Object.FindAnyObjectByType<GameManager>()?.PalletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
