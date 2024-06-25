using System.Text;

namespace ReportingBackendApp
{
    public static class Base64Extensions
    {
        /// <summary>
        /// Codifica un string a Base64.
        /// </summary>
        /// <param name="plainText">El string a codificar.</param>
        /// <returns>El string codificado en Base64.</returns>
        public static string ToBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText), "El texto no puede ser nulo o vacío.");
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodifica un string de Base64 a texto plano.
        /// </summary>
        /// <param name="base64EncodedData">El string codificado en Base64.</param>
        /// <returns>El string decodificado en texto plano.</returns>
        public static string FromBase64(this string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                throw new ArgumentNullException(nameof(base64EncodedData), "El texto codificado en Base64 no puede ser nulo o vacío.");
            }

            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
