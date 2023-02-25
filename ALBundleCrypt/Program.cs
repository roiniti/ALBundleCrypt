using System;
using System.IO;

namespace ALBundleCrypt
{
    class Program
    {
		static void DoDecode(string _InputFileName, string _OutputFileName)
		{
			byte[] gcbuffer = File.ReadAllBytes(_InputFileName);
			byte[] out_assert = DigitalSea.Scipio(gcbuffer);
			File.WriteAllBytes(_OutputFileName, out_assert);
		}

		static void DoEncode(string _InputFileName, string _OutputFileName)
		{
			byte[] gcbuffer = File.ReadAllBytes(_InputFileName);
			byte[] out_assert = DigitalSea.XANA(gcbuffer);
			File.WriteAllBytes(_OutputFileName, out_assert);
		}

		static void Main(string[] args)
		{
			switch (args.Length)
			{
				case 2:
					{
                        switch (args[1])
                        {
							case "-d":
								DoDecode(args[2], args[2] + "_dec");
								break;
							case "-e":
								DoEncode(args[2], args[2] + "_enc");
								break;
							default:
								PrintHelp();
								break;
                        }
						break;
					}
				case 3:
					{
						switch (args[1])
						{
							case "-d":
								DoDecode(args[2], args[3]);
								break;
							case "-e":
								DoEncode(args[2], args[3]);
								break;
							default:
								PrintHelp();
								break;
						}
						break;
					}
				default:
					{
						PrintHelp();
						break;
					}
			}

		}

		private static void PrintHelp()
        {
			Console.WriteLine(@"Usage ALBundleCrypt -d|-e ""input_path"" [""output_path""]");
		}
	}
}
