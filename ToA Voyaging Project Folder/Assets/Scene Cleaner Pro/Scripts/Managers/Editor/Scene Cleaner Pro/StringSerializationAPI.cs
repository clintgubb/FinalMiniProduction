using UnityEngine;
using System.Collections;
using System;
using Devdog.SceneCleanerPro.FullSerializer;

namespace Devdog.SceneCleanerPro.Editor
{
    /// <summary>
    /// Json serializtaion by FullSerializer
    /// </summary>
    public static class StringSerializationAPI
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        public static string Serialize(Type type, object value)
        {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        public static object Deserialize(Type type, string serializedState)
        {
            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }

        /// <summary>
        /// Writes json string in txt file at path
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <param name="toWrite">string to write</param>
        public static void JsonWrite(string path, string toWrite)
        {
            System.IO.File.WriteAllText(Application.dataPath + "/" + path + ".txt", toWrite);
        }

        /// <summary>
        /// Reads json string from txt file at path
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <returns></returns>
        public static string JsonRead(string path)
        {
            return System.IO.File.ReadAllText(Application.dataPath + "/" + path + ".txt");
        }
        
    }
}
