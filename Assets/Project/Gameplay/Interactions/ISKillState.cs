using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ISKillState
{
   
    void OnEnterState();

    void OnExitState();

    void OntapBolt(Bolt bolt);

    void OntapHole(Hole hole);

    void OntapPlank(Plank plank);

}
