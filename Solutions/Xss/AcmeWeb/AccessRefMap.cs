// * This component and its source code representation are copyright protected
// * and proprietary to McCullough & Associates, Worldwide
// *
// * This component and source code may be used for instructional and
// * evaluation purposes only. No part of this component or its source code
// * may be sold, transferred, or publicly posted, nor may it be used in a
// * commercial or production environment, without the express written consent
// * of C&M McCullough, Inc. dba McCullough&Associates
// *
// * Copyright (c) 2016 McCullough & Associates
// * 

namespace SecurityUtility
{
    /// <summary>
    /// This class obscures sensitive direct id's with a random 6 character string.
    /// </summary>
    /// <typeparam name="T">The type of the direct id</typeparam>
    public class AccessRefMap<T>
    {
        public Dictionary<string, T> Items { get; set; } = new Dictionary<string, T>();
        /// <summary>
        /// Generates a random id and associates it with the directRef
        /// </summary>
        /// <param name="directRef">The Direct Id</param>
        /// <returns>Random indirect id</returns>
        public string AddDirectReference(T directRef)
        {
            if (Items.ContainsValue(directRef))
                return Items.First(kv => kv.Value.Equals(directRef)).Key;
            //generate random string
            string randkey = "";
            char[] rndchar = new char[6];
            Random rnd = new Random();
            do
            {
                for (var i = 0; i < rndchar.Length; i++)
                    rndchar[i] = (char)rnd.Next('A', 'Z');
                randkey = new string(rndchar);
            } while (Items.ContainsKey(randkey));
            Items[randkey] = directRef;
            return randkey;
        }

        /// <summary>
        /// Returns the associated direct id
        /// </summary>
        /// <param name="indirectRef">The indirect id</param>
        /// <returns>the direct id</returns>
        public T GetDirectReference(string indirectRef)
        {
            return Items[indirectRef];
        }
    }
}
