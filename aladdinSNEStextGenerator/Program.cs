using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Rectangle, Bitmap
using System.IO; // MemoryStream, Directory

namespace aladdinSNEStextGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+===============================================================+");
            Console.WriteLine("| aladdinSNEStextGenerator (made by Ogan Özkul aka Ryuguu Chan) |");
            Console.WriteLine("+===============================================================+\n");

            MemoryStream stream = new MemoryStream(AladdinSNEStextImage.rawData);
            Bitmap bitmap = new Bitmap(stream);

            Dictionary<char, Rectangle> char2rects = new Dictionary<char, Rectangle>
            {
                { 'A', new Rectangle(8, 8, 8, 8) },
                { 'B', new Rectangle(16, 8, 8, 8) },
                { 'C', new Rectangle(24, 8, 8, 8) },
                { 'D', new Rectangle(32, 8, 8, 8) },
                { 'E', new Rectangle(40, 8, 8, 8) },
                { 'F', new Rectangle(48, 8, 8, 8) },
                { 'G', new Rectangle(56, 8, 8, 8) },
                { 'H', new Rectangle(64, 8, 8, 8) },
                { 'I', new Rectangle(72, 8, 8, 8) },
                { 'J', new Rectangle(80, 8, 8, 8) },
                { 'K', new Rectangle(88, 8, 8, 8) },
                { 'L', new Rectangle(96, 8, 8, 8) },
                { 'M', new Rectangle(104, 8, 8, 8) },
                { 'N', new Rectangle(112, 8, 8, 8) },
                { 'O', new Rectangle(120, 8, 8, 8) },
                { 'P', new Rectangle(128, 8, 8, 8) },
                { 'Q', new Rectangle(136, 8, 8, 8) },
                { 'R', new Rectangle(144, 8, 8, 8) },
                { 'S', new Rectangle(152, 8, 8, 8) },
                { 'T', new Rectangle(160, 8, 8, 8) },
                { 'U', new Rectangle(168, 8, 8, 8) },
                { 'V', new Rectangle(176, 8, 8, 8) },
                { 'W', new Rectangle(184, 8, 8, 8) },
                { 'X', new Rectangle(192, 8, 8, 8) },
                { 'Y', new Rectangle(200, 8, 8, 8) },
                { 'Z', new Rectangle(208, 8, 8, 8) },
                { ' ', new Rectangle(216, 8, 8, 8) }
            };

            if (args.Length >= 1)
            {
                // 0) collecting all the arguments and make them into one string
                string text = "";
                for (int i = 0; i < args.Length; i++)
                {
                    text += args[i];
                    if (i != args.Length) { text += " "; }
                }

                // 1) converting the whole input into uppercase
                string r = text.ToUpper();

                // 2) checking wether all the chars are valid or not
                // TODO: adding more character (according to the image "aladdinSNEStextImage.png found in the repo"
                string validChars = "QWERTZUIOPLKJHGFDSAYXCVBNM ";

                foreach (char c in r)
                {
                    if (!validChars.Contains(c.ToString())) 
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("your text contains unsupported characters!");
                        Console.WriteLine("here's a list of all the currently supported characters...\n---");
                        for (int i = 0; i < validChars.Length; i++)
                        {
                            Console.WriteLine("* {0}", validChars[i].ToString() == " " ? "[blank space]" : validChars[i].ToString());
                        }
                        Environment.Exit(0);
                    }
                }

                // 3) computing the whole thing
                Bitmap R = new Bitmap((r.Length * 8), 8);

                using (Graphics g = Graphics.FromImage(R))
                {
                    for (int i = 0; i < r.Length; i++)
                    {
                        int beginXpos = (i * 7);
                        g.DrawImage(bitmap.Clone(char2rects[r[i]], System.Drawing.Imaging.PixelFormat.Format32bppArgb), beginXpos + i, 0);
                    }
                }

                // 4) converting all black colors to transparent color
                for (int y = 0; y < R.Height; y++)
                {
                    for (int x = 0; x < R.Width; x++)
                    {
                        if (R.GetPixel(x, y) == Color.FromArgb(0, 0, 0)) { R.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0)); }
                    }
                }

                // 5) letting the user choose the filename
                Console.Write("The output's name [output.png]: ");
                string outputName = Console.ReadLine();
                if (string.IsNullOrEmpty(outputName)) { outputName = "output"; }

                // 6) saving the pictuer in the current working directory (CWD)
                string saveFilename = Directory.GetCurrentDirectory() + "\\" + outputName + ".png";
                R.Save(saveFilename);
                
                // 7) saying goodbye
                Console.WriteLine($"done...\n\nIt's saved under the name \"{outputName}.png\"");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Here's the syntax\n----\naladdinSNEStextGenerator [your text here]");
            }
        }
    }
}
