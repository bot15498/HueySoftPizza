using UnityEngine; // MonoBehavior

public class Credits : MonoBehaviour
{
  public GameObject crediterinos;

  public void ToggleCredits()
  {
    crediterinos.SetActive(!crediterinos.activeSelf);
  }
}
