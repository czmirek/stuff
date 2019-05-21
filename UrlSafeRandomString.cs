// A piece of code to generate URL safe and cryptographically random string in .NET Core 2
byte[] randomBytes = new byte[32];
using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
    rng.GetBytes(randomBytes);

string str = Convert.ToBase64String(randomBytes)
                    .Replace("+", "")
                    .Replace("/", "")
                    .Replace("=", "")
                    .Replace("-", "");
