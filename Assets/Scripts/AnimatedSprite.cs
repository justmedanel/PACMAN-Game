using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]//nu pot avea un animatedsprite daca nu avem un spriterenderer
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;//0.25 secunde pentru a schimba intre sprites
    public int animationFrame { get; private set; }//private set pentru ca utilizatorul poate vedea animatiile dar nu le poate seta cum vrea el
    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }
    private void Advance()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }
        this.animationFrame++;
        if(this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        if (this.animationFrame >= 0 & this.animationFrame < this.sprites.Length) {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}


