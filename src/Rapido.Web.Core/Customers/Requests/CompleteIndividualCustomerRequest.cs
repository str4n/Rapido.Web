namespace Rapido.Web.Core.Customers.Requests;

public sealed record CompleteIndividualCustomerRequest(string Name, string FullName, string Country, 
    string Province, string City, string Street, string Postalcode, string Nationality);