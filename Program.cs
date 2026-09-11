




using System.Globalization;



class Program
{
    static void Main()
    {
        List<Parcel> parcelList = new();
        ShipFees shipFees = new();
        string week = "vecka2.txt";

        bool run = true;

        while (run == true)
        {
            Console.WriteLine("VÄLKOMMEN TILL LASSES LAST 1.0\n");
            Console.WriteLine("1) Beräkna frakt för ett paket");
            Console.WriteLine("2) Beräkna frakt för flera paket från fil");
            Console.WriteLine("3) Sök Paket\n");
            Console.WriteLine("4) Skriv ut lista");
            Console.WriteLine("5) Avsluta\n");
            Console.Write("Val: ");
            string input = Console.ReadLine();
            
            

            if (input == "1")
            {
                
                BuildParcel(parcelList,shipFees);
                TagParcel(parcelList);
                WriteOrderToFile(parcelList,week);
                PrintParcels(parcelList);
               
            }
            else if (input == "2")
            {
                ReadParcelFile(parcelList);
                //TagParcel(parcelList);
                WriteParcelsToFile(parcelList);
                PrintParcels(parcelList);
            }
            else if (input == "3")
            {
                SearchParcel(parcelList);
                PrintParcels(parcelList);
            }
            else if (input == "4")
            {
                ReadParcelFile(parcelList);
                PrintParcels(parcelList);
            }
            else if (input == "5")
            {
                //run = false;
                break;
            }
            else
            {
                Console.WriteLine("Välj någon av valen");
                    
                continue;
                     
            }
  
        }
    }



    //============================== Methods ======================================= > > >

    static void BuildParcel(List<Parcel> parcelList,ShipFees shipFees)
    {
        parcelList.Clear();
        Parcel parcel = new();

        bool nameCheck = false;

        while (nameCheck == false)
        {
            Console.Write("Namn : "); 
            
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Namn får inte vara tomt");
                continue;
            }
            else
            {
                parcel.Sender = newName;
                nameCheck = true;
            }
    
        }

        Console.Write("Vikt :");  
        
        double newWeight = ParseDouble(Console.ReadLine()); 
        
        parcel.Weight = newWeight;

        bool insuranceCheck = false;

        while (insuranceCheck == false)
        {
            Console.Write("Skall paketet försäkras? [j/n] :"); 
            
            string insurance = Console.ReadLine(); 

            if (insurance == "j")
            {
                parcel.Insurance = true;

                Console.WriteLine("Upskattat värde i kr? :  ");

                double newValue = ParseDouble(Console.ReadLine());

                parcel.Value = newValue;
                insuranceCheck = true;
                
            }
            else if (insurance == "n")
            {
                insuranceCheck = true;
            }
        
        }

        bool memberCheck = false;

        while (memberCheck == false)
        {
            Console.WriteLine("Är du medlem? [j/n] :");
            
            string member = Console.ReadLine();

            if (member == "j")
            {
                parcel.Member = true;
                memberCheck = true;
            }
            else if (member == "n")
            {
                memberCheck = true;
            }
        }

        Console.WriteLine("Vilket land skall du skicka till?");

        //parcel.Country = Console.ReadLine();

        for (int i = 0;i < shipFees.Fees.Count; i++)
        {
            Console.WriteLine($"{i+1}.{shipFees.Fees[i].Name}");
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        char key = keyInfo.KeyChar;

        if (char.IsDigit(key))
        {
            int choice = key - '0';

            CountryID selectedCountry = shipFees.Fees[choice - 1];

            parcel.Country = selectedCountry.Name;
            
        }




        parcelList.Add(parcel);
    
    }

     static double ParseDouble(string input)
    {

        while (true)
        {
            
            if (!double.TryParse(input,CultureInfo.InvariantCulture,out double number))
            {
                Console.WriteLine("Försök igen");
                input = Console.ReadLine();
                continue;
            }
            else
            {
                return number; 
            }
       
        }
    }

