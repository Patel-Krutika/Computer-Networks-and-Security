using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
public class IP
{
	public static void Main(string[] args)
	{
		//string for argument from user
		string str = "";
		bool match = false;
		//check if user provided arguments
		//if argument provided make it into a string
		if(args.Length >0)
		{
			for(int i = 0; i<args.Length; i++)
			{
				str = str + " " +args[i];
			}
		}
		IPGlobalProperties prop = IPGlobalProperties.GetIPGlobalProperties();
		NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		IPGlobalProperties gp = IPGlobalProperties.GetIPGlobalProperties();

		//... ...

		Console.WriteLine("Windows IP Configuration");
		Console.WriteLine();
		Console.WriteLine(" Host name . . . . . . . . . . . . : {0}", prop.HostName);
		Console.WriteLine(" Primary DNS Suffix . . . . . . . : {0}", nics[0].GetIPProperties().DnsSuffix);
		Console.WriteLine();
		
		//... ...

		foreach (NetworkInterface adapter in nics)
		{
			String address = adapter.GetPhysicalAddress().ToString();
			 IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
			 GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
			UnicastIPAddressInformationCollection UnicastIPInfoCol = adapter.GetIPProperties().UnicastAddresses;
			
			if (adapter.NetworkInterfaceType.Equals(NetworkInterfaceType.Wireless80211))
			{
				string interfaceType = "Wireless LAN";
				//check if user input is a part of adapter name
				string name = interfaceType + " adapter " + adapter.Name;
				if(name.Contains(str))
				{
					match = true;
					Console.WriteLine("{0} adapter {1}:",interfaceType,adapter.Name);
					Console.WriteLine("\tDescription: {0}", adapter.Description.ToString());
					Console.WriteLine("\tConnection-Specific DNS Suffix . . . . . . . : {0}", nics[0].GetIPProperties().DnsSuffix);
					//check if there exists a default gateway address
					if(addresses.Count == 0)
						Console.WriteLine("\tDefault Gateway :  . . . . . . . . . .  :");
					else
					{
						foreach(GatewayIPAddressInformation ad in addresses)
						{
							Console.WriteLine("\tDefault Gateway  . . . . . . . . . . : {0}",ad.Address.ToString());
						}
					}
					IPAddress ad0 = UnicastIPInfoCol[0].Address;
					   if(ad0.IsIPv6LinkLocal)
							Console.WriteLine("\tLink-local Ipv6 Address  . . . . . . . . . .  : {0}",  ad0);
						else
							Console.WriteLine("\tIPv6 Address  . . . . . . . . . .  : {0}",ad0);
						for(int i = 1; i < UnicastIPInfoCol.Count; i++)
					   {
						if(UnicastIPInfoCol[i].Address.ToString().Contains(":"))
						{
							Console.WriteLine("\tIPv6 Address  . . . . . . . . . .  : {0}",UnicastIPInfoCol[i].Address);
						}
						else
						{
							Console.WriteLine("\tIPv4 Address  . . . . . . . . . .  : {0}", UnicastIPInfoCol[i].Address);
							Console.WriteLine("\tSubnet Mask  . . . . . . . . . .  : {0}", UnicastIPInfoCol[i].IPv4Mask);
						}
					   }
				}
			}
			
			if (adapter.NetworkInterfaceType.Equals(NetworkInterfaceType.Ethernet))
			{
				string interfaceType = "Ethernet";
				//check if user input is a part of adapter name
				string name = interfaceType + " adapter " + adapter.Name;
				if(name.Contains(str))
				{
					match = true;
					Console.WriteLine("{0} adapter {1}:",interfaceType,adapter.Name);
					Console.WriteLine("\tDescription: {0}", adapter.Description.ToString());
					Console.WriteLine("\tConnection-Specific DNS Suffix . . . . . . . : {0}", nics[0].GetIPProperties().DnsSuffix);
					//check if there exists a default gateway address
					if(addresses.Count == 0)
						Console.WriteLine("\tDefault Gateway   . . . . . . . . . .  :");
					else
					{
						foreach(GatewayIPAddressInformation ad in addresses)
						{
							Console.WriteLine("\tDefault Gateway :  . . . . . . . . . .  {0}",ad.Address.ToString());
						}
					}
					IPAddress ad0 = UnicastIPInfoCol[0].Address;
				   if(ad0.IsIPv6LinkLocal)
						Console.WriteLine("\tLink-local Ipv6 Address  . . . . . . . . . .  : {0}",  ad0);
					else
						Console.WriteLine("\tIPv6 Address  . . . . . . . . . .  : {0}",ad0);
					for(int i = 1; i < UnicastIPInfoCol.Count; i++)
				   {
					if(UnicastIPInfoCol[i].Address.ToString().Contains(":"))
					{
						Console.WriteLine("\tIPv6 Address  . . . . . . . . . .  : {0}",UnicastIPInfoCol[i].Address);
					}
					else
					{
						Console.WriteLine("\tIPv4 Address  . . . . . . . . . .  : {0}", UnicastIPInfoCol[i].Address);
						Console.WriteLine("\tSubnet Mask  . . . . . . . . . .  : {0}", UnicastIPInfoCol[i].IPv4Mask);
					}
				   }
				}
			}	
		}
		
		if(!match)
		{
			Console.WriteLine("No matches found!, try again");
		}
	}
}