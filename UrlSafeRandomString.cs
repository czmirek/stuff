byte[] randomBytes = new byte[32];
using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
    rng.GetBytes(randomBytes);

string str = Convert.ToBase64String(randomBytes)
                    .Replace("+", "")
                    .Replace("/", "")
                    .Replace("=", "")
                    .Replace("-", "");
