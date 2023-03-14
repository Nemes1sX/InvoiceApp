# InvoiceApp
Web API which issues invoices in .NET 7
## Functionality
Issues invoices. The invoice is issued by the service provider(legal person from EU) to its customer. VAT is applied when service provider is VAT payer and client who can be individual or legal person is from EU. 
VAT value is determined by service provider country not to mention to apply the case above.
## Installation
* First, download the given Project from my GitHub repository / from article resources.
* Open the project using visual studio
* Go to package manager console (VS) or CLI (VSC)
* Update the database ```Update-Database``` (in VS PM console) or ```dotnet ef database update``` (in CLI terminal) for example if project is launched from VSC.
* To seed countries, some legal persons and individuals in the project folder terminal run command ```dotnet run seeddata``` after while you can terminate with Ctrl+C
* Run the project
