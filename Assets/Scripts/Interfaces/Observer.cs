using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    public void Notify(Observable observable);
}
