# WPF N-Tier Test

WPF sales management application with 3-Tier architechture, targeted for EFcore and SQL server infrastructure.
## General architechture overview :

Three-tier architecture is a well-established software application architecture that organizes applications into three logical and physical computing tiers: the presentation tier, or user interface; the application tier, where data is processed; and the data tier, where application data is stored and managed. Read more : https://www.ibm.com/topics/three-tier-architecture

The proposed design in this application is a 3 tier infused with MVVM, see bellow figure :

![image](https://github.com/anisdjaidja/WPF-N-Tier-Test/assets/58264397/fde3bfca-7ed2-46e2-ae85-3f9cc99c00c7)

## Requirements to build

- EFcore version 9 (compatibility issues with version 6)
- .net core 6, 7, 8
- SQL server version 2019

## Important notes

- We are supposed to gitignore the datacontex factory located in the Data accesss layer but for demonstration purposes it is included in the repo.
- We are supposed to move password handeling logic to the Data access layer or to the Logic layer, we chose to move it to Logic layer for demonstration purposes.

## Author : Anis Djaidja, IT, Software Engineer
## Disclaimer : this project is for a test in mformatik company, not developed with production in mind
