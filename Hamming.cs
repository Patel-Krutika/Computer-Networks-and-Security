+using System;
using System.Collections;
using System.Text;
class Hamming
{
	static void DisplayAuthorInfo()
	{
		Console.WriteLine("Author: Krutika Patel");
		Console.WriteLine("Date: 09/27/2019");
	}
	static void DisplayUsageInfo()
	{

			Console.WriteLine("Hamming - use Hamming method for error detection and correction");
			Console.WriteLine("usage: hamming");
			Console.WriteLine("usage: hamming -h");
			Console.WriteLine("usage: hamming -r");
			Console.WriteLine("usage: hamming [-e | -d] [message in binary]");
			Console.WriteLine("usage: hamming -ea [message in ascii]");
			Console.WriteLine("usage: hamming -ex [message in hex]");
			Console.WriteLine("Options:");
			Console.WriteLine("-e create message with hamming bits for transmission");
			Console.WriteLine("-d extract orginal message from message received");
			Console.WriteLine("-a must be with -e; input message in ascii");
			Console.WriteLine("Please enter ascii input in such format #.# (e.g. 72.105.33");
			Console.WriteLine("-x must be with -e; input message in hex");
			Console.WriteLine("-r display author information");
			Console.WriteLine("-h display help");
			Console.WriteLine("Note: Output is always in binary; ascii to binary is UTF-8 encoding.");
			Console.WriteLine("Original message is extracted after error correction (if any).");
	}
	static void createMessage(string message)
	{
		int n = get_n(message.Length);//n that satifies the inequality 2^n > m+n+1
		int[] one_Positions = new int[20]; //hardcode the one positions array
		int[] transmission_Message = new int[n+message.Length];//message to be transmitted
		string[] final_Message = new string[transmission_Message.Length];
		//make the hamming positions null

		for(int i = 0; i < transmission_Message.Length; i++)
		{
			if(isPowerOfTwo(i+1))
			{
				transmission_Message[i] = -1;
			}
		}

		int j=0;//counter for Original message array
		//put the Original message values in the transmission_Message array
		//add the bit postions that have a 1 in one_Positions ArrayList
		int one = 0;
		for(int i = transmission_Message.Length-1; i >= 0; i--)
		{
			if (transmission_Message[i] != -1)
			{
					transmission_Message[i] = Convert.ToInt32(new string(message[j],1));
					j++;
			}
			if(transmission_Message[i] == 1)
			{
					one_Positions[one] = i+1;
					one ++;
			}
		}

		int xored = one_Positions[0];
		for(int i = 1; i < one_Positions.Length; i++)
		{
			xored = xored ^ one_Positions[i];

		}

		string hamming_bits = Convert.ToString(xored,2);
		int k = hamming_bits.Length-1;
		StringBuilder encoded = new StringBuilder();
		for(int i = 0; i < transmission_Message.Length; i++)
		{
			if(transmission_Message[i] == -1)
			{
				if(k>=0)
				{
					transmission_Message[i] = Convert.ToInt32(new string(hamming_bits[k],1));
					k--;
				}
				else
					transmission_Message[i] = 0;
			}
			encoded.Append(transmission_Message[i].ToString());
		}
		StringBuilder encoded2 = new StringBuilder();
		for(int i = encoded.Length-1; i>=0; i--)
		{
			encoded2.Append(encoded[i]);
		}

		Console.WriteLine(encoded2);
	}
		static bool isPowerOfTwo(int x)
		{
			return (x!= 0 && (x&(x-1))==0);
		}
		static int get_n(int message)
		{
			int n = 0;
			for(int i = 0; i < message; i++)
			{
				if(Math.Pow(2,i) > (message+i+1))
				{
					n = i;
					break;
				}
			}
			return n;
		}
	static void getMessage(string message)
	{
		StringBuilder m = new StringBuilder();
		StringBuilder decoded_Message = new StringBuilder();
		StringBuilder decoded_Message2 = new StringBuilder();
		int[] one_Positions = new int[20];
		int one = 0;
		string hamming_bits = "";
		for(int i = message.Length-1; i>=0; i--)
		{
			m.Append(message[i]);
		}
		//gets the one positions and creates the hamming code
		for(int i = 0; i < m.Length; i++)
		{
			if(!isPowerOfTwo(i+1) && m[i]=='1')
			{
				one_Positions[one] = i+1;
				one++;
			}
			if(isPowerOfTwo(i+1))
			{
				hamming_bits = m[i]+hamming_bits;
			}
		}
		int hamming = Convert.ToInt32(hamming_bits,2);
		int xored = hamming;
		for(int i = 0; i < one_Positions.Length; i++)
		{
			xored = xored ^ one_Positions[i];
		}
		if(xored==0)
		{
			for(int i = 0; i < m.Length; i++)
			{
				if(!isPowerOfTwo(i+1))
				{
					decoded_Message.Append(m[i]);
				}
			}
		}
		else
		{
			if(m[xored-1]==1)
				m[xored-1]='0';
			else
				m[xored-1]='0';
		}

		for(int i = 0; i < m.Length; i++)
		{
			if(!isPowerOfTwo(i+1))
			{
				decoded_Message.Append(m[i]);
			}
		}

		for(int i = decoded_Message.Length-1; i >=0; i--)
		{
			decoded_Message2.Append(decoded_Message[i]);
		}
		Console.WriteLine(decoded_Message2);
	}

	public static void Main(string[] args)
	{

		/*
		Console.WriteLine("args.Length = " + args.Length);
		for(int i = 0; i < args.Length; i++)
		{
			Console.WriteLine(args[i]);
		}
		*/
		 	//code to check args length and values

		int arguments = args.Length;
		if((arguments == 0) || (arguments>2))
		{
			Console.WriteLine("No command line option(s) provided!");
			DisplayUsageInfo();
		}
		else if(arguments == 1 || arguments == 2)
		{
			string opt = (args[0].ToLower());
			if(opt.Equals("-r"))
			{
				DisplayAuthorInfo();
			}
			else if(opt.Equals("-h"))
			{
				DisplayUsageInfo();
			}
			else if(opt.Equals("-e"))
			{
				string message = args[1];
				createMessage(message);
			}
			else if(opt.Equals("-d"))
			{
				string message = args[1];
				getMessage(message);
			}
			else if (opt.Equals("-ea"))
			{
				char[] splitters = {'.'};
				string[] message = args[1].Split(splitters);
				string m = "";
				int max_bit = 0;
				for (int i = 0; i<message.Length; i++)
				{
					message[i] = Convert.ToString(Convert.ToInt32(message[i],10),2);
					if(message[i].Length>max_bit)
						max_bit=message[i].Length;
				}

				for(int i = 0; i<message.Length; i++)
				{
					if(message[i].Length<max_bit)
					{
						while(message[i].Length!=max_bit)
						{
							message[i] = "0"+message[i];
						}
					}
				}

				for(int i = 0; i<message.Length; i++)
				{
					m = m + message[i];
				}
				createMessage(m);
			}
			else if(opt.Equals("-ex"))
			{
				string message = args[1];
				string encode = Convert.ToString(Convert.ToInt32(message,16),2);
				createMessage(encode);
			}
			else
			{
				Console.WriteLine("Invalid command line option(s)!");
				DisplayUsageInfo();
			}
		}
	}
}
