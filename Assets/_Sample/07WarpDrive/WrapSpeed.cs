using UnityEngine;
using UnityEngine.VFX;
using System.Collections;


public class WrapSpeed : MonoBehaviour
{
    public VisualEffect warpSpeedVfx;
    public MeshRenderer cylinder;

    private float rate = 0.1f;
    const string WrapAmountParamter = "WarpAmount";
    const string ShaderActiveParamter = "_Active";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        warpSpeedVfx.Stop();
        warpSpeedVfx.SetFloat(WrapAmountParamter, 0f);

        cylinder.material.SetFloat(ShaderActiveParamter, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActiveParicle(true));
            StartCoroutine(ActiveShader(true));
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(ActiveParicle(false));
            StartCoroutine(ActiveShader(false));
        }
    }

    IEnumerator ActiveParicle(bool active)
    {
        if(active)
        {
            warpSpeedVfx.Play();
            float amount = warpSpeedVfx.GetFloat(WrapAmountParamter);

            while(amount < 1f && active)
            {
                amount += rate;
                warpSpeedVfx.SetFloat(WrapAmountParamter, amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = warpSpeedVfx.GetFloat(WrapAmountParamter);

            while (amount > 0f && !active)
            {
                amount -= rate;
                warpSpeedVfx.SetFloat(WrapAmountParamter, amount);
                yield return new WaitForSeconds(0.1f);

                if(amount <= 0f+rate)
                {
                    amount = 0f;
                    warpSpeedVfx.SetFloat(WrapAmountParamter, amount);
                    warpSpeedVfx.Stop();
                }
            }
        }
    }

    IEnumerator ActiveShader(bool active)
    {
        if (active)
        {
            
            float amount = cylinder.material.GetFloat(ShaderActiveParamter);

            while (amount < 1f && active)
            {
                amount += rate;
                cylinder.material.SetFloat(ShaderActiveParamter, amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = cylinder.material.GetFloat(ShaderActiveParamter);

            while (amount > 0f && !active)
            {
                amount -= rate;
                cylinder.material.SetFloat(ShaderActiveParamter, amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0f + rate)
                {
                    amount = 0f;
                    cylinder.material.SetFloat(ShaderActiveParamter, amount);
                }
            }
        }
    }

}
