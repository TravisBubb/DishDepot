using System.Security.Cryptography;

namespace BSS.DishDepot.Domain.Services;

public static class PasswordHasher
{
    // Versioning system so that hashing algorithms can be changed in the future without breaking old data in DB
    private const byte VersionId1 = 0x01;
    private const byte DefaultVersionId = VersionId1;

    private static readonly Dictionary<byte, PasswordHasherVersion> Versions = new()
    {
        [VersionId1] = new PasswordHasherVersion(HashAlgorithmName.SHA256, SaltSize: 256 / 8, KeySize: 256 / 8, Iterations: 600000)
    };

    private sealed record PasswordHasherVersion(HashAlgorithmName Algorithm, int SaltSize, int KeySize, int Iterations);

    public static string HashPassword(string password)
    {
        // Always use the newest version for new passwords
        var version = Versions[DefaultVersionId];

        // If you create a new version that makes the output size bigger than 1024 bytes,
        // consider allocating an array instead or using the shared array pool instead:
        // https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraypool-1.shared?view=net-8.0
        var hashedPasswordByteCount = 1 + version.SaltSize + version.KeySize;
        Span<byte> hashedPasswordBytes = stackalloc byte[hashedPasswordByteCount];

        // Creating spans is cheap and allows us to use span-based cryptography APIs
        var saltBytes = hashedPasswordBytes.Slice(start: 1, length: version.SaltSize);
        var keyBytes = hashedPasswordBytes.Slice(start: 1 + version.SaltSize, length: version.KeySize);

        // Write the version ID, the salt, and then the key
        hashedPasswordBytes[0] = DefaultVersionId;
        RandomNumberGenerator.Fill(saltBytes);
        Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, keyBytes, version.Iterations, version.Algorithm);

        return Convert.ToBase64String(hashedPasswordBytes);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        // We can predict the number of bytes that will be decoded from the Base64 string,
        // to avoid allocating a byte array with "Convert.FromBase64String" and instead use span-based APIs.
        // Again, consider creating an array or using the shared array pool if the output size is bigger than 1024 bytes.
        var hashedPasswordByteCount = ComputeDecodedBase64ByteCount(hashedPassword);
        Span<byte> hashedPasswordBytes = stackalloc byte[hashedPasswordByteCount];

        if (!Convert.TryFromBase64String(hashedPassword, hashedPasswordBytes, out _))
        {
            // This shouldn't happen unless there's a mistake in how we compute the decoded Base64 byte count.
            throw new InvalidOperationException("Failed to convert from basse 64 string");
        }

        if (hashedPasswordBytes.Length == 0)
        {
            return false;
        }

        var versionId = hashedPasswordBytes[0];
        if (!Versions.TryGetValue(versionId, out var version)) return false;

        var expectedHashedPasswordLength = 1 + version.SaltSize + version.KeySize;
        if (hashedPasswordBytes.Length != expectedHashedPasswordLength) return false;

        var saltBytes = hashedPasswordBytes.Slice(start: 1, length: version.SaltSize);
        var expectedKeyBytes = hashedPasswordBytes.Slice(start: 1 + version.SaltSize, length: version.KeySize);

        Span<byte> actualKeyBytes = stackalloc byte[version.KeySize];
        Rfc2898DeriveBytes.Pbkdf2(providedPassword, saltBytes, actualKeyBytes, version.Iterations, version.Algorithm);

        // This method prevents leaking timing information when comparing the two byte spans.
        if (!CryptographicOperations.FixedTimeEquals(expectedKeyBytes, actualKeyBytes))
            return false;

        // TODO: Add a return type to indicate that the password needs to be re-hashed
        // if the versionId != DefaultVersionId
        return true;
    }

    private static int ComputeDecodedBase64ByteCount(string base64String)
    {
        // Base64 encodes three bytes by four characters, and there can be up to two padding characters.
        var characterCount = base64String.Length;
        var paddingCount = 0;

        if (characterCount > 0 && base64String[characterCount - 1] == '=')
        {
            paddingCount++;

            if (characterCount > 1 && base64String[characterCount - 2] == '=')
                paddingCount++;
        }

        return (characterCount * 3 / 4) - paddingCount;
    }
}
