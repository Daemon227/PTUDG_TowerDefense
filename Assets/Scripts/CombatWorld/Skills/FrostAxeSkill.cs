using UnityEngine;

public class FrostAxeSkill : MonoBehaviour, ISkill
{
    public int id = 2;
    public Sprite skillSprite;
    public float countdown = 5f;
    public float dame = 5f;
    public float timeEffect = 5f;
    public int cost = 25;
    public string description;
    public float CountDown => countdown;
    public int Cost => cost;

    public Sprite SkillSprite => skillSprite;

    public string Description => description;

    public int GetID => id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAI>().timeEffect = timeEffect;
            collision.GetComponent<IEnemy>().Freeze();
        }
    }

    public void EndSkill()
    {
        Destroy(this.gameObject);
    }
}
