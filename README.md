# DiningPhilosophers
Java implementation of the dining philosophers problem. There is also a rough C# implementation under ```program.cs```. Since I use a Mac I was only able to run the C# version on an online compiler, but I wanted to write it out as a C# threading refresher.

## Build Instructions
```
javac -d . philosopher.java
javac -d . diningphilosophers.java
```

## Run Instructions
For the default execution (5 philosophers, 100 thread loops):
```
java dp.DiningPhilosophers
```
To customize execution:
```
java dp.DiningPhilosophers [-d/-duration thread_loops] [-c/-count philosopher_count]
```
