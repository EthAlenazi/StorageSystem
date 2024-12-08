namespace SimpleStorageService.Services.Helpers
{
    public static class CommonValidation
    {
        /// <summary>
        /// Validates whether a string is a valid Base64 encoded string.
        /// </summary>
        /// <param name="base64String">The string to validate.</param>
        /// <returns>True if the string is valid Base64, false otherwise.</returns>
        public static bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return false;

            // Check if the string length is valid for Base64
            if (base64String.Length % 4 != 0)
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}

