



class Parcel
{
    public string Sender {get;set;}

    public double Weight {get;set;}

    public double Value {get;set;} 

    public bool Member {get;set;}

    public bool Insurance {get;set;}

    public string Country {get;set;} = " ";

    public double BaseCost => GetBaseCost(Country);

    public double HighWeightFee = 30;

    public double HighWeightLimit = 20;

    public double HighWeightCost => GetHightWeightCost(Weight,HighWeightLimit,HighWeightFee);

    public double WeightCost => GetWeightCost(Weight,Member);
    
    public double TotalCost => GetTotalCost(BaseCost,WeightCost,HighWeightCost,InsuranceCost);

    public double InsuranceCost => GetInsuranceCost(Value);

    public string Tag {get;set;} = "NotSet";



//========================= Methods ========== > > > >

    public double GetBaseCost(string country)
    {
        double cost = 0;
        
        switch (country)
        {
            case "Sverige":
            cost = 49;
            return Math.Round(cost,2);
        
            case "Danmark":
            cost = 79;
            return Math.Round(cost,2);
        
            case "Norge":
            cost = 99;
            return Math.Round(cost,2);

            case "Finland":
            cost = 89;
            return Math.Round(cost,2);
            case "Island":
            cost = 129;
            return Math.Round(cost,2);
    
            case "International":
            cost = 499;
            return Math.Round(cost,2);
        }

        return Math.Round(cost,2);
    }




    public double GetHightWeightCost(double weight,double higweightLimit,double highweightFee)
    {
        if (weight > higweightLimit)
        {
            double cost = (weight-higweightLimit)*highweightFee;
        
            return Math.Round(cost,2);
        }
        else
        {
            return 0;
        }
        
    }

    public double GetWeightCost(double weight,bool member)
    {
        double freeWeight = 2;
        double cost = 0;

        if (member == true)
        {
            freeWeight = 5;
        }

        if (weight <= freeWeight)
        {
            
        }
        else if (weight > freeWeight)
        {
            
            cost = (weight - freeWeight)*10;
        }

        return Math.Round(cost,2);
    }

    public double GetInsuranceCost(double value)
    {
        double cost = 0;

        if (Insurance == true)
        {
            cost = value*0.01;
            return Math.Round(cost,2);
        }
        
        return Math.Round(cost,2);
    }

    public double GetTotalCost(double baseCost,double weightCost,double highweightCost,double insuranceCost)
    {
        double cost = weightCost+highweightCost+baseCost+insuranceCost;

        return Math.Round(cost,2);
        
    }

}

