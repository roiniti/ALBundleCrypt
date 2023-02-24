using System;

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



		static void Main(string[] args)
		{
			switch (args.Length)
			{
				case 2:
					{
						DoDecode(args[1], args[1] + "_dec");
						break;
					}
				case 3:
					{
						DoDecode(args[1], args[2]);
						break;
					}
				default:
					{
						Console.WriteLine(@"Arg error -> ""input_path"" [""output_path""]");
						break;
					}
			}

		}
	}
}
