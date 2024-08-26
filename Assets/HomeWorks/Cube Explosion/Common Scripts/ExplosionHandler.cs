using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 5.0f;
    [SerializeField] private float _explosionRadius = 2.0f;

    public void AddExplosionForceAtPoint(Vector3 explodePosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explodePosition, _explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Explode(rigidbody, explodePosition);
            }
        }
    }

    public void AddExplosionForceTo(Rigidbody rigidbody, Vector3 explodePosition)
    {
        if (rigidbody != null)
        {
            Explode(rigidbody, explodePosition);
        }
    }

    private void Explode(Rigidbody rigidbody, Vector3 explodePosition)
    {
        rigidbody.AddExplosionForce(_explosionForce, explodePosition, _explosionRadius);
    }
}
