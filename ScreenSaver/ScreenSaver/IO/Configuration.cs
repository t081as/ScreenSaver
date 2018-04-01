#region MIT License
// The MIT License (MIT)
//
// Copyright © 2017-2018 Tobias Koch <t.koch@tk-software.de>
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace ScreenSaver.IO
{
    /// <summary>
    /// A persistent configuration class storing simple key value pairs as <see cref="string"/>.
    /// </summary>
    public class Configuration
    {
        #region Constants and Fields

        /// <summary>
        /// Contains keys and values.
        /// </summary>
        private Dictionary<string, string> values;

        /// <summary>
        /// Represents the lock object used to synchronize access to the <see cref="values"/> dictionary.
        /// </summary>
        private object valuesLock = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.values = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Stores the given configuration using the given file name.
        /// </summary>
        /// <param name="fileName">The path and name of the file that shall be used to store the configuration.</param>
        /// <param name="configuration">The configuration that shall be stored.</param>
        /// <exception cref="ArgumentNullException"><c>fileName</c> is null.</exception>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is null.</exception>
        /// <exception cref="IOException">Error while saving the data.</exception>
        public void Save(string fileName, Configuration configuration)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the configuration from the given file.
        /// </summary>
        /// <param name="fileName">The path and name of the file that shall be used to load the configuration.</param>
        /// <returns>The configuration loaded from the file.</returns>
        /// <exception cref="ArgumentNullException"><c>fileName</c> is null.</exception>
        /// <exception cref="IOException">Error while loading the data.</exception>
        public Configuration Load(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the given key to the given value.
        /// </summary>
        /// <param name="key">The key that shall be used to store the value.</param>
        /// <param name="value">The value that shall be stored.</param>
        /// <exception cref="ArgumentNullException"><c>key</c> is null.</exception>
        /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>key</c> is empty.</exception>
        public void SetValue(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (key.Trim() == string.Empty)
            {
                throw new ArgumentException("key is empty", nameof(key));
            }

            key = key.ToLower();

            lock (this.valuesLock)
            {
                if (this.values.ContainsKey(key))
                {
                    this.values[key] = value;
                }
                else
                {
                    this.values.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Gets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key that shall be used to retrieve the value.</param>
        /// <returns>The value associated with the given key.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>key</c> is empty.</exception>
        /// <exception cref="ArgumentException"><c>key</c> does not contain a value.</exception>
        public string GetValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Trim() == string.Empty)
            {
                throw new ArgumentException("key is empty", nameof(key));
            }

            key = key.ToLower();

            lock (this.valuesLock)
            {
                if (!this.values.ContainsKey(key))
                {
                    throw new ArgumentException("key not found", nameof(key));
                }

                return this.values[key];
            }
        }

        /// <summary>
        /// Gets the value associated with the given key or the given default value if there is no value stored for the key..
        /// </summary>
        /// <param name="key">The key that shall be used to retrieve the value.</param>
        /// <param name="defaultValue">The value that shall be returned if there is no stored value for the given key.</param>
        /// <returns>The value associated with the given key or the default value.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>key</c> is empty.</exception>
        public string GetValue(string key, string defaultValue)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Trim() == string.Empty)
            {
                throw new ArgumentException("key is empty", nameof(key));
            }

            try
            {
                return this.GetValue(key);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }

        #endregion
    }
}
