
public class ShipFees
{
    public CountryID SWE = new("Sverige",49);
    public CountryID DE = new("Danmark",79);

    public CountryID NE = new("Norge",99);
    public CountryID FIN = new("Finland",89);

    public CountryID ISL = new("Island",149);
    public CountryID INT = new("International",499);

    public List<CountryID> Fees = new();
    
    public ShipFees()
    {
        Fees = [SWE,DE,NE,FIN,ISL,INT];
    } 
}