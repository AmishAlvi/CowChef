using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observable
{
    public void Subscribe(Observer observer);
}
