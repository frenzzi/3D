
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private float RandomValue => Random.value;

    public T Spawn<T>(T prefab, Vector3 position, Vector3 scale) where T : MonoBehaviour
    {
        T newCube = Instantiate(prefab, position, Quaternion.identity);

        newCube.transform.localScale = scale;

        Renderer cubeRenderer = newCube.GetComponent<Renderer>();
        cubeRenderer.material.color = new Color(RandomValue, RandomValue, RandomValue);

        return newCube;
    }
}
