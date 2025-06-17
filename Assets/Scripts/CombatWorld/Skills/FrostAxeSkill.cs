using UnityEngine;

public class FrostAxeSkill : MonoBehaviour, ISkill
{
    public float countdown = 5f;
    public float dame = 5f;
    public float timeEffect = 5f;
    public float CountDown => countdown;

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
