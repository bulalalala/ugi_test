//
// /**************************************************************************
//
// MethodExtension.cs
//
// Author: xiaohong  <704872627@qq.com>
//
// Unity课程讨论群:  152767675
//
// Date: 15-8-25
//
// Description:Provide  functions  to connect Oracle
//
// Copyright (c) 2015 xiaohong
//
// **************************************************************************/

using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace XHFrameWork
{
	static public class MethodExtension
	{
		/// <summary>
		/// Gets the or add component.
		/// </summary>
		/// <returns>The or add component.</returns>
		/// <param name="go">Go.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		static public T GetOrAddComponent<T>(this GameObject go) where T : Component
		{
			T ret = go.GetComponent<T>();
			if (null == ret)
				ret = go.AddComponent<T>();
			return ret;
		}

        public static T DeepClone<T>( T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        static public void DestroyAllChildren(this Transform transform)
        {
            //Debug.Log("Destory");
            foreach (var child in transform.OfType<Transform>().ToList())
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }
        static public void ClearAllChildren(this Transform transform)
        {
            foreach (var child in transform.OfType<Transform>().ToList())
            {
                Image image = transform.FindChild("Image").GetComponent<Image>();
                image.sprite = null;
                image.enabled = false;
                Text text = image.transform.FindChild("Text").GetComponent<Text>();
                text.text = null;
                text.enabled = false;
            }
        }
    }
}

