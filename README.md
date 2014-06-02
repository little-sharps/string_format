[![Build status](https://ci.appveyor.com/api/projects/status/80cp4snyr72k93jr)](https://ci.appveyor.com/project/cbrianball/string-format)
StringFormat
=============

This library extends the functionality of String.Format (and StringBuilder.AppendFormat). It allows you to use named variables instead of ordinal place holders.

	TokenStringFormat.Format("{name} was born on {dob:D}. {name} was {weight} lbs. and {height} inches long.", new {name = "John", dob = DateTime.Today, weight = 11, height = 19});
	
The output would be:

	"John was born on Sunday, May 16 2012. John was 11 lbs. and 19 inches long."
	
The package is now available on [NuGet.org](http://nuget.org/packages/StringFormat)

License
=======
Copyright (c) 2011 Brian Ball and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
