# Contributing Guidelines

Our philosophy is that freedom is needed to find creative solutions for problems. Therefore, these guidelines are not very detailed.
However, there are some rules to help each other understanding and maintaining the programming code. These are the rules for coding.

1. Use C# as the one and only .NET language for this project. Do not write code in VB.NET or F#.NET.
2. If you fundamentally disagree with the SOLID principles, then do not contribute. Logically, you are allowed to deviate from SOLID if you
have specific good reasons to do so.
3. When creating collections, you should prefer using LINQ over using loops.
4. Use the dispose pattern for disposable objects that really require a disposal (basically everything except Tasks). 
5. Use resharper but feel free to ignore warnings if you think they are useless.

There are also rules for architecture. Our current lead developer and software architect is [Daan Acohen](https://github.com/DaanAcohen).
Here are our architecture rules:
1. Do not change the architecture yourself. We will not accept this. If you want to change it, propose a new architecture to Daan.
2. Read our architecture page on our [wiki](https://github.com/ConnectingApps/DocMySwagApp/wiki).
