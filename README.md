# leagueranking
Leauge Ranking Application in c# and python

This is the Span Digital Coding Test

I have done this in C# (as this is currently my most proficient language) as well as Python (my python is a bit rusty). Both runs on Linux. The C# one does not have unit testing or logging but it is included in the Python application. I did it in both languages to showcase my understand of the problem as well as showing my technical skills in different programming languages

PYTHON
-------

The Python version runs on Python 3. No additional libraries were used. The project can be cloned and run using the command below:

python main.py or python3 main.py

I have included some basic unit testing that needs to be expanded. To run the unit tests run the command below:

python test_leagueranking_unittest.py or python3 test_leagueranking_unittest.py

This version takes the stdin as input. Simply paste the results (i have included a version as well named ranking.txt) and press q to calculate the ranking.

C#
---

The C# version has been compiled into a stand-alone release. This means that everything is needed to run it in a cross-platform environment. The code is in the C# folder and the c#_compiled folder contains the runtime version. Simply cd into the directory and run ./Leagueranking. This will bring up the application

This version uses both command line input as well as file input. To test file input simply specify the full path and name to the file i.e. /tmp/ranking.txt