    static string SetMembership(Parcel parcel)
    {

        if (parcel.Member == true)
        {
            return "Ja";
        }
        
        else
        {
            return "Nej";
        }
    }

    static string SetInsurance(Parcel parcel)
    {
        if (parcel.Insurance == true)
        {
            return "Ja";
        }
        else
        {
            return "Nej";
        }
    }

    static void ReadParcelFile(List<Parcel> parcelList)
    {   
        parcelList.Clear();
        bool fileCheck = false;

        while (fileCheck == false)
        {
            
            Console.WriteLine("Ange sök väg: \n");
            string inputPath = Console.ReadLine();
            string path = Path.Combine("files",$"{inputPath}.txt");
            

            if (String.Equals(inputPath,"exit", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }


            if (File.Exists(path))
            {
                string[] parcels = File.ReadAllLines(path);

                foreach (string parcelInfo in parcels)
                {
                    string[] splitInfo = parcelInfo.Split(';');
                    Parcel newParcel = new();

                    newParcel.Sender = splitInfo[0];
                    newParcel.Weight = ParseDouble(splitInfo[1]);
                    if (splitInfo[2] == "Ja" || splitInfo[2] == "True")
                    {
                        newParcel.Insurance = true;
                    }
                    newParcel.Value = ParseDouble(splitInfo[3]);
                    if (splitInfo[4] == "Ja" || splitInfo[4] == "True")
                    {
                        newParcel.Member = true;
                    }
                    newParcel.Country = splitInfo[5];

                    newParcel.Tag = splitInfo[6];



                    parcelList.Add(newParcel);
                }

                fileCheck = true;
            }
            else
            {
                Console.WriteLine("Filen finns inte\n");
            }
            
        }

       
    }

    static void WriteParcelsToFile(List<Parcel> parcelList)
    {
        

        bool fileCheck = false;
        

        while (fileCheck == false)
        {
            Console.WriteLine("Ange vart filerna skall sparas: \n");
            string inputPath = Console.ReadLine();
            string path = Path.Combine("files",$"{inputPath}.txt");

            if (string.Equals(inputPath, "exit", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
                
            if (File.Exists(path))
            {
                foreach (Parcel parcel in parcelList)
                {
                    string writeParcel =
                    $"{parcel.Sender};"+
                    $"{parcel.Weight};"+
                    $"{parcel.Insurance};"+
                    $"{parcel.Value};"+
                    $"{parcel.Member};"+
                    $"{parcel.Country};"+
                    $"{parcel.Tag};"+
                    $"{parcel.BaseCost};"+
                    $"{parcel.WeightCost};"+
                    $"{parcel.HighWeightCost};"+
                    $"{parcel.InsuranceCost};"+
                    $"{parcel.TotalCost}";
                    

                    File.AppendAllLines(path,[writeParcel]);
                    
                }

                fileCheck = true;
            }
            else
            {
                Console.WriteLine("Filen finns inte\n");
            }

        }
        
    }
    static void PrintParcels(List<Parcel> parcelList)
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (Parcel parcel in parcelList)
        {
            
            string membership = SetMembership(parcel);
            string insurance = SetInsurance(parcel);
            Console.WriteLine("\n\n\n");
            Console.WriteLine("====================================");
            Console.WriteLine("             FRAKTKVITTO            ");
            Console.WriteLine("====================================");
            Console.WriteLine($"Avsändare: {parcel.Sender}"         );
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Vikt: {parcel.Weight}");
            if (parcel.Insurance == true)
            {
                Console.WriteLine($"Försäkring: {insurance}");
                Console.WriteLine($"Värde: {parcel.Value}");
            }
            Console.WriteLine($"Medlem: {membership}");
            Console.WriteLine($"Land: {parcel.Country}");
            Console.WriteLine($"\n");
            Console.WriteLine($"Grundavgift: {parcel.BaseCost}");
            Console.WriteLine($"Viktavgift: {parcel.WeightCost}");
            if (parcel.HighWeightCost != 0)
            {
                Console.WriteLine($"Tungviktsgods: {parcel.HighWeightCost}");
                
            }
            if (parcel.Insurance == true)
            {
                Console.WriteLine($"Försäkringsavgift: {parcel.InsuranceCost}");
                
            }
            Console.WriteLine("\n");
            Console.WriteLine($"Paket ID: {parcel.Tag}");
            Console.WriteLine("====================================");
            Console.WriteLine($"Totalt att betala: {parcel.TotalCost}");
            Console.WriteLine("====================================\n");
            
        }
        Console.ResetColor();
        Console.Write("Tryck Enter för att forsätta ");
        Console.ReadLine();
    }

    static void TagParcel(List<Parcel> parcelList)
    {
    
        foreach (Parcel parcel in parcelList)
        {
            bool fileExistsCheck = false;

            while(fileExistsCheck == false)
            {
                
                DateTime time = DateTime.Now;
                int idNumber = Random.Shared.Next(100000,10000000);
                string tag = $"PK-{time:yyMMdd-hh}-{idNumber}";

                parcel.Tag = tag;

                string dirPath = Path.Combine("files","tagfiles",$"{parcel.Tag}");

                if (File.Exists(dirPath))
                {
                    continue;
                }
                else
                {
                    File.Create(dirPath).Dispose();

                    string writeParcel =
                    $"{parcel.Sender};"+
                    $"{parcel.Weight};"+
                    $"{parcel.Insurance};"+
                    $"{parcel.Value};"+
                    $"{parcel.Member};"+
                    $"{parcel.Country};"+
                    $"{parcel.Tag};"+
                    $"{parcel.BaseCost};"+
                    $"{parcel.WeightCost};"+
                    $"{parcel.HighWeightCost};"+
                    $"{parcel.InsuranceCost};"+
                    $"{parcel.TotalCost}";
                    

                    File.AppendAllText(dirPath,writeParcel);
                    fileExistsCheck = true;
                }
            }
        }
    }

    static void SearchParcel(List<Parcel> parcelList)
    {
        Console.Write("Ange Paket ID : ");
        string tag = Console.ReadLine();

        string path = Path.Combine("files","tagfiles",tag);

        if (File.Exists(path))
        {
            LoadSearch(parcelList,path);
        }
    }

    static void LoadSearch(List<Parcel> parcelList,string path)
    {
        parcelList.Clear();
        string[] parcels = File.ReadAllLines(path);

            
    
        foreach (string parcelInfo in parcels)
        {
            string[] splitInfo = parcelInfo.Split(';');
            Parcel newParcel = new();

            newParcel.Sender = splitInfo[0];
            newParcel.Weight = ParseDouble(splitInfo[1]);
            if (splitInfo[2] == "Ja" || splitInfo[2] == "True")
            {
                newParcel.Insurance = true;
            }
            newParcel.Value = ParseDouble(splitInfo[3]);
            if (splitInfo[4] == "Ja" || splitInfo[4] == "True")
            {
                newParcel.Member = true;
            }
            newParcel.Country = splitInfo[5];

            newParcel.Tag = splitInfo[6];



            parcelList.Add(newParcel);
        }
            
    }

    static void WriteOrderToFile(List<Parcel> parcelList,string week)
    {
        string path = Path.Combine("files",week);

        if (File.Exists(path))
        {
            foreach (Parcel parcel in parcelList)
            {
                string writeParcel =
                $"{parcel.Sender};"+
                $"{parcel.Weight};"+
                $"{parcel.Insurance};"+
                $"{parcel.Value};"+
                $"{parcel.Member};"+
                $"{parcel.Country};"+
                $"{parcel.Tag};"+
                $"{parcel.BaseCost};"+
                $"{parcel.WeightCost};"+
                $"{parcel.HighWeightCost};"+
                $"{parcel.InsuranceCost};"+
                $"{parcel.TotalCost}";
                

                File.AppendAllText(path,writeParcel+ Environment.NewLine);
                
            }
        }
    }

}


