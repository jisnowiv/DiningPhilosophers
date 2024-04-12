package dp;

public class Philosopher implements Runnable {
	private Object leftFork;
	private Object rightFork;
	private Integer limit = 100;

	public Philosopher(Object left, Object right) {
		this.leftFork = left;
		this.rightFork = right;
	}

	public Philosopher(Object left, Object right, int loops) {
		this.leftFork = left;
		this.rightFork = right;
		this.limit = loops;
	}

	private void doAction(String action) throws InterruptedException {
		System.out.println(Thread.currentThread().getName() + " " + action);
		Thread.sleep(((int) (Math.random() * 100)));
	}

	@Override
	public void run() {
		try {
			doAction(System.nanoTime() + ": Starting for " + this.limit + " loops.");
			int count = 0;
			while (count < this.limit) {
				doAction(System.nanoTime() + ": Thinking");
				synchronized (leftFork) {
					doAction(System.nanoTime() + ": Picked up left fork");
					synchronized (rightFork) {
						doAction(System.nanoTime() + ": Picked up right fork");
						doAction(System.nanoTime() + ": Put down right fork");
					}
					doAction(System.nanoTime() + ": Put down left fork. Returning to thinking.");
				}
				count += 1;
			}
			doAction(System.nanoTime() + ": Done eating. Exiting thread.");
		} catch (InterruptedException e) {
			Thread.currentThread().interrupt();
		}
	}
}