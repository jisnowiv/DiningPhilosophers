# DiningPhilosophers
Java implementation of the dining philosophers problem.

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
