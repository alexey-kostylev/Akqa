Solution captures a name and a number, converts the number into a word representation and returns this data back as a web page.	

solution employs ASP.NET MVC technology using Visual Studio 2015.

ASP.NET MVC is choosen to keep the balance between development and installment and also to keep unit tests simple and consistent. 
The alternative would be to use a Single Page Application like Angular, but the installment phase is significantly more complex because of different technolgies used and unit tests would have to be done in two different environments.

used components that dont come along with the standard ASP.NET Web application template
	- unity
	- Test components:
		- FluentAssertions
		- Moq
		- AutoFixture

Solution structure:
	Converting logic - Akqa.Logic project
	Web - Akqa.Web
	Tests - Akqa.Tests

Web part exposes 3 endpoints:
	- default get action to display a form 
	- post action to accept the form
	- get action to display converted data

Workflow:
	- Open the supplied .sln file, press F5. The visual studio wil download the required nuget packages, start IIS express and open a default view.
	- in the new form enter name and a number and press Enter.
	- another view will appear on the screen with converted data.
	- to return to the previous form you have to click on a link in the bottom of resulted view or to refresh the page.

Issues:
	I could not achieve 100% test coverage of Akqa.Web project because some classes are not called directly lile MvcApplication.