using System.Security.Cryptography;
using System.Text;
using static lizzy.Core.Shared.Constants;

namespace lizzy.Core.Helpers;

public static class CryptographyHelper
{
    public static (string, string) GetCodeVerifierValues()
    {
        string codeVerifier = string.Join(null, Enumerable
        .Range(0, CodeVerifierLength)
        .Select(_ => RandomStringInput[Random.Shared.Next(RandomStringInput.Length)]));

        byte[] bytes = Encoding.UTF8.GetBytes(codeVerifier);

        byte[] hashedBytes = SHA256.HashData(bytes);

        string codeVerifierBase64 = GetBase64Value(hashedBytes);

        return (codeVerifier, codeVerifierBase64);
    }

    public static string GetBase64Value(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
        .Replace("=", string.Empty)
        .Replace("+", "-")
        .Replace("/", "_");
    }
}