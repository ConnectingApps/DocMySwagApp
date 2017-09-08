# DocMySwagApp
A .NET Core command line tool to generate a human readable document (e.g. html) from a Swagger xml file

# Table of contents

  - [DocMySwagApp](#docmyswagapp)
  - [Table of contents](#table-of-contents)
  - [Use DocMySwagApp](#use-docmyswagapp)
	- [Get DocMySwagApp](#get-docmyswagapp)
	- [Create documentation](#create-documentation)
  - [Test DocMySwagApp](#test-docmyswagapp)
  - [Join The Development Team](#join-the-development-team)
  - [FAQ](#faq)
	- [What is a swagger xml file?](#what-is-a-swagger-xml-file)
	- [Why do I need this?](#why-do-i-need-this)
	- [Are Linux and Mac OS supported?](#are-linux-and-mac-os-supported)
	- [Can I use my own document type?](#can-i-use-my-own-document-type)

# Use DocMySwagApp

## Get DocMySwagApp
To get the complete solution, you can just download it. The easiest way to do so is to [download the zip file](https://github.com/ConnectingApps/DocMySwagApp/archive/master.zip) with the complete content.
To create a running application, you need to have the [.NET Core SDK 2.0](https://www.microsoft.com/net/download/core) installed. The project (inside the solution) to publish is DocMySwagApp.
If you have visual studio 2017 installed, you can use that to publish. Otherwise you can publish it from the command-line.
Just open the folder, you just added to your PC by extracting the zip file and type:

```sh
cd DocMySwagApp
dotnet publish -c RELEASE -o D:/TargetFolder
```
The folder DocMySwagApp is the project folder of the actual commandline tool. It is inside the same folder where you can find the solution file.
The D:/TargetFolder can be replaced by any folder you want to publish to.

## Create documentation
The main goal of the tool is to generate documentation from a swagger xml. A typical command-line way to do this is this:
```sh
dotnet DocMySwagApp.dll i=C:\MyProject\SwaggerFile.xml o=C:\MyDocumentationFolder\MyDocumentation.html t=html
```
Logically, you should be aware of the operating system you use. Unix based operating systems are case sensitive when it comes to file paths.
Windows is not like that. Unix based operating systems also have a different convention for slashes than windows . 

If you want a more detailed explanation of the tool, just do not give arguments and run it like this.
```sh
dotnet DocMySwagApp.dll
```

# Test DocMySwagApp
The solution contains automated tests in the following folders: ApiModel.UnitTest, DocMySwagApp.IntegrationTest, DocMySwagApp.UnitTest and FullHtmlGeneration.UnitTest .
To run them, you can use visual studio or run them from the command-line like this:

```sh
dotnet test -c RELEASE
```

Feel free to use this from the root folder, you may get an error message for projects that do not have automated tests. You can ignore these.

# Join the Development Team
The tool has been created to be used and improved by .NET developers. You can help yourself, your organisation and other .NET developers
by fixing bugs, discovering bugs and last but not least adding support for new document types. If you want to help, [contact Connecting Apps](http://www.connectingapps.net/contact/).

Team member   | Join Date   | Team role      |
------------- | ----------- |----------------|
[@DaanAcohen](https://github.com/DaanAcohen)   | 08-09-2017  | Lead developer |

# FAQ

## What is a swagger xml file?
A swagger xml file is a file to describe your complete web API including code comments and data models.

If your web api does not have a swagger xml file, just add it after you added the swagger dependency. You can do this with 
[Visual Studio](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio#tabpanel_9fKY8SstIr-1_visual-studio), 
Visual Studio for Mac or Visual Studio Code.

## Why do I need this?
If you create a web backend using .NET Core, you may want to add this info in a nice human readable document that you can share in your document management system.
If this info is in an XML file, it useless for this purpose. You can still see it in your swagger web interface but not on any other place.
This tool parses your swagger xml file and creates a human readable document such as html to solve the problem.

## Are Linux and Mac OS supported?
Yes. It is a .NET Core project for exactly this reason. Most .NET developers, including .NET Core Developers, use Windows and this tool has
been initially created on Windows but we highly appreciate it if you use, test and further develop it on a Unix based OS. 

## Can I use my own document type?
It is open source for a reason. If your document type is not supported, feel free to add a project into the solution that does support it.





