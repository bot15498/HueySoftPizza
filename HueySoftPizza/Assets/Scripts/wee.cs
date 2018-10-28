using UnityEngine; // MonoBehavior
using UnityEngine.UI; // Color

public class wee : MonoBehaviour
{
  public float weeMinAlpha;
  public float weeMaxAlpha;
  public float weeTime;

  private float weeTimer;
  enum WeeDirection { Positive, Negative };
  private WeeDirection weeDirection;

  public GameObject weeWee;

  void Update()
  {
    if (weeDirection == WeeDirection.Positive)
      weeTimer += Time.deltaTime;
    else
      weeTimer -= Time.deltaTime;

    if (weeTimer > weeTime)
      weeDirection = WeeDirection.Negative;
    else if (weeTimer < 0f)
      weeDirection = WeeDirection.Positive;

    Color weeColor = weeWee.GetComponent<Image>().color;
    weeColor.a = Mathf.Lerp(weeMinAlpha, weeMaxAlpha, (weeTimer / weeTime));
    weeWee.GetComponent<Image>().color = weeColor;
  }
}
