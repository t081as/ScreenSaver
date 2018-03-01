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
#endregion

namespace ScreenSaver
{
    /// <summary>
    /// Provides translations for the screen saver framework.
    /// </summary>
    internal class ScreenSaverTranslation
    {
        #region Constants and Fields

        /// <summary>
        /// The two letter language code of the configured language.
        /// </summary>
        private string twoLetterLanguageCode;

        /// <summary>
        /// Translations of the "OK" button.
        /// </summary>
        private IDictionary<string, string> okButtonTranslations;

        /// <summary>
        /// Translations of the "Cancel" button.
        /// </summary>
        private IDictionary<string, string> cancelButtonTranslations;

        /// <summary>
        /// Translations of the "no options available" message.
        /// </summary>
        private IDictionary<string, string> noOptionsTranslations;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaverTranslation"/> class using the given language code.
        /// </summary>
        /// <param name="twoLetterLanguageCode">The two letter language code.</param>
        /// <exception cref="ArgumentNullException"><c>twoLetterLanguageCode</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>twoLetterLanguageCode</c> is invalid.</exception>
        public ScreenSaverTranslation(string twoLetterLanguageCode)
        {
            if (twoLetterLanguageCode == null)
            {
                throw new ArgumentNullException("twoLetterLanguageCode");
            }

            if (twoLetterLanguageCode.Length != 2)
            {
                throw new ArgumentException("Invalid language code", "twoLetterLanguageCode");
            }

            this.twoLetterLanguageCode = twoLetterLanguageCode.ToLower();

            this.okButtonTranslations = new Dictionary<string, string>();
            this.cancelButtonTranslations = new Dictionary<string, string>();
            this.noOptionsTranslations = new Dictionary<string, string>();

            this.AddTranslations();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the two letter code for the default language.
        /// </summary>
        public string DefaultLanguageCode
        {
            get
            {
                return "en";
            }
        }

        /// <summary>
        /// Gets the translation of the "OK" button.
        /// </summary>
        public string OkButtonTranslation
        {
            get
            {
                if (this.okButtonTranslations.ContainsKey(this.twoLetterLanguageCode))
                {
                    return this.okButtonTranslations[this.twoLetterLanguageCode];
                }
                else
                {
                    return this.okButtonTranslations[this.DefaultLanguageCode];
                }
            }
        }

        /// <summary>
        /// Gets the translation of the "Cancel" button.
        /// </summary>
        public string CancelButtonTranslation
        {
            get
            {
                if (this.cancelButtonTranslations.ContainsKey(this.twoLetterLanguageCode))
                {
                    return this.cancelButtonTranslations[this.twoLetterLanguageCode];
                }
                else
                {
                    return this.cancelButtonTranslations[this.DefaultLanguageCode];
                }
            }
        }

        /// <summary>
        /// Gets the translation of the "no options available" message.
        /// </summary>
        public string NoOptionsMessageTranslation
        {
            get
            {
                if (this.noOptionsTranslations.ContainsKey(this.twoLetterLanguageCode))
                {
                    return this.noOptionsTranslations[this.twoLetterLanguageCode];
                }
                else
                {
                    return this.noOptionsTranslations[this.DefaultLanguageCode];
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the translations for the defined screen saver framework strings.
        /// </summary>
        protected void AddTranslations()
        {
            // Default (English (en))
            this.okButtonTranslations.Add(this.DefaultLanguageCode, "Ok");
            this.cancelButtonTranslations.Add(this.DefaultLanguageCode, "Cancel");
            this.noOptionsTranslations.Add(this.DefaultLanguageCode, "This screen saver has no options that you can set.");

            // German (de)
            this.okButtonTranslations.Add("de", "Ok");
            this.cancelButtonTranslations.Add("de", "Abbrechen");
            this.noOptionsTranslations.Add("de", "Dieser Bildschirmschoner hat keine Optionen, die festgelegt werden können.");
        }

        #endregion
    }
}
