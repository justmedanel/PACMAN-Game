using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pallets;
    public ScoreManager scoreManager; // Assign in Inspector

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start() => NewGame();
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pallet in pallets) pallet.gameObject.SetActive(true);
        ResetState();
    }

    private void ResetState()
    {
        ghostMultiplier = 1;
        foreach (var ghost in ghosts) ghost.ResetState();
        pacman.ResetState();
    }

    private void GameOver()
    {
        foreach (var ghost in ghosts) ghost.gameObject.SetActive(false);
        pacman.gameObject.SetActive(false);
    }

    // --------------- Score/Lives Methods ---------------
    private void SetScore(int newScore)
    {
        score = newScore;
        if (scoreManager != null) scoreManager.SetScore(score);
    }

    private void SetLives(int newLives) => lives = newLives;

    // --------------- Game Events ---------------
    public void GhostEaten(Ghost ghost)
    {
        // Multiply ghost points by the current multiplier
        int points = ghost.points * ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++; // Increase multiplier for next ghost
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(lives - 1);
        if (lives > 0) Invoke(nameof(ResetState), 3f);
        else GameOver();
    }

    public void PalletEaten(Pallet pallet)
    {
        pallet.gameObject.SetActive(false);
        SetScore(score + pallet.points);

        if (!HasRemainingPallets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPalletEaten(PowerPallet pallet)
    {
        foreach (var ghost in ghosts) ghost.frightened.Enable(pallet.duration);
        PalletEaten(pallet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pallet.duration);
    }

    // --------------- Helpers ---------------
    private bool HasRemainingPallets()
    {
        foreach (Transform pallet in pallets)
            if (pallet.gameObject.activeSelf) return true;
        return false;
    }

    private void ResetGhostMultiplier() => ghostMultiplier = 1;
}
