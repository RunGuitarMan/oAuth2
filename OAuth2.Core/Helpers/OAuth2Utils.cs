using System;
using System.Security.Cryptography;
using System.Text;

namespace OAuth2.Core.Helpers
{
    public static class OAuth2Utils
    {
        /// <summary>
        /// Генерирует криптографически безопасную случайную строку для токенов.
        /// </summary>
        /// <param name="size">Размер в байтах (по умолчанию 32 байта)</param>
        /// <returns>Base64 строка, представляющая случайный токен</returns>
        public static string GenerateRandomToken(int size = 32)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenData = new byte[size];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        /// <summary>
        /// Вычисляет SHA256 хэш для заданной строки.
        /// </summary>
        /// <param name="rawData">Исходные данные</param>
        /// <returns>Хэш в виде строки в шестнадцатеричном формате</returns>
        public static string ComputeSha256Hash(string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Сравнивает два хэша на равенство, используя алгоритм безопасного сравнения.
        /// </summary>
        /// <param name="hash1">Первый хэш</param>
        /// <param name="hash2">Второй хэш</param>
        /// <returns>True, если хэши совпадают, иначе false</returns>
        public static bool CompareHashes(string hash1, string hash2)
        {
            if (hash1 == null || hash2 == null || hash1.Length != hash2.Length)
                return false;

            int result = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                result |= hash1[i] ^ hash2[i];
            }
            return result == 0;
        }
    }
}