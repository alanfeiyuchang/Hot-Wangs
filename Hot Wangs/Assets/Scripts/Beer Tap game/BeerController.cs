using UnityEngine;
using UnityEngine.UI;

public class BeerController : MonoBehaviour
{
    [SerializeField] Transform mug;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform bottomRight;

    [SerializeField] TapController tap;

    AudioManager audioScript;

    [SerializeField] Transform froth;

    [SerializeField] float fillSpeed = 1f;
    float fill = 0f;

    [SerializeField] float frothFillSpeed = .3f;
    [SerializeField] float frothReduceSpeed = .1f;
    [SerializeField] float frothToBeerRatio = .3f;
    float frothFill = 0f;

    [SerializeField] Button submit;
    [SerializeField] float fillTotal;
    [SerializeField] OverflowDetector overflow;

    private void Start()
    {
        audioScript = FindObjectOfType<AudioManager>();
        UpdateFill();
    }

    private void Update()
    {
        Vector3 newPos = transform.position;
        if (mug.rotation.z >= 0)
        {
            newPos.y = bottomLeft.position.y;
        }
        else
        {
            newPos.y = bottomRight.position.y;
        }
        transform.position = newPos;

        if (mug.rotation.eulerAngles.z <= 0)
        {
            fillSpeed = 1f / 90f * mug.rotation.eulerAngles.z + .5f;
        }
        else if (mug.rotation.eulerAngles.z <= 90)
        {
            fillSpeed = -1f / 4050f * Mathf.Pow(mug.rotation.eulerAngles.z - 45, 2) + 1;
        }
        else
        {
            fillSpeed = -1f / 90f * mug.rotation.eulerAngles.z + 1.5f;
        }

        frothFillSpeed = 1f - fillSpeed;

        if (tap.GetTapOn())
        {
            fill += fillSpeed * Time.deltaTime;
            frothFill += frothFillSpeed * Time.deltaTime;
        }

        frothFill -= frothReduceSpeed * Time.deltaTime;
        if (frothFill <= 0f)
        {
            frothFill = 0f;
        }
        else
        {
            fill += frothReduceSpeed * Time.deltaTime * frothToBeerRatio;
        }

        if (frothFill > froth.localScale.y)
        {
            if (!audioScript.IsSoundPlaying("froth"))
            {
                audioScript.PlaySound("froth");
            }
        }
        else
        {
            audioScript.StopSound("froth");
        }

        if (fill >= fillTotal && !overflow.GetOverflowed())
        {
            submit.interactable = true;
        }
        else
        {
            submit.interactable = false;
        }

        UpdateFill();

#if UNITY_EDITOR
        submit.interactable = true;
#endif
    }

    private void UpdateFill()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = fill;
        transform.localScale = newScale;

        Vector3 newFrothScale = froth.localScale;
        newFrothScale.y = frothFill;
        froth.localScale = newFrothScale;

        Vector3 newFrothPos = froth.position;
        newFrothPos.y = transform.position.y + transform.localScale.y / 2 + froth.localScale.y / 2;
        froth.position = newFrothPos;
    }

    public void ResetBeer()
    {
        fill = 0f;
        frothFill = 0f;
        UpdateFill();
    }
}
