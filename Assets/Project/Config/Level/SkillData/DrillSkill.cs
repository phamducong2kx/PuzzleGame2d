using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class A
{
    public virtual void helo()
    {

    }
}

public class B : A
{
    public override void helo()
    {
        A x = new B();
    }
}

public interface C
{

}