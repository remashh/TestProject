using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace DefaultNamespace
{
    public interface ICreateObjFactory
    {
        ICreateObj CreateObj(object obj);
    }
}

