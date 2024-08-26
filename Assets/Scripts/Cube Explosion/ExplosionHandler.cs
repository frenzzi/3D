using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public void AddExplosionForceAtPoint(Vector3 explodePosition, float explosionForce, float explosionRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(explodePosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Explode(rigidbody, explodePosition,explosionForce, explosionRadius);
            }
        }
    }

    public void AddExplosionForceTo(Rigidbody rigidbody, Vector3 explodePosition, float explosionForce, float explosionRadius)
    {
        if (rigidbody != null)
        {
            Explode(rigidbody, explodePosition, explosionForce, explosionRadius);
        }
    }

    private void Explode(Rigidbody rigidbody, Vector3 explodePosition, float explosionForce, float explosionRadius)
    {
        rigidbody.AddExplosionForce(explosionForce, explodePosition, explosionRadius);
    }
}
