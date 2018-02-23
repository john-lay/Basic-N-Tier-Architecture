namespace SecretHash
{
    using System;
    using System.Security.Cryptography;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetHash("123@abc")); //lCXDroz4HhR1EIx8qaz3C13z/quTXBkQ3Q5hj7Qx3aA=
            Console.ReadLine();
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
