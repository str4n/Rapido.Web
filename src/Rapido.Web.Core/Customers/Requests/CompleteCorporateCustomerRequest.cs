namespace Rapido.Web.Core.Customers.Requests;

public sealed record CompleteCorporateCustomerRequest(string Name, string TaxId, string Country, 
    string Province, string City, string Street, string Postalcode, string Nationality);