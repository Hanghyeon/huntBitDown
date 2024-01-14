using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caldwell.Unit;

public class Crow : AnimalBase
{
    protected override void IntroductionOther()
    {
        if (!testSkin)
            return;

        if (CurState == State.Sleep)
        {
            testSkin.color = Color.green;
            m_renderer.material = testSkin;
        }

        if (CurState == State.Alert)
        {
            testSkin.color = Color.yellow;
            m_renderer.material = testSkin;
        }

        if (CurState == State.Roaming)
        {
            testSkin.color = Color.red + Color.green * 0.5f;
            m_renderer.material = testSkin;
        }

        if (CurState == State.Engauge)
        {
            testSkin.color = Color.red;
            m_renderer.material = testSkin;
            MakeNoise();
        }

        if (CurHP <= 0)
        {
            testSkin.color = Color.white * (Color.grey * 0.2f);
            m_renderer.material = testSkin;
        }
    }

    protected void MakeNoise()
    {
        Die();
    }

    protected void Die()
    {
        //var renderers = new List<Renderer>(this.GetComponentsInChildren<Renderer>());
        //var colliders = new List<Collider>(this.GetComponentsInChildren<Collider>());

        ////off renderers
        //{
        //    var iterator = renderers.GetEnumerator();
        //    while (iterator.MoveNext())
        //    {
        //        var renderer = iterator.Current;
        //        renderer.enabled = false;
        //    }
        //}

        ////off colliders
        //{
        //    var iterator = colliders.GetEnumerator();
        //    while (iterator.MoveNext())
        //    {
        //        var collider = iterator.Current;
        //        collider.enabled = false;
        //    }
        //}

        this.gameObject.SetActive(false);
    }
}