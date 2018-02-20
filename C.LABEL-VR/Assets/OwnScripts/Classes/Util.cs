﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using PostProcess;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

public static class Util
{

    #region Methods
    public static void DisableAllChildren(GameObject parent_inp)
    {
        for (int i = 0; i < parent_inp.transform.childCount; i++)
        {
            var child = parent_inp.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }
    }

    public static GameObject FindInactiveGameobject(GameObject parent_inp, string sName_inp)
    {
        var children = parent_inp.GetComponentsInChildren<Transform>(true);

        try
        {
            foreach (var child in children)
            {
                if (child.name == sName_inp)
                {
                    return child.gameObject;
                }
            }
        }
        catch
        {
            UnityEngine.Debug.Log("FindInactiveGameobject: null");
        }
        return null;
    }

    public static bool IsGameobjectTypeOf<T>(GameObject objectToTest_inp)
    {
        var testVal = objectToTest_inp.GetComponent<T>();

        if (testVal == null)
            return false;
        else
            return true;
    }

    public static void AlignToCamera(GameObject objectWithCamera_inp, GameObject objectToBeAligned_inp, float distanceToCamera_inp)
    {
        Camera objectCamera;
        try
        {
            objectCamera = objectWithCamera_inp.GetComponent<Camera>();
        }
        catch
        {
            UnityEngine.Debug.Log("Util.AlignToCamera(): Object has no Camera Component!");
            return;
        }

        Vector3 forwardVector = objectCamera.transform.forward;
        forwardVector.y = 0;

        objectToBeAligned_inp.transform.rotation = Quaternion.Euler(new Vector3(0, objectWithCamera_inp.transform.rotation.eulerAngles.y, 0));
        objectToBeAligned_inp.transform.position = objectWithCamera_inp.transform.position + forwardVector * distanceToCamera_inp;
    }

    public static GameObject CreateDefaultLabelPoint()
    {
        UnityEngine.Object o = Resources.Load("DefaultLabelpoint");
        GameObject go = UnityEngine.Object.Instantiate(o) as GameObject;
        go.name = o.name;
        return go;
    }

    #endregion

    #region Enums

    public enum Datatype
    {
        pcd,
        lidar,
        hdf5
    }

    public enum MovementMode
    {
        FreeFly,
        TeleportMode
    }

    #endregion

    #region Nested Classes

    //public class Tuple<T1>
    //{
    //    public Tuple(T1 item1)
    //    {
    //        Item1 = item1;
    //    }

    //    public T1 Item1 { get; set; }
    //}

    //public class Tuple<T1, T2> : Tuple<T1>
    //{
    //    public Tuple(T1 item1, T2 item2) : base(item1)
    //    {
    //        Item2 = item2;
    //    }

    //    public T2 Item2 { get; set; }
    //}

    

    public static class ClippingDistances
    {
        public static float _distanceToCamera_IngameOptions { get { return 5.0f; } }
        public static float _distanceToCamera_InfoMessage { get { return 4.9f; } }
        public static float _distanceToCamera_Clipping { get { return 4.89f; } }
        public static float _distanceToCamera_ClippingDefault { get { return 0.1f; } }
    }

    public static class EyeBlink
    {
        public static BlinkEffect _blinker = GameObject.Find("CenterEyeAnchor").GetComponent<BlinkEffect>();

        public static void EyeLidDown()
        {
            if (_blinker.state == BlinkEffect.State.Idle)
                _blinker.FadeIn();
        }

        public static void EyeLidUp()
        {

            if (_blinker.state == BlinkEffect.State.FadingIn || _blinker.state == BlinkEffect.State.WaitingForFadeOut)
                _blinker.FadeOut();
        }

        public static void Blink()
        {
            _blinker.Blink();
        }
    }

    public static class DataLoadInfo
    {
        public static string _sDataPath { get; private set; }
        public static Datatype _dataType { get; private set; }

        public static void StoreInfo(string sPath_inp, Datatype dataType_inp)
        {
            _sDataPath = sPath_inp;
            _dataType = dataType_inp;
        }
    }

    

    
    #endregion
}
