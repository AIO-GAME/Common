/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-11
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if SUPPORT_UNITASK
#define UNITASK
#endif

#if SUPPORT_YOOASSET
#define YOOASSET
#endif

#if UNITASK
using ATask = Cysharp.Threading.Tasks.UniTask;
using ATaskObject = Cysharp.Threading.Tasks.UniTask<UnityEngine.Object>;
using ATaskGameObject = Cysharp.Threading.Tasks.UniTask<UnityEngine.GameObject>;
using ATaskObjectArray = Cysharp.Threading.Tasks.UniTask<UnityEngine.Object[]>;
using ATaskScene = Cysharp.Threading.Tasks.UniTask<UnityEngine.SceneManagement.Scene>;
using ATaskString = Cysharp.Threading.Tasks.UniTask<System.String>;
using ATaskByteArray = Cysharp.Threading.Tasks.UniTask<System.Byte[]>;
using ATaskBoolean = Cysharp.Threading.Tasks.UniTask<System.Boolean>;
#else
using System.Threading.Tasks;
using ATask = System.Threading.Tasks.Task;
using ATaskObject = System.Threading.Tasks.Task<UnityEngine.Object>;
using ATaskGameObject = System.Threading.Tasks.Task<UnityEngine.GameObject>;
using ATaskObjectArray = System.Threading.Tasks.Task<UnityEngine.Object[]>;
using ATaskScene = System.Threading.Tasks.Task<UnityEngine.SceneManagement.Scene>;
using ATaskString = System.Threading.Tasks.Task<System.String>;
using ATaskByteArray = System.Threading.Tasks.Task<System.Byte[]>;
using ATaskBoolean = System.Threading.Tasks.Task<System.Boolean>;
#endif
#if YOOASSET
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;
using YooAsset;

namespace AIO.UEngine
{
    internal partial class YAssetSystem
    {
        private static bool LoadCheckNet(UnityWebRequest operation)
        {
            switch (operation.result)
            {
                case UnityWebRequest.Result.InProgress:
                    Debug.LogError("请求正在进行中");
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("无法连接到服务器");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("服务器返回响应错误");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("数据处理异常");
                    break;
                default:
                    Debug.LogError("未知错误");
                    break;
                case UnityWebRequest.Result.Success:
                    break;
            }
#if UNITY_2020_3_OR_NEWER
            if (operation.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(operation.error);
                return false;
            }
#else
        if (operation.isHttpError || operation.isNetworkError)
        {
            UnityEngine.Debug.LogError(operation.error);
            return false;
        }
#endif

            if (!operation.isDone)
            {
                Debug.LogError("请求未完成");
                return false;
            }

            return true;
        }

        private static async ATaskBoolean LoadCheckOPTask(OperationHandleBase operation)
        {
            if (!operation.IsValid)
            {
                Print.Error(operation.LastError);
                return false;
            }

            await operation.Task;
            if (operation.Status != EOperationStatus.Succeed)
            {
                Print.Error(operation.LastError);
                return false;
            }

            return true;
        }

        private static bool LoadCheckOPSync(OperationHandleBase operation)
        {
            if (!operation.IsValid)
            {
                Print.ErrorFormat("操作句柄失效 -> {0}", operation.LastError);
                return false;
            }

            if (operation.Status == EOperationStatus.Failed)
            {
                Print.ErrorFormat("资源加载失败 -> {0}", operation.LastError);
                return false;
            }

            return true;
        }

        private static IEnumerator LoadCheckOPCO(OperationHandleBase operation, Action<bool> cb)
        {
            if (!operation.IsValid)
            {
                Print.Error(operation.LastError);
                cb?.Invoke(false);
                yield break;
            }

            yield return operation;
            if (operation.Status != EOperationStatus.Succeed)
            {
                Print.Error(operation.LastError);
                cb?.Invoke(false);
                yield break;
            }

            cb?.Invoke(true);
            yield break;
        }
    }
}
#endif