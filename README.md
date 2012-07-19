StringFormat
=============

This library extends the functionality of String.Format (and StringBuilder.AppendFormat). It allows you to use named variables instead of ordinal place holders.

	TokenStringFormat.Format("{name} was born on {dob:D}. {name} was {weight} lbs. and {height} inches long.", new {name = "John", dob = DateTime.Today, weight = 11, height = 19});
	
The output would be:

	"John was born on Sunday, May 16 2012. John was 9 lbs. and 19 inches long."
	
The package is now available on [NuGet.org](http://nuget.org/packages/StringFormat)