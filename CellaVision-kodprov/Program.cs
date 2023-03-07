// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Text.RegularExpressions;

class Program
{
    //here is the stream of bytes that are read by the ReadByte(), feel free to try different textlines
    public static string endlessStreamOfBytes = "PING 23 MOVE 24 X 100 \n TEST 42"; 
    public static bool run = true;
    public static ArrayList tokens = new ArrayList();

    static void Main(string[] args)
    {
        string textline = "";

        while (run)
        {
            byte b = ReadByte();

            if (b == 10) // 10 is line break
            {
                run = false;
                ProcessLine(textline.Trim());
                textline = "";
            }
            else if (Char.IsLetter((Char)b) || Char.IsNumber((Char)b) || b == 32) // 32 is space
            {
                textline += (Char)b;
            }
            else throw new Exception("Wrong input: \"" + (Char)b + "\" is not a valid character");
        }

        //This is to check if what Tokens are accepted and that they have the righht type.
        //Console.WriteLine("Tokens:");
        //foreach (var token in tokens)
        //{
        //    Console.WriteLine(token + "\t : " + token.GetType());
        //}
    }

    public static byte ReadByte()
    {        
        byte b = (byte)endlessStreamOfBytes[0];
        endlessStreamOfBytes = endlessStreamOfBytes.TrimStart((Char)b);

        return b;
    }

    public static void ProcessLine(string input)
    {
        string[] lokalTokens = input.Split(' ');
        foreach (string token in lokalTokens)
        {
            if (Regex.IsMatch(token, @"^[a-zA-Z]+$"))
            {
                tokens.Add(token);
            }
            else if (Regex.IsMatch(token, @"^[0-9]+$"))
            {
                int sequenceNumber = int.Parse(token);
                tokens.Add(sequenceNumber);
            }
            else throw new Exception("Wrong input: \"" + token + "\" is neither a valid word nor number");
        }
    }
}