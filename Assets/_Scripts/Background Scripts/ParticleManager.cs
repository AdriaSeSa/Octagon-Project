using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem[] enemyDeathParticles = new ParticleSystem[24];

    public void SpawnEnemyDeathParticle(Vector3 enemyPosition)
    {
        foreach (var particle in enemyDeathParticles)
        {
            if (!particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(true);
                particle.transform.position = enemyPosition;

                StartCoroutine(PlayParticle(particle));

                break;
            }
        }
    }

    private IEnumerator PlayParticle(ParticleSystem particle)
    {
        particle.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        particle.gameObject.SetActive(false);
    }
}
