public interface Damageable
{
  public bool CanDamage(LyraProjectile lyraProjectile);
  public void NonDamagingHit();
}