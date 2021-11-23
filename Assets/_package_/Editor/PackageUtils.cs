using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskKits;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace CommandTool
{
    public static class PackageUtils
    {
        public static async Task<bool> AddPackageAsync(string packageId, Action<bool> completed)
        {
            var request = Client.Add(packageId);
            var condition = new TaskCondition();
            await condition.WaitUntilProgress(() => request.IsCompleted);
            var success = request.Status == StatusCode.Success;
            if (request.Status >= StatusCode.Failure)
            {
                Debug.LogError($"AddPackageAsync Failure: {request.Error.message}");
                success = false;
            }

            completed?.Invoke(success);
            return success;
        }

        public static async Task<bool> UpdatePackageAsync(string packageId, Action<bool> completed)
        {
            return await AddPackageAsync(packageId, completed);
        }

        public static async Task<bool> RemovePackageAsync(string packageName, Action<bool> completed)
        {
            var request = Client.Remove(packageName);
            var condition = new TaskCondition();
            await condition.WaitUntilProgress(() => request.IsCompleted);
            var success = request.Status == StatusCode.Success;
            if (request.Status >= StatusCode.Failure)
            {
                Debug.LogError($"RemovePackageAsync Failure: {request.Error.message}");
                success = false;
            }

            completed?.Invoke(success);
            return success;
        }

        public static async Task<bool> ListPackageAsync(Action<bool, Dictionary<string, PackageInfo>> completed)
        {
            var request = Client.List(true, true);
            var condition = new TaskCondition();
            await condition.WaitUntilProgress(() => request.IsCompleted);
            var success = request.Status == StatusCode.Success;
            if (request.Status >= StatusCode.Failure)
            {
                Debug.LogError($"ListPackageAsync Failure: {request.Error.message}");
                success = false;
            }

            var list = new Dictionary<string, PackageInfo>();
            if (success)
            {
                foreach (var package in request.Result)
                {
                    list.Add(package.name, package);
                }
            }

            completed?.Invoke(success, list);
            return success;
        }

//        #region 添加或更新插件包
//
//        private static AddRequest _addRequest;
//        private static Action _addCompletedCallback;
//
//        /// <summary>
//        /// 添加或更新插件包
//        /// </summary>
//        /// <param name="packageId"></param>
//        public static void AddOrUpdatePackage(string packageId, Action completed)
//        {
//            _addRequest = Client.Add(packageId);
//            _addCompletedCallback = completed;
//            EditorApplication.update += AddProgress;
//        }
//
//
//        private static void AddProgress()
//        {
//            if (_addRequest.IsCompleted)
//            {
//                if (_addRequest.Status == StatusCode.Success)
//                {
//                    Debug.Log("Installed: " + _addRequest.Result.packageId);
//                }
//                else if (_addRequest.Status >= StatusCode.Failure)
//                {
//                    Debug.Log(_addRequest.Error.message);
//                }
//
//                _addCompletedCallback?.Invoke();
//                _addCompletedCallback = null;
//
//                EditorApplication.update -= AddProgress;
//            }
//        }
//
//        #endregion


//        #region 插件包列表
//
//        private static ListRequest ListRequest;
//
//        private static Action<Dictionary<string, PackageInfo>> _listRequestCompletedCallback;
//
//        public static void List(Action<Dictionary<string, PackageInfo>> completed)
//        {
//            ListRequest = Client.List(true, true);
//            _listRequestCompletedCallback = completed;
//            EditorApplication.update += ListProgress;
//        }
//
//        private static void ListProgress()
//        {
//            if (ListRequest.IsCompleted)
//            {
//                if (ListRequest.Status == StatusCode.Success)
//                {
//                    var list = new Dictionary<string, PackageInfo>();
//                    foreach (var package in ListRequest.Result)
//                    {
////                        Debug.Log($"{package.name},{package.displayName},{package.version}");
//                        list.Add(package.name, package);
//                    }
//
//                    _listRequestCompletedCallback?.Invoke(list);
//                    _listRequestCompletedCallback = null;
//                }
//                else if (ListRequest.Status >= StatusCode.Failure)
//                {
//                    Debug.Log(ListRequest.Error.message);
//                }
//
//                EditorApplication.update -= ListProgress;
//            }
//        }
//
//        #endregion
    }
}