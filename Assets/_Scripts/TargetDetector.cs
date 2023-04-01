using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private ParticleSystem confettiParticleSystem;
    [SerializeField] private float confettiDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Stop the ball
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ballRigidbody.isKinematic = true;

            // Hide the ball
            Renderer ballRenderer = other.gameObject.GetComponent<Renderer>();
            ballRenderer.enabled = false;

            // Instantiate and play the confetti particle system
            ParticleSystem confetti = Instantiate(confettiParticleSystem, transform.position, Quaternion.identity);
            confetti.Play();
            Destroy(confetti.gameObject, confettiDuration);
            Invoke("ReloadScene", 1.5f);
        }
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
