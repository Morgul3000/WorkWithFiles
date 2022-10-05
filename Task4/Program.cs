using System;
using System.IO;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Name;
            string Group;
            string DateOfBirth;
            
            string path = @"C:\Users\udav\Desktop\Students.dat";

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                Name = reader.ReadString();
                //Group = reader.ReadString();
                //DateOfBirth = reader.ReadString();
            }

            Console.WriteLine(Name);
            //Console.WriteLine(Group);
            //Console.WriteLine(DateOfBirth);
        }
    }
}
