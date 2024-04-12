/**
 * to build:
 * 		javac -d . philospher.java
 *		javac -d . diningphilosophers.java
 * to run:
 * 		java dp.DiningPhilosophers [-d thread_loops] [-duration thread_loops] [-c philosopher_count] [-count philosopher_count]
 */

package dp;

public class DiningPhilosophers {
	public static void main(String[] args) throws Exception {
		int duration = 100;
		int count = 5;

		if (args.length > 0) {
			int idx = 0;
			while (idx < args.length) {
				System.out.println(args[idx]);
				

				if (args[idx].equals("-d") || args[idx].equals("-duration")) {
					idx += 1;
					try {
						duration = Integer.parseInt(args[idx]);
						//	System.out.println("Duration: " + duration);
					} catch (NumberFormatException e) {
						System.out.println("Invalid number for duration.");
					}
				}

				if (args[idx].equals("-c") || args[idx].equals("-count")) {
					idx += 1;
					try {
						count = Integer.parseInt(args[idx]);
						//	System.out.println("Count: " + count);
					} catch (NumberFormatException e) {
						System.out.println("Invalid number for count.");
					}
				}
				idx += 1;
			}
		}

		Philosopher[] philosophers = new Philosopher[count];
		Object[] forks = new Object[philosophers.length];
		Thread[] threads = new Thread[philosophers.length];

		for (int i = 0; i < forks.length; i++) {
			forks[i] = new Object();
		}

		for (int i = 0; i < philosophers.length; i++) {
			Object leftFork = forks[i];
			Object rightFork = forks[(i + 1) % forks.length];

			if (i == philosophers.length - 1) {
				//	The last philosopher picks up the right fork first
				philosophers[i] = new Philosopher(rightFork, leftFork, duration);
			} else {
				philosophers[i] = new Philosopher(leftFork, rightFork, duration);
			}

			Thread t = new Thread(philosophers[i], "Philosopher " + (i + 1));
			t.start();
		}
	}
}