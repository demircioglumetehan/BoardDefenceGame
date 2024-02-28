using BoardDefenceGame.Enemy;
using DG.Tweening;
using UnityEngine;

namespace BoardDefenceGame.DefenceItems
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 1f;
        public void ShootBullet(BaseEnemy targetEnemy, int damage)
        {
            var distance = Vector3.Distance(transform.position, targetEnemy.transform.position);
            var lerpTime = distance / bulletSpeed;
            transform.DOMove(targetEnemy.transform.position, lerpTime).OnComplete(() =>
            {
                targetEnemy.TakeDamage(damage);
                this.gameObject.SetActive(false);
            }).SetEase(Ease.Linear);
        }
    }
}

