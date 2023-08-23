﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-22
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace AIO.UEngine
{
    public partial class YAssetProxy
    {
        public override GameObject InstGameObject(string location, Transform parent = null)
        {
            return YAssetSystem.InstGameObject(location, parent);
        }

        public override Task<GameObject> InstGameObjectTask(string location, Transform parent = null)
        {
            return YAssetSystem.InstGameObjectTask(location, parent);
        }

        public override IEnumerator InstGameObjectCO(string location, Action<GameObject> cb, Transform parent = null)
        {
            return YAssetSystem.InstGameObjectCO(location, parent, cb);
        }
    }
}